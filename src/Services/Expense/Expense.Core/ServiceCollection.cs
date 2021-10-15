using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Expense.Core
{
    public static class CoreServiceCollection
    {
        public static IServiceCollection AddCoreServiceCollection(this IServiceCollection services)
            => services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}