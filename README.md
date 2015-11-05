# Sphere

Sphere is an abstraction layer around data access code using a repository pattern. The purpuse of this library is mainly to add a thin layer on top of the data access code to allow you to exchange real and mock implementation and thus make testing a lot easier. Also, because is an abstraction, you could change or mix different methods to access data without to much impact in your application

# How does it work ?
Sphere has three main classes
* **Repository :** Interface defining all available methods
* **ContextRepository :** Repository implementation based on EntityFramework's DbContext
* **FakeRepository :** Mock repository using in-memory collection to store data temporarily

Aside from this classes theres a **SphereConfig** class which holds any configuration or customization you wish to add.

The repository implementations inherit from the context abstraction an implement their data access code as they see fit. If any configuration are needed, its added to the SphereConfig class.

# How do I use it?

To start using Sphere, you only need to configure the global DbContext instance in the static **SphereConfig** class. This allows the default constructor in ContextRepository to use this instance instead of passing a new one each time you want a new repository.

You can do this as follows
```C#
  protected void Application_Start()
  {
    SphereConfig.GlobalContext = new SampleContext(); //Replace SampleContext with your DbContext instance
  }
```

The best place to put this is in the application entry point. In the previous example I used the Global.asax's Application_Start() impliying I'm using a wep application but this could easly be translated to the Main() method in a Dektop application.

# How do I install it?

Sphere is available as a nuget package. You can ad it to your project running the following command in your pagackage console

```
Install-Package Sphere.Core
```

You can also find Sphere's nuget page here : https://www.nuget.org/packages/Sphere.Core

