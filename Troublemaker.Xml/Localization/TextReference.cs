using System;

namespace Troublemaker.Xml
{
    public struct TextReference : IEquatable<TextReference>, IComparable<TextReference>, IComparable
    {
        private readonly String? _reference;
        
        public TextId Id { get; }

        public TextReference(String? reference, TextId id)
        {
            _reference = reference;
            Id = id;
        }

        public Boolean IsEmpty => String.IsNullOrEmpty(_reference);
        
        public Int32 CompareTo(TextReference other)
        {
            return String.Compare(_reference, other._reference, StringComparison.Ordinal);
        }

        public Int32 CompareTo(Object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is TextReference other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TextReference)}");
        }

        public Boolean Equals(TextReference other) => _reference == other._reference;
        public override Boolean Equals(Object? obj) => obj is TextReference other && Equals(other);
        public override Int32 GetHashCode() => (_reference != null ? _reference.GetHashCode() : 0);
        
        public static Boolean operator ==(TextReference left, TextReference right) => left.Equals(right);
        public static Boolean operator !=(TextReference left, TextReference right) => !left.Equals(right);
        public static implicit operator TextId(TextReference self) => self.Id;

        public override String ToString() => $"{_reference} ({Id})";

        public static TextReference Sentence(String key)
        {
            var reference = $"Sentence/{key}/Value";
            return LocalizationMap.Instance.TryGetValue(reference, out var id)
                ? new TextReference(reference, id)
                : default;
        }
    }
}