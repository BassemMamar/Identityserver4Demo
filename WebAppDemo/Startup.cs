using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //we’ve turned off the JWT claim type mapping to allow well-known claims (e.g. ‘sub’ and ‘idp’) 
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options => // adds the authentication services to DI
            {
                options.DefaultScheme = "Cookies";// using a cookie as the primary means to authenticate a user 
                options.DefaultChallengeScheme = "oidc";// because when we need the user to login, we will be using the OpenID Connect scheme
            })
                .AddCookie("Cookies")// add the handler that can process cookies
                .AddOpenIdConnect("oidc", options => // configure the handler that perform the OpenID Connect protocol
                {
                    options.SignInScheme = "Cookies";// issue a cookie using the cookie handler once the OpenID Connect protocol is complete

                    options.Authority = "http://localhost:5000"; // indicates that we are trusting IdentityServer
                    options.RequireHttpsMetadata = false; // not using https 
                    options.Resource= "openid";
                    options.ClientId = "MVC";// identity this client
                    options.SaveTokens = true;// persist the tokens from IdentityServer in the cookie 
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
