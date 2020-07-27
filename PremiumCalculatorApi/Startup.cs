using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PremiumCalculatorApi.Data.Model;
using PremiumCalculatorApi.Data.Repository;

namespace PremiumCalculatorApi
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
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PremiumCalculatorDb"));
            });

            //Setting the allowed CORS domains
            services.AddCors(o => o.AddPolicy("AllowWebOrigins", builder =>
            {
                //Allowed domains
                builder.WithOrigins("https://localhost:44328")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddControllers();

            //Setting interface implementation
            //MockPremiumRuleRepository for In Memory implementation
            //DbPremiumRuleRepository for SQL implementation
            //services.AddTransient<IPremiumRuleRepository, DbPremiumRuleRepository>();
            services.AddTransient<IPremiumRuleRepository, MockPremiumRuleRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Using the CORS policy
            app.UseCors("AllowWebOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
