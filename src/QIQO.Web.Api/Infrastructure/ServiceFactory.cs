﻿using Core;
using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Web.Api
{
    public class ServiceFactory : IServiceFactory
    {
        private IServiceCollection _services;

        public ServiceFactory(IServiceCollection services)
        {
            _services = services;
        }

        public T CreateClient<T>() where T : IServiceContract
        {
            var p = _services.BuildServiceProvider();
            return p.GetService<T>();
        }
    }
}
