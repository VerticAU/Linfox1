using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace OpportunityQualification.BusinessLogic
{
    public class ContractManager
    {
        private const string _contractEntityName = "salesorder";
        private const string _contractProductEntityName = "salesorderdetail";

        private IOrganizationService _crmSvc;

        public ContractManager(IOrganizationService crmSvc)
        {
            this._crmSvc = crmSvc;
        }
         public static string ContractEntityName { get { return _contractEntityName; } }
        public Guid CreateContract(Entity sourceOpp)
        {
            var targetContract = new Entity(_contractEntityName);
            targetContract["name"] = sourceOpp.GetAttributeValue<string>("name");
            targetContract["vertic_account"]= sourceOpp.GetAttributeValue<EntityReference>("parentaccountid");
            targetContract["opportunityid"] = sourceOpp.ToEntityReference();
            targetContract["pricelevelid"] = sourceOpp.GetAttributeValue<EntityReference>("pricelevelid");
            targetContract["transactioncurrencyid"] = sourceOpp.GetAttributeValue<EntityReference>("transactioncurrencyid");
            return _crmSvc.Create(targetContract);
        }

        public void CreateProducts(IEnumerable<Entity> opportunityProducts, Guid contractId)
        {
            foreach (var iOpportunityProduct in opportunityProducts)
            {
                var contractProduct = new Entity(_contractProductEntityName);
                contractProduct["salesorderid"] = new EntityReference(_contractEntityName, contractId);
                contractProduct["productdescription"] = iOpportunityProduct.GetAttributeValue<string>("productdescription");
                contractProduct["productid"] = iOpportunityProduct.GetAttributeValue<EntityReference>("productid");
                contractProduct["uomid"] = iOpportunityProduct.GetAttributeValue<EntityReference>("uomid");
                _crmSvc.Create(contractProduct);
            }
        }
    }
}
