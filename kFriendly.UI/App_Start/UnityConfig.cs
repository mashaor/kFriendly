using kFriendly.Core.Interfaces;
using kFriendly.Infrastructure.Data;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace kFriendly.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            container.RegisterType<IQueryBusiness, ApiQueryBusiness>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));



            //IUnityContainer container = new UnityContainer();
            //container.RegisterType<IFoo, Foo1>("Foo1");
            //container.RegisterType<IFoo, Foo2>("Foo2");

            //container.RegisterType<Client1>(new InjectionConstructor(new ResolvedParameter<IFoo>("Foo1")));
            //container.RegisterType<Client2>(new InjectionConstructor(new ResolvedParameter<IFoo>("Foo2")));

            //Client1 client1 = container.Resolve<Client1>();
            //Client2 client2 = container.Resolve<Client2>();
        }
    }
}