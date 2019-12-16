using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Lib.AspNetCore.Mvc.JqGrid.Core.Request;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Hosting;

namespace Demo.AspNetCore.JqGrid
{
    public class Startup
    {
         public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                //Set For Antiforgery Post Method
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.AllowEmptyInputInBodyModelBinding = true;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
                jsonOptions.JsonSerializerOptions.WriteIndented = true;
            }).AddNewtonsoftJson();
            
            services.AddRazorPages();
        }

          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            JqGridRequest.ParametersNames = new JqGridParametersNames() { PagesCount = "npage" };
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=JavaScript}/{action=Basics}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
