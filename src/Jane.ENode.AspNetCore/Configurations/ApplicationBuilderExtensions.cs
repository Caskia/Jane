using Microsoft.AspNetCore.Builder;
using Jane.ENode;

namespace Jane.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseJaneENode(this IApplicationBuilder app)
        {
            app.ApplicationServices.PopulateENodeDIContainer();

            app.UseJane();
        }
    }
}