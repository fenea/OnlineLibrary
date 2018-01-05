using Business;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Swashbuckle.AspNetCore.Swagger;


namespace Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment _env;
        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<DatabaseContext>(options =>options.UseSqlite("Data Source=MvcBooks.sqlite",x=>x.MigrationsAssembly(("Presentation"))));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();


            //secure conn

            services.AddMvc(opt =>
            {
                if (_env.IsProduction())
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }

            });
               
           
        
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

           

            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddTransient<IBookRepository, BookRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseAuthentication();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller="Account",action="Login" });

               
            });
        }
    }
}
