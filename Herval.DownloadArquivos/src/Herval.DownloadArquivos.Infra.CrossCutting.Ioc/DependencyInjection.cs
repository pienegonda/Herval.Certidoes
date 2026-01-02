using Herval.Notifications.Contexts;
using Herval.Notifications.Interfaces;
using Herval.DownloadArquivos.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Herval.DownloadArquivos.Infra.CrossCutting.Ioc
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services, Assembly apiAssembly)
        {
            // Domain Services
            
            // Notifications
            services.AddScoped<INotificationContext, NotificationContext>();

            // Repositories
            //services.AddScoped<IFakeRepository, FakeRepository>();

            // Database Contexts
            services.AddDbContext<Context>();
        }
    }
}

