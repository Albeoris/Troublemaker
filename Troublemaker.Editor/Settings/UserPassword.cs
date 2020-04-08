using System;
using System.Buffers.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Troublemaker.Editor.Settings
{
    public sealed class UserPassword
    {
        private const Int32 Size = 256;
        
        private readonly Byte[] _salt;
        private readonly Byte[] _saltedPassword;
        
        public UserPassword(String rawPassword)
        {
            _salt = new Byte[Size];
            Byte[] password = Encoding.UTF8.GetBytes(rawPassword);
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                rngCsp.GetNonZeroBytes(_salt);

            using (var rfc = new Rfc2898DeriveBytes(password, _salt, 3))
                _saltedPassword = rfc.GetBytes(Size);
        }

        private UserPassword(Byte[] salt, Byte[] saltedPassword)
        {
            _salt = salt;
            _saltedPassword = saltedPassword;
        }

        public Boolean IsValid(String rawPassword)
        {
            Byte[] password = Encoding.UTF8.GetBytes(rawPassword);
            
            using (var rfc = new Rfc2898DeriveBytes(password, _salt, 3))
            {
                Byte[] saltedPassword = rfc.GetBytes(Size);
                for (int i = 0; i < Size; i++)
                {
                    if (saltedPassword[i] != _saltedPassword[i])
                        return false;
                }
            }

            return true;
        }

        private const String Header = "---------- v1 ----------";
        public String Serialize(String userName)
        {
            StringBuilder sb = new StringBuilder(2048);
            sb.AppendLine(Header);
            sb.AppendLine(userName);
            sb.AppendLine(Convert.ToBase64String(_salt));
            sb.AppendLine(Convert.ToBase64String(_saltedPassword));
            return sb.ToString();
        }

        public static (String userName, UserPassword password) Deserialize(String content)
        {
            using (StringReader sr = new StringReader(content))
            {
                var header = sr.ReadLine();
                if (header != Header)
                    throw new NotSupportedException(header);

                String userName = sr.ReadLine();
                Byte[] salt = Convert.FromBase64String(sr.ReadLine());
                Byte[] saltedPassword = Convert.FromBase64String(sr.ReadLine());

                var password = new UserPassword(salt, saltedPassword);
                return (userName, password);
            }
        }
    }
}