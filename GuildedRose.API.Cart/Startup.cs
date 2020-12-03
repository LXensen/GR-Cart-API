using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using GuildedRose.API.Cart.Models;
using GuildedRose.API.Cart.Interfaces;
using GuildedRose.API.Cart.Services;

namespace GuildedRose.API.Cart
{
    public class Startup
    {
        readonly string alloworiginLocalhost = "localhostOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: alloworiginLocalhost,
                        builder =>
                        {
                            builder.AllowAnyMethod()
                            .AllowAnyOrigin()
                            .AllowAnyHeader();
                        });
            });
            services.AddControllers();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddDbContext<CartContext>(opt => opt.UseInMemoryDatabase("CartItems"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GuildedRose.API.Cart", Version = "v1" });
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GuildedRose.API.Cart v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(alloworiginLocalhost);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
