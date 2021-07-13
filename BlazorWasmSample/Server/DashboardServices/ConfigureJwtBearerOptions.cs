using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.DashboardServices {
    public class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions> {
        public void PostConfigure(string name, JwtBearerOptions options) {
            var originalOnMessageReceived = options.Events.OnMessageReceived;
            options.Events.OnMessageReceived = async context => {
                await originalOnMessageReceived(context);

                if(string.IsNullOrEmpty(context.Token) && context.Request.HasFormContentType) {
                    var formData = await context.Request.ReadFormAsync();
                    var accessToken = formData?["Authorization"];
                    var path = context.HttpContext.Request.Path;

                    if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/dashboardControl")) {
                        context.Token = accessToken;
                    }
                }
            };
        }
    }
}
