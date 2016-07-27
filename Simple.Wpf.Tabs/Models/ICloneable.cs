namespace Simple.Wpf.Tabs.Models
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}