﻿using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(PhotoMosaicMVC.Areas.Identity.IdentityHostingStartup))]
namespace PhotoMosaicMVC.Areas.Identity
    {
        public class IdentityHostingStartup : IHostingStartup
        {
            public void Configure(IWebHostBuilder builder)
            {
                builder.ConfigureServices((context, services) => {
                });
            }
        }
    }
