using CommonFunctions;
using Microsoft.Xrm.Sdk;
using OpportunityQualification.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunityQualification.Plugins
{
    //Event: Update
    public class OpportunityWinPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetContext();
            var targetOpp = context.target();
            if (!IsValidEvent(targetOpp))
                return;
            var postImage = context.postimage();
            var crmSvc = serviceProvider.CreateOrganizationService(context.UserId);

            var opportunityManager = new OpportunityManager(postImage, crmSvc);
            var contractManager = new ContractManager(crmSvc);
            var contractId = contractManager.CreateContract(postImage);
            opportunityManager.SetQualifiedContract(postImage,contractId);
            var opportunityProducts = opportunityManager.RetrieveProducts();
            contractManager.CreateProducts(opportunityProducts, contractId);
        }

        private bool IsValidEvent(Entity targetOpp)
        {
            if (targetOpp.LogicalName != Constants.Opportunity)
                return false;
            if (!targetOpp.ContainsNotNull("statecode"))
                return false;
            if (targetOpp.GetAttributeValue<OptionSetValue>("statecode").Value != (int)OpportunityState.Won)
                return false;
            return true;
        }
    }
}
