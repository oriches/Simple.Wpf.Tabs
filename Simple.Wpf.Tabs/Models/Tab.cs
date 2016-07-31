namespace Simple.Wpf.Tabs.Models
{
    using System;

    public sealed class Tab : IEquatable<Tab>
    {
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
            return obj is Tab && Equals((Tab) obj);
        }

        public override int GetHashCode()
        {
            return TypeId.GetHashCode();
        }

        public static bool operator ==(Tab left, Tab right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tab left, Tab right)
        {
            return !Equals(left, right);
        }

        public Guid TypeId { get; private set; }

        public string Name { get; private set; }

        public Tab(Guid typeId, string name)
        {
            TypeId = typeId;

            Name = name;
        }
    }
}