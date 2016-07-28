Simple.Wpf.Tabs
===============

[![Build status](https://ci.appveyor.com/api/projects/status/db6cyf2xksk6sc4o/branch/master?svg=true)](https://ci.appveyor.com/project/oriches/simple-wpf-tabs/branch/master)

As with all my 'important' stuff it builds using the amazing [AppVeyor](https://ci.appveyor.com/project/oriches/simple-wpf-tabs).


Okay this is *opinionated software* on the way to build a tabbed UI in an MVC based architect implemented in WPF using MVVM as the flavour of MVC.

The reason for this existing is not because it doesn't already exist but I've had to look at so many implementations and come to the conclusion most are frankly not fit-for-purpose. They do the job but they make the developers life hell when trying to a new tab or modify how the tabs work. I want to understand how *I would do it* and if I could keep it to the bare minimum of functionality.

First off, I use modern versions of tooling available to any .Net (WPF) developer - eg rx.net, josn.net, autofac.

My experiences of working in the enterprise has taught me that most WPF applications are bogged down using PRISM & Unity - both in this developers opinions are shite and poor implementations of a pluggable architecture and an IoC container. PRISM really is the worst kind of software - 'a solution looking for a problem'. If I need a truely dynamic pluggable arhcitecture I probably still wouldn't use PRISM.

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



