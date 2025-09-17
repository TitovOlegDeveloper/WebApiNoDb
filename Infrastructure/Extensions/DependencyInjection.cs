using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            // Регистрируем InMemory репозиторий как Singleton (данные сохраняются в памяти)
            services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

            // Регистрируем сервисы
            //services.AddSingleton<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}
