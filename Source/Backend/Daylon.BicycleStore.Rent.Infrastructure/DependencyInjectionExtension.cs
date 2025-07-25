﻿using Daylon.BicycleStore.Rent.Domain.Repositories;
using Daylon.BicycleStore.Rent.Infrastructure.DataAccess;
using Daylon.BicycleStore.Rent.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Daylon.BicycleStore.Rent.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            if (string.IsNullOrEmpty(connectionString))
            { throw new Exception("Connection string is not configured."); }

            services.AddDbContext<BicycleStoreDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IBicycleRepository, BicycleRepository>();
        }
    }
}
