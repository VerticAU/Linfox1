using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFunctions
{
    public static class CrmExtensions
    {
        public static IPluginExecutionContext GetContext(this IServiceProvider serviceProvider)
        {
            return (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
        }
        public static Entity target(this IPluginExecutionContext context)
        {
            return (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity) ?
                ((Entity)context.InputParameters["Target"])
                : null;
        }
        public static bool ContainsNotNull(this Entity e, string fieldName)
        {
            return e.Contains(fieldName) && e[fieldName] != null;
        }
        public static IOrganizationService CreateOrganizationService(this IServiceProvider serviceProvider, Guid? userId)
        {
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            return serviceFactory.CreateOrganizationService(userId);
        }
        public static Entity postimage(this IPluginExecutionContext context)
        {
            if (context.PostEntityImages.Contains("postimage"))
                return (Entity)context.PostEntityImages["postimage"];
            else if (context.PostEntityImages.Contains(context.PrimaryEntityName))
                return (Entity)context.PostEntityImages[context.PrimaryEntityName];
            else
                return null;
        }
    }
}
