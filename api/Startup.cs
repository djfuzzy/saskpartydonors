using System;
using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaskPartyDonors.Data;
using SaskPartyDonors.Extensions;
using SaskPartyDonors.Services.Contributions;
using SaskPartyDonors.Services.Importers;
using SaskPartyDonors.Services.Recipients;

namespace SaskPartyDonors
{
  public class Startup
    {
        private const string _defaultCorsPolicyName = "DefaultCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            Configuration.GetValue<string>("App:CorsOrigins")
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

            services.AddDbContext<SaskPartyDonorsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddTransient<AirtableCsvImporter>();
            services.AddTransient<SaskCsvImporter>();
            services.AddTransient<IContributionService, ContributionService>();
            services.AddTransient<IRecipientService, RecipientService>();

            services.AddSwaggerDocument(settings =>
            {
                settings.FlattenInheritanceHierarchy = true;
                settings.Title = "SaskPartyDonors API";
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_defaultCorsPolicyName);

            app.UseAuthorization();

            app.UseOpenApi();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
