using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using diplom.Extensions;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Repository;
using Repository.DataShaping;

namespace diplom
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
                "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers(config => { 
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true;
                }) .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters()
                .AddCustomCSVFormatter();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateCompanyExistsAttribute>();
            services.AddScoped<ValidateEmployeeForCompanyExistsAttribute>();
            services.AddScoped<ValidateClientExistsAttribute>();
            services.AddScoped<ValidateOrderForClientExistsAttribute>();
            services.AddScoped <IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>(); 
            
            services.ConfigureVersioning();
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Company, CompanyDto>()
                    .ForMember(c => c.FullAddress,
                        
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
                CreateMap<Employee, EmployeeDto>();
                CreateMap<CompanyForCreationDto, Company>();
                CreateMap<EmployeeForCreationDto, Employee>();
                CreateMap<EmployeeForUpdateDto, Employee>();
                CreateMap<CompanyForUpdateDto, Company>();
                CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap(); 
                
                
                CreateMap<Client, ClientDto>()
                    .ForMember(c => c.AddressAge,
                        
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Age)));
                CreateMap<Order, OrderDto>();
                CreateMap<ClientForCreationDto, Client>();
                CreateMap<OrderForCreationDto, Order>();
                CreateMap<OrderForUpdateDto, Order>();
                CreateMap<ClientForUpdateDto, Client>();
                CreateMap<OrderForUpdateDto, Order>(); 
                CreateMap<OrderForUpdateDto, Order>().ReverseMap(); 
                
                CreateMap<UserForRegistrationDto, User>();







            }
        }
    }
}
