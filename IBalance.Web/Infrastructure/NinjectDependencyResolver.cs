using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace IBalance.Web.Infrastructure
{
    // This class is the resolver, but it is also the global scope
    // so we derive from NinjectScope.
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel) : base(kernel)
        {
            this._kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }
}