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
                //burada yaz�lan kodlar bizim microsoftun identitiy yap�s�n� ekledi�imiz projemzde kullan�c� kay�t olurken �ifrelerinde otomatik olarak
                //b�y�k k���k harf ve rakamlardan olu�ma �art� getiriyor biz ise bu �art� burada yazd���m�z kodlaral kald�r�yoruz
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<BlogContext>();

            services.AddControllersWithViews();

            ////bu kod kullan�c�n�n oturum a�mas�nda kullan�l�r
            services.AddSession();

            //bu kod projedeki sayfalara oturum a�madan eri�meyi engeller
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //bu kod sisteme giri� giri� yap�larak eri�ilebilecek olan sayfalara , giri� yapmadan eri�mek istersek bizi do�rudan login sayfas�na y�nlendirir. 
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => { x.LoginPath = "/Login/Index"; });


            //bu kod eri�im olmayan sayfalara gitmek isteyen kullan�c�lar� bir uyar� sayfas�na y�nlendirmek i�in �al���r
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

            //bu kod projenin �al��mas� s�ras�nda olu�abilcek herhangi bir hata koduna kar��n bizi bir error sayfas�na y�nlendiriyor
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //kullan�c�n�n oturum a�mas� i�in gerekli
            app.UseSession();

            //�dentity kullanarak sisteme login olurken kulland�k
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //olu�turdu�umuz area y� �al��t�rmak i�in b�yle bir endpoind tan�mlad�k bu yap�y� area olu�unda sistem bize kendi scaffoldingReadme.txt diye bir dosyaya yaz�yor ordan kopyala yap��t�rla buraya yaz�yoruz
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
