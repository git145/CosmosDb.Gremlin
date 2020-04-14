using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using CosmosDb.Gremlin.Domain.Services;
using CosmosDb.Gremlin.Core.Models.Configuration;
using CosmosDb.Gremlin.Core.Interfaces.Models.Configuration;
using CosmosDb.Gremlin.Core.Interfaces.Services;

namespace CosmosDb.Gremlin
{
    public class Startup
    {
        private IConfiguration _configuration;

        private IGremlinConfigurationModel _gremlinConfigurationModel = new GremlinConfigurationModel();

        public Startup()
        {
            SetCurrentDirectory();

            SetConfiguration();
        }

        private void SetCurrentDirectory()
        {
            string pathToExe = Process.GetCurrentProcess().MainModule.FileName;

            string pathToContentRoot = Path.GetDirectoryName(pathToExe);

            Directory.SetCurrentDirectory(pathToContentRoot);
        }

        private void SetConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile(@"appsettings.json");

            AddFixedConfiguration(configurationBuilder);

            _configuration = configurationBuilder.Build();
        }

        private void AddFixedConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile(@"Configuration\GremlinConfiguration.json");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers().AddNewtonsoftJson();

            AddConfiguration(services);

            AddServices(services);
        }

        private void AddConfiguration(IServiceCollection services)
        {
            services.AddSingleton(_configuration);

            AddGremlinConfiguration(services);
        }

        private void AddGremlinConfiguration(IServiceCollection services)
        {
            _configuration.GetSection("Gremlin").Bind(_gremlinConfigurationModel);

            services.AddSingleton(_gremlinConfigurationModel);
        }

        private void AddServices(IServiceCollection services)
        {
            AddGremlinService(services);
        }

        private void AddGremlinService(IServiceCollection services)
        {
            IGremlinService gremlinService = new GremlinService(_gremlinConfigurationModel);

            services.AddSingleton(gremlinService);
        }

        public void Configure(IApplicationBuilder app,
            IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(bldr => bldr.WithOrigins("http://localhost:8080")
                .WithMethods("GET", "POST")
                .AllowAnyHeader());

            WebSocketOptions webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };

            app.UseWebSockets(webSocketOptions);

            app.UseFileServer();

            app.UseMvc();
        }
    }
}
