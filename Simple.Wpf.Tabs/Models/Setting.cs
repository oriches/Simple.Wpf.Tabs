namespace Simple.Wpf.Tabs.Models
{
    using System.Diagnostics;

    [DebuggerDisplay("Name = {Name}, Value = {Value}")]
    public sealed class Setting
    {
        public string Name { get; private set; }

        public object Value { get; private set; }

        public Setting()
        {
        }

        public Setting(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}