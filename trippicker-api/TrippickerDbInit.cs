using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace trippicker_api {
    public static class TrippickerDbInit
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TrippickerDbContext>();

            context.Database.Migrate();

            if (context.ChangeTracker.HasChanges())
		    {
                context.SaveChanges();
            }
        }
    }
}