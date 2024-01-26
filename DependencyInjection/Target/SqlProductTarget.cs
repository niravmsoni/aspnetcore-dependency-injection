using DependencyInjection.Model;
using DependencyInjection.Target.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Target
{
    public class SqlProductTarget : IProductTarget
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SqlProductTarget(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void AddProduct(Product product)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TargetContext>();

            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Close()
        {
            ;
        }

        public void Open()
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TargetContext>();

            context.Database.EnsureCreated();
        }
    }
}
