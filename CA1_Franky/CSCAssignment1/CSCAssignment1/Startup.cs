using System;
using System.Collections.Generic;
using System.Linq;
using CSCAssignment1.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CSCAssignment1.Startup))]

namespace CSCAssignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
