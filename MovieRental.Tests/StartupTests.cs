using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MovieRental.MVC;
using MovieRental.MVC.Controllers;
using System.Collections.Generic;
using System;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MovieRental.Tests
{
    public class StartupTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly List<Type> _controllerTypes;

        public StartupTests(WebApplicationFactory<Startup> factory)
        {
            _controllerTypes = typeof(Startup)
               .Assembly
               .GetTypes()
               .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
               .ToList();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    _controllerTypes.ForEach(c => services.AddScoped(c));
                });
            });
        }
        [Fact]
        public void ConfigureServices_ForControllers_RegistersAllDependencies()
        {
            //arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            // assert
            _controllerTypes.ForEach(t =>
            {
                var controller = scope.ServiceProvider.GetService(t);
                controller.Should().NotBeNull();

            });
        }
    }
}
