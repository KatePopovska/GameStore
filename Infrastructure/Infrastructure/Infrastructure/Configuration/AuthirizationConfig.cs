using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class AuthorizationConfig
    {
        public string Authority { get; set; } = null!;
        public string SiteAudience { get; set; } = null!;
    }
}
