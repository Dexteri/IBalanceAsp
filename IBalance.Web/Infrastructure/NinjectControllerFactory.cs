using System;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using IBalance.Domain.Abstract;
using IBalance.Domain.Concrete;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web;

namespace IBalance.Web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            // creating container
            ninjectKernel = new StandardKernel();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(ninjectKernel);
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,
        Type controllerType)
        {
            // getting object from container
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // configuring container
            ninjectKernel.Bind<IConsignmentRepository>().To<ConsignmentRepository>();
            ninjectKernel.Bind<ICounterpartyRepository>().To<CounterpartyRepository>();
            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}