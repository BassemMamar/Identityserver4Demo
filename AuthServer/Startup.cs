using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Test;
using Microsoft.Extensions.Logging;

namespace AuthServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                 //.AddSigningCredential(new X509Certificate2(@"C:\TempApps\PersonalCertificateForDemos.pfx","123456"))
                .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
                .AddInMemoryClients(InMemoryConfiguration.GetClients())
                .AddTestUsers(InMemoryConfiguration.GetUsers());

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    // IdentityServer configures a cookie handler specifically for the results of external authentication 
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // next info for demo
                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();

            loggerFactory.AddConsole();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
