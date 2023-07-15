using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ����������� ���� ����� ��� ���������� ������������ � ������������� ������� ����� � ����� ���-����������.
        public void ConfigureServices(IServiceCollection services)
        {
            //�������� ������ ����������� �� ����� ������������
            string connection = Configuration.GetConnectionString("Test");

            //��������� DatabaseContext � �������� ������� � ����������
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(connection));

            //��������� IdentityContext � �������� ������� � ����������
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>() //��������� ��� ������������ � ����
                .AddEntityFrameworkStores<IdentityContext>(); //������ � ���������� ������ ����� ���� ����� ��������� ��������

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true
                };
            });


            services.AddTransient<IOrdersRepository, OrdersDbRepository>();
            services.AddTransient<IProductsRepository, ProductsDbRepository>();
            services.AddTransient<ICartsRepository, CartsDbRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteDbRepository>();
            services.AddSingleton<IRolesRepository, RolesInMemoryRepository>();
            services.AddSingleton<IUsersManager, UsersManager>();
            services.AddControllersWithViews();
        }

        // ���� ����� ���������� ������ ����������. ����������� ���� ����� ��� ��������� ��������� HTTP-��������.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection(); // ������ ����������� ������������������ � HTTPS ������

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting(); //Middlewear ������������� ������������ ��� ���������� ������� �������������

            app.UseAuthentication();//����������� ��������������(�������� ��� ����� �� ����)
            app.UseAuthorization();//����������� �����������(������� ����� ������������ � � ����� ������ �����������)


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Area",
                    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
