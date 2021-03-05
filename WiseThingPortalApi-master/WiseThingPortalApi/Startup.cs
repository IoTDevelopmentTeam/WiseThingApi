using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WiseThing.Data.Respository;
using WiseThing.Portal.Business;
using AutoMapper;

namespace WiseThingPortalApi
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
            var connStr = Configuration.GetConnectionString("PortalDbConnection");
            services.AddDbContextPool<WisethingPortalContext>(options => options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IUserHandler, UserHandler>();
            services.AddTransient<IDeviceHandler, DeviceHandler>();
            services.AddTransient<IPaneHandler, PaneHandler>();
            services.AddTransient<ISecurityQuestionHandler, SecurityQuestionHandler>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserDeviceRepository, UserDeviceRepository>();
            services.AddTransient<IPaneRepository, PaneRepository>();
            services.AddTransient<ISecurityQuestionRepository, SecurityQuestionRepository>();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    builder =>
                    {
                        //builder.WithOrigins("http://wisethingportalbucket.s3-website.us-east-2.amazonaws.com").AllowAnyHeader().AllowAnyMethod();
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
