using DataAccess.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BlogContext>();
            services.AddIdentity<AppUser, AppRole>(x=> 
            {
                //burada yazýlan kodlar bizim microsoftun identitiy yapýsýný eklediðimiz projemzde kullanýcý kayýt olurken þifrelerinde otomatik olarak
                //büyük küçük harf ve rakamlardan oluþma þartý getiriyor biz ise bu þartý burada yazdýðýmýz kodlaral kaldýrýyoruz
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<BlogContext>();

            services.AddControllersWithViews();

            ////bu kod kullanýcýnýn oturum açmasýnda kullanýlýr
            services.AddSession();

            //bu kod projedeki sayfalara oturum açmadan eriþmeyi engeller
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //bu kod sisteme giriþ giriþ yapýlarak eriþilebilecek olan sayfalara , giriþ yapmadan eriþmek istersek bizi doðrudan login sayfasýna yönlendirir. 
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => { x.LoginPath = "/Login/Index"; });


            //bu kod eriþim olmayan sayfalara gitmek isteyen kullanýcýlarý bir uyarý sayfasýna yönlendirmek için çalýþýr
            //services.ConfigureApplicationCookie(options  =>
            //{
            //    options.AccessDeniedPath = new PathString("/ErrorPage/inde.html");
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //bu kod projenin çalýþmasý sýrasýnda oluþabilcek herhangi bir hata koduna karþýn bizi bir error sayfasýna yönlendiriyor
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //kullanýcýnýn oturum açmasý için gerekli
            app.UseSession();

            //ýdentity kullanarak sisteme login olurken kullandýk
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //oluþturduðumuz area yý çalýþtýrmak için böyle bir endpoind tanýmladýk bu yapýyý area oluþunda sistem bize kendi scaffoldingReadme.txt diye bir dosyaya yazýyor ordan kopyala yapýþtýrla buraya yazýyoruz
                  endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
