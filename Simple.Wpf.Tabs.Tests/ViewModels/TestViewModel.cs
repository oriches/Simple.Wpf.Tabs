namespace Simple.Wpf.Tabs.Tests.ViewModels
{
    using Tabs.ViewModels;

    public sealed class TestViewModel : BaseViewModel
    {
        private string _stringProperty;
        private int _integerProperty;

        public string StringProperty
        {
            get { return _stringProperty; }
            set { SetPropertyAndNotify(ref _stringProperty, value, () => StringProperty); }
        }

        public int IntegerProperty
        {
            get { return _integerProperty; }
            set { SetPropertyAndNotify(ref _integerProperty, value, () => IntegerProperty); }
        }
    }
}