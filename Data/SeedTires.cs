using TestIgnatov.Data.Seeds;
using TireDrift.Data.Seeds;

namespace TireDrift.Data
{
    public static class SeedTires
    {
        public static async Task Seed(IApplicationBuilder app)
        {
            await SeedRoles.Seed(app);
            await SeedServices.Seed(app);
        }
    }
}