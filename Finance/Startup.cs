﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Finance.Startup))]
namespace Finance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new CommonStartup().ConfigureAuth(app);
        }
    }
}
