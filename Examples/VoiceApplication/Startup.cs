﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;

using System.Net.Http.Headers;

namespace aspvoice
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) => services.AddMvc(config => {
            foreach (var formatter in config.InputFormatters)
            {
                if (formatter.GetType() == typeof(JsonInputFormatter))
                    ((JsonInputFormatter)formatter).SupportedMediaTypes.Add(
                        Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain"));
            }
        }
        ).AddWebApiConventions();


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc(routes => {
                    routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Running...!");
            });
        }
    }
}
