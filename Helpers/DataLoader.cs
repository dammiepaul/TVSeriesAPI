using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TVSeriesAPI.Helpers
{
    public static class DataLoader
    {
        public static void PrePopulate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                MigrateDBChangesAndSeedData(serviceScope.ServiceProvider.GetService<MovieSeriesContext>());
            }
        }

        public static void MigrateDBChangesAndSeedData(MovieSeriesContext context)
        {
            Console.WriteLine("APPLYING MIGRATIONS AND SEEDING DATA...");

            context.Database.Migrate();

            Console.WriteLine("MIGRATION AND SEEDING COMPLETED SUCCESSFULLY.");
        }
    }
}
