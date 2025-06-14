using GameGather.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Infrastructure.Database.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(GameGatherDbContext context)
        {


            
            await PostGameSeeder.SeedAsync(context);



            await context.SaveChangesAsync();
        }
    }
}
