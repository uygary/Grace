﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grace.DependencyInjection;
using Grace.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Grace.AspNetCore.Hosting
{
    /// <summary>
    /// C# extensions for adding Grace as service container
    /// </summary>
    public static class GraceServiceProviderExtensions
    {
        /// <summary>
        /// Add Grace DI container
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="configuration"></param>
        public static void AddGrace(this IServiceCollection collection, IInjectionScopeConfiguration configuration = null)
        {
            collection.AddSingleton<IServiceProviderFactory<DependencyInjectionContainer>>(new GraceServiceProviderFactory(configuration));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static void UseGrace(this IWebHostBuilder builder, IInjectionScopeConfiguration configuration = null)
        {
            builder.ConfigureServices(c => c.AddGrace(configuration));
        }

        private class GraceServiceProviderFactory : IServiceProviderFactory<DependencyInjectionContainer>
        {
            private readonly IInjectionScopeConfiguration _configuration;

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="configuration"></param>
            public GraceServiceProviderFactory(IInjectionScopeConfiguration configuration)
            {
                _configuration = configuration ?? new InjectionScopeConfiguration();
            }

            /// <summary>
            /// Creates a container builder from an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
            /// </summary>
            /// <param name="services">The collection of services</param>
            /// <returns>A container builder that can be used to create an <see cref="T:System.IServiceProvider" />.</returns>
            public DependencyInjectionContainer CreateBuilder(IServiceCollection services)
            {
                return new DependencyInjectionContainer(_configuration);
            }

            /// <summary>
            /// Creates an <see cref="T:System.IServiceProvider" /> from the container builder.
            /// </summary>
            /// <param name="containerBuilder">The container builder</param>
            /// <returns>An <see cref="T:System.IServiceProvider" /></returns>
            public IServiceProvider CreateServiceProvider(DependencyInjectionContainer containerBuilder)
            {
                return containerBuilder.Populate(new ServiceDescriptor[0]);
            }
        }
    }
}