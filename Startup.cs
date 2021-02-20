using System;
using FiledCom.Data;
using FiledCom.Dtos;
using FiledCom.ExternalServices;
using FiledCom.Services;
using FiledCom.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace FiledCom
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
            // services.AddDbContext<PaymentContext>(opt => {
            //     opt.UseInMemoryDatabase("FiledComDB");
            // });

            services.AddDbContext<PaymentContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("FiledComConnection"));
            });

            services.AddControllers().AddNewtonsoftJson(s => 
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddMvc(setup => {
            }).AddFluentValidation();
            services.AddTransient<IValidator<PaymentDto>, PaymentValidator>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // services.AddScoped<IPaymentRepo, InMemoryPaymentRepo>();
            services.AddScoped<IPaymentRepo, SqlPaymentRepo>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<IPaymentGateway, PremiumPaymentService>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
