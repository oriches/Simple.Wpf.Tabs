using System;

namespace Simple.Wpf.Tabs.Models
{
    public struct Memory : IEquatable<Memory>
    {
        public bool Equals(Memory other) => WorkingSetPrivate == other.WorkingSetPrivate && Managed == other.Managed;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Memory && Equals((Memory)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (WorkingSetPrivate.GetHashCode() * 397) ^ Managed.GetHashCode();
            }
        }

        public static bool operator ==(Memory left, Memory right) => left.Equals(right);

        public static bool operator !=(Memory left, Memory right) => !left.Equals(right);

        public Memory(decimal workingSetPrivate, decimal managed)
        {
            WorkingSetPrivate = workingSetPrivate;
            Managed = managed;
        }

        public decimal WorkingSetPrivate { get; }

        public decimal Managed { get; }
    }
}