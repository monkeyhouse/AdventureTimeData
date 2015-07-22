﻿using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

public class DependencyFactory
{
    private static IUnityContainer _container;

    /// <summary>
    /// Public reference to the unity container which will 
    /// allow the ability to register instrances or take 
    /// other actions on the container.
    /// </summary>
    public static IUnityContainer Container
    {
        get
        {
            return _container;
        }
        private set
        {
            _container = value;
        }
    }

    /// <summary>
    /// Static constructor for DependencyFactory which will 
    /// initialize the unity container.
    /// </summary>
    static DependencyFactory()
    {
        var container = new UnityContainer();

        var section = new UnityConfigurationSection();

         section.Configure(container);
  
        _container = container;
    }

    /// <summary>
    /// Resolves the type parameter T to an instance of the appropriate type.
    /// </summary>
    /// <typeparam name="T">Type of object to return</typeparam>
    public static T Resolve<T>()
    {
        T ret = default(T);

        if (Container.IsRegistered(typeof(T)))
        {
            ret = Container.Resolve<T>();
        }

        return ret;
    }
}