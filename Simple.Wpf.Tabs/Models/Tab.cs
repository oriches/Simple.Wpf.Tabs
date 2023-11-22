using System;

namespace Simple.Wpf.Tabs.Models
{
    public sealed class Tab : IEquatable<Tab>
    {
        public Tab(Guid typeId, string name)
        {
            TypeId = typeId;

            Name = name;
        }

        public Guid TypeId { get; }

        public string Name { get; }

        public bool Equals(Tab other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TypeId.Equals(other.TypeId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Tab && Equals((Tab)obj);
        }

        public override int GetHashCode() => TypeId.GetHashCode();

        public static bool operator ==(Tab left, Tab right) => Equals(left, right);

        public static bool operator !=(Tab left, Tab right) => !Equals(left, right);
    }
}