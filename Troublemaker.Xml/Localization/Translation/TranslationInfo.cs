using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public sealed class TranslationInfo
    {
        public Issuer Changed { get; }
        public Issuer Approved { get; private set; }

        public String Text { get; }

        public TranslationInfo(String text, in Issuer changed, in Issuer approved)
        {
            Text = text;

            Changed = changed;
            Approved = approved;
        }

        public String Title
        {
            get
            {
                if (Approved != default)
                    return $"Approved by {Approved.Name}{Environment.NewLine}{Approved.Time.LocalDateTime}";
                
                if (Changed != default)
                    return $"Changed by {Changed.Name}{Environment.NewLine}{Changed.Time.LocalDateTime}";

                return String.Empty;
            }
        }

        public TranslationInfo CreateEdit(String text)
        {
            Issuer current = Issuer.GetCurrent();
            return new TranslationInfo(text, current, default);
        }

        public void Approve()
        {
            Approved = Issuer.GetCurrent();
        }

        public void Disapprove()
        {
            Approved = default;
        }

        private const String Separator = "----- v1 -----";
        private const String Undefined = "<Undefined>";

        public void Serialize(StringWriter sw)
        {
            sw.WriteLine(Separator);
            WriteIssuer(Changed, sw);
            WriteIssuer(Approved, sw);
            sw.WriteLine(Text);
        }

        public static Boolean TryDeserialize(StringReader sr, [NotNullWhen(true)] out TranslationInfo? info)
        {
            String? line = sr.ReadLine();
            if (line is null)
            {
                info = null;
                return false;
            }

            if (line != Separator)
                throw new NotSupportedException(line);

            Issuer changed = ReadIssuer(sr);
            Issuer approved = ReadIssuer(sr);
            String text = sr.RequireLine();

            info = new TranslationInfo(text, changed, approved);
            return true;
        }

        private void WriteIssuer(in Issuer created, StringWriter sw)
        {
            if (created == default)
            {
                sw.WriteLine(Undefined);
            }
            else
            {
                sw.WriteLine(created.Name);
                sw.WriteLine(created.Time.ToString("O", CultureInfo.InvariantCulture));
            }
        }

        private static Issuer ReadIssuer(StringReader sr)
        {
            String name = sr.RequireLine();
            if (name == Undefined)
                return default;
            
            DateTimeOffset time = DateTimeOffset.ParseExact(sr.RequireLine(), "O", CultureInfo.InvariantCulture);
            return new Issuer(name, time);
        }
    }
}