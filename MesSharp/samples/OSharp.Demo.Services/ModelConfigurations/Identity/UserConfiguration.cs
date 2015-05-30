

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mes.Demo.ModelConfigurations.Identity
{
    public partial class UserConfiguration
    {
        partial void UserConfigurationAppend()
        {
            HasMany(m => m.Roles).WithMany(n => n.Users);
        }
    }
}