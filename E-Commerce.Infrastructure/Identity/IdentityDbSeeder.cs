using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity
{
    /*
     3. IdentityDbSeeder

    Instead of calling every seeder manually:

    await roleSeeder.SeedAsync();
    await permissionSeeder.SeedAsync();
    await adminSeeder.SeedAsync();

    you create one coordinator.

    public class IdentityDbSeeder
    {
        public async Task SeedAsync()
        {
            await SeedRoles();
            await SeedPermissions();
            await SeedAdminUser();
        }
    }

    Then in Program.cs:

    await app.SeedIdentityAsync();
     */
    public class IdentityDbSeeder
    {


    }
}
