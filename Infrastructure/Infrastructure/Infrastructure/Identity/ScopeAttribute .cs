using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ScopeAttribute : Attribute
    {
        public ScopeAttribute(string scopeName)
        {
            ScopeName = scopeName;
        }
        public string ScopeName { get; }
    }
}
