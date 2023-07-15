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

        // Используйте этот метод для добавления контроллеров и представлений которые нужны в нашем веб-приложении.
        public void ConfigureServices(IServiceCollection services)
        {
            //получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("Test");

            //добавляем DatabaseContext в качестве сервиса в приложении
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(connection));

            //добавляем IdentityContext в качестве сервиса в приложении
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>() //указываем тип пользователя и роли
                .AddEntityFrameworkStores<IdentityContext>(); //работа с хранилищем данных будет идти через указанный контекст

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

        // Этот метод вызывается средой выполнения. Используйте этот метод для настройки конвейера HTTP-запросов.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection(); // Запрос позволяющий переконвертировать в HTTPS запрос

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting(); //Middlewear маршрутизатор используется для добавления шаблона маршрутизации

            app.UseAuthentication();//Подключение аутентификации(Отвечает кто зашел на сайт)
            app.UseAuthorization();//Подключение авторизации(Говорит какой пользователь и с каким именем авторизован)


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
