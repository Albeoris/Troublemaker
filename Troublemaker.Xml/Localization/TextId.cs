using System;

namespace Troublemaker.Xml
{
    public struct TextId : IComparable<TextId>, IComparable, IEquatable<TextId>
    {
        public String Type { get; }
        public Int32 Index { get; }

        public TextId(String type, Int32 index)
        {
            Type = String.Intern(type);
            Index = index;
        }
        
        public static TextId ParseLine(String value)
        {
            Int32 index = value.IndexOf('#');
            if (index < 1)
                throw new NotSupportedException(value);

            var type = value.Substring(0, index);
            var id = Int32.Parse(value.Substring(index + 1));
            return new TextId(type, id);
        }

        public String FormatLine() => $"{Type}#{Index}";

        public static TextId ParsePath(String value)
        {
            var result = TryParsePath(value);
            if (result is null)
                throw new NotSupportedException(value);

            return result.Value;
        }

        public static TextId? TryParsePath(String value)
        {
            Int32 index = value.IndexOf('/');
            if (index < 1)
                return null;

            var type = value.Substring(0, index);
            if (Int32.TryParse(value.Substring(index + 1), out var id))
                return new TextId(type, id);

            return null;
        }

        public String FormatPath() => $"{Type}/{Index}";
        public Boolean Equals(TextId other) => Type == other.Type && Index == other.Index;
        public override Boolean Equals(Object? obj) => obj is TextId other && Equals(other);
        public override Int32 GetHashCode() => HashCode.Combine(Type, Index);
        public static Boolean operator ==(TextId left, TextId right) => left.Equals(right);
        public static Boolean operator !=(TextId left, TextId right) => !left.Equals(right);

        public Int32 CompareTo(TextId other)
        {
            var typeComparison = String.Compare(Type, other.Type, StringComparison.Ordinal);
            if (typeComparison != 0) return typeComparison;
            return Index.CompareTo(other.Index);
        }

        public Int32 CompareTo(Object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is TextId other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TextId)}");
        }

        public override String ToString() => FormatLine();
    }
}