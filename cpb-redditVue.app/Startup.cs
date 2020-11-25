using cpb_redditVue.dal.Interfaces;
using cpb_redditVue.dal.Models;
using cpb_redditVue.dal.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using VueCliMiddleware;

namespace cpb_redditVue.app
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
            services.AddAuthentication(options => 
            { 
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/signin";
                    options.LogoutPath = "/signout";
                })
                .AddReddit(options =>
                {
                    options.ClientId = "qlglngOuasiAyQ";
                    options.ClientSecret = "6UE9A02Yo0HNo-J7npyx-Ruc0zpNKA";
                    options.Scope.Clear();

                    new List<string> { "identity", "mysubreddits", "read" }
                    .ForEach(options.Scope.Add);

                    options.SaveTokens = true;
                });

            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });

            services.AddOptions();

            services.Configure<RedditConfig>(Configuration.GetSection("RedditConfig"));

            services.AddHttpContextAccessor();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ISubredditService, SubredditService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });
        }
    }
}
