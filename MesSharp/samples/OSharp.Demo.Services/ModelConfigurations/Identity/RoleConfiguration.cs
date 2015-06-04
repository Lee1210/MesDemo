

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mes.Demo.ModelConfigurations.Identity
{
    public partial class RoleConfiguration
    {
        partial void RoleConfigurationAppend()
        {
       //     HasRequired(m => m.Organization).WithMany(n => n.Roles);
        }
    }
}