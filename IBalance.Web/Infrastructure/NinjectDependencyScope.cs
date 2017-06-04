using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace IBalance.Web.Infrastructure
{
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot _resolver;

        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this._resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return _resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return _resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = _resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            _resolver = null;
        }
    }
}