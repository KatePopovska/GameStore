using Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extentions
{
    public static class ConfigurationExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ClientConfig>(
                builder.Configuration.GetSection("Client"));
            builder.Services.Configure<AuthorizationConfig>(
                builder.Configuration.GetSection("Authorization"));
        }
    }
}
