using System;

namespace Troublemaker.Xml
{
    public readonly struct Issuer
    {
        public static String CurrentName { get; set; } = "Anonymous";
        public static Issuer GetCurrent() => new Issuer(CurrentName, DateTimeOffset.Now);

        public String Name { get; }
        public DateTimeOffset Time { get; }

        public Issuer(String name, in DateTimeOffset time)
        {
            Name = name;
            Time = time;
        }

        public Boolean Equals(Issuer other) => Name == other.Name && Time.Equals(other.Time);
        public static Boolean operator ==(Issuer left, Issuer right) => left.Equals(right);
        public static Boolean operator !=(Issuer left, Issuer right) => !left.Equals(right);

        public override Boolean Equals(Object? obj) => (obj is Issuer issuer) && Equals(issuer);
        public override Int32 GetHashCode() => HashCode.Combine(Name, Time);
        public override String ToString() => $"{Name} on {Time.LocalDateTime:U}";
    }
}