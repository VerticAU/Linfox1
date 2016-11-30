using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace OpportunityQualification.BusinessLogic
{
    public class OpportunityManager
    {
        private const string _opportunityEntityName = "opportunity";
        private const string _opportunityProductEntityName = "opportunityproduct";
        private IOrganizationService crmSvc;
        private Entity _opportunityEntity;

        public OpportunityManager(Entity postImage, IOrganizationService crmSvc)
        {
            this._opportunityEntity = postImage;
            this.crmSvc = crmSvc;
        }

        public void SetQualifiedContract(Entity sourceImage, Guid contractId)
        {
            var updateOpportunity = new Entity(_opportunityEntityName);
            updateOpportunity.Id = sourceImage.Id;
            updateOpportunity["vertic_qualifiedcontract"] = new EntityReference(ContractManager.ContractEntityName, contractId);
            crmSvc.Update(updateOpportunity);
        }

        public IEnumerable<Entity> RetrieveProducts()
        {
            var query = new QueryExpression(_opportunityProductEntityName);
            query.ColumnSet = new ColumnSet(true);
            var filter = new FilterExpression();
            filter.AddCondition("opportunityid", ConditionOperator.Equal, _opportunityEntity.Id);
            query.Criteria = filter;
            return crmSvc.RetrieveMultiple(query).Entities;
        }
    }
}
