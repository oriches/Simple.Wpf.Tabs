using System;

namespace Simple.Wpf.Tabs.Models
{
    public sealed class Heartbeat : IEquatable<Heartbeat>
    {
        public Heartbeat(string timestamp) => Timestamp = timestamp;

        public string Timestamp { get; }

        public bool Equals(Heartbeat other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Timestamp, other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Heartbeat && Equals((Heartbeat)obj);
        }

        public override int GetHashCode() => Timestamp?.GetHashCode() ?? 0;

        public static bool operator ==(Heartbeat left, Heartbeat right) => Equals(left, right);

        public static bool operator !=(Heartbeat left, Heartbeat right) => !Equals(left, right);
    }
}