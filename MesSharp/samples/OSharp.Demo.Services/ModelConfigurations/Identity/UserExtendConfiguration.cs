

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mes.Demo.ModelConfigurations.Identity
{
    public partial class UserExtendConfiguration
    {
        partial void UserExtendConfigurationAppend()
        {
            HasRequired(m => m.User).WithRequiredDependent(n => n.Extend);
        }
    }
}