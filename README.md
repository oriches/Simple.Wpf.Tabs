Simple.Wpf.Tabs
===============

[![Build status](https://ci.appveyor.com/api/projects/status/db6cyf2xksk6sc4o/branch/master?svg=true)](https://ci.appveyor.com/project/oriches/simple-wpf-tabs/branch/master)

As with all my 'important' stuff it builds using the amazing [AppVeyor](https://ci.appveyor.com/project/oriches/simple-wpf-tabs).

The app is skinned using [Mah Apps](http://mahapps.com/).

Okay this is *opinionated software* on the way to build a tabbed UI in an MVC based architect implemented in WPF using MVVM as the flavour of MVC.

The reason for this existing is not because it doesn't already exist but I've had to look at so many implementations and come to the conclusion most are frankly not fit-for-purpose. They do the job but they make the developers life hell when trying to a new tab or modify how the tabs work. I want to understand how *I would do it* and if I could keep it to the bare minimum of functionality.

First off, I use modern versions of tooling available to any .Net (WPF) developer - eg rx.net, josn.net, autofac.

My experiences of working in the enterprise has taught me that most WPF applications are bogged down using PRISM & Unity, both in this developer's opinion are shite and poor implementations of a pluggable architecture and an IoC container. PRISM really is the worst kind of software - 'a solution looking for a problem'. If I need a truely dynamic pluggable arhcitecture I probably still wouldn't use PRISM.

rant over :)

What is needed to add a new tab to app?

3 things, a View Model for tab contents, a Template (View) for rendering the View Model and Strategy defining how to create the View Model.

As an example I've taken the 'Blue' tab from the app as an exaMPLE, I've also included all the base classes & interfaces:

###View Model

```C#
public sealed class Tab
{
  public Guid TypeId { get; private set; }

  public string Name { get; private set; }

  public Tab(Guid typeId, string name)
  {
    TypeId = typeId;
    Name = name;
  }
}

public interface ITabViewModel : ITransientViewModel
{
  Tab Tab { get; }

  string Name { get; }
}

public interface IBlueTabViewModel : ITabViewModel
{
}

public abstract class TabBaseViewModel : BaseViewModel
{
  protected TabBaseViewModel(Tab tab)
  {
    Tab = tab;
  }

  public Tab Tab { get; }

  public string Name { get { return Tab.Name; } }
}

public sealed class BlueTabViewModel : TabBaseViewModel, IBlueTabViewModel
{
  public BlueTabViewModel(Tab tab) : base(tab)
  {
  }
}
```

###Template
With all the WPF MVVM apps I build I make heavy use of data templates pattern in XAML - I avoid implementing user controls because it encourages to much code behind :) If I need some code behind (purely for UI purposes) I use a Blend Behavior.

```XAML
<DataTemplate DataType="{x:Type t:BlueTabViewModel}">

    <Grid Background="Blue">

    </Grid>

</DataTemplate>
```

###Strategy
Why does it have a strategy? 

So when the application starts I don't need to know about the actually tab implementations, there is a service (TabService) which has an IEnumerable&lt;ITabStrategy&gt; injected and it will resolve the tab View Models and invoke the creating of the tab via the specific strategy when required. The tab service is responible for invoking the persistence & restoration of the tabs from the application settings (stored on disk).

```C#
public interface ITabStrategy : IStrategy
{
  Guid TypeId { get; }

  string Name { get; }

  bool CanHandle(Tab tab, out ITabViewModel tabViewModel);

  ITabViewModel Create();
}

public sealed class BlueTabStrategy : ITabStrategy
{
	private readonly Func<Tab, IBlueTabViewModel> _factory;
	public static readonly Tab Default = new Tab(Guid.Parse("{F0F9D927-7E08-4E17-AF1B-106B5DCF1C22}"), "Blue");

	public Guid TypeId { get; }

	public string Name { get; }

	public BlueTabStrategy(Func<Tab, IBlueTabViewModel> factory)
	{
		_factory = factory;

		TypeId = Default.TypeId;
		Name = Default.Name;
	}

	public bool CanHandle(Tab tab, out ITabViewModel tabViewModel)
	{
		tabViewModel = null;
		if (tab.TypeId == Default.TypeId)
		{
			tabViewModel = _factory(tab);
			return true;
		}

		return false;
	}

	public ITabViewModel Create()
	{
		return new BlueTabViewModel(new Tab(TypeId, Name));
	}
}
```

Happy coding...


