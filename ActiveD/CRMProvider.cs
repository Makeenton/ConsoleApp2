using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;


namespace ActiveD
{
    public class CRMProvider
    {
        public void CRMConnect()
        {
            ClientCredentials credentials = new ClientCredentials();


            credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            Uri uri = new Uri("http://nskdccrm.alventa.ru/alventa/xrmservices/2011/Organization.svc");

            OrganizationServiceProxy proxy = new OrganizationServiceProxy(uri, null, credentials, null);
            proxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());
            IOrganizationService service = (IOrganizationService)proxy;


            Entity retrievedAccount = service.Retrieve("systemuser", new Guid("14E25FE5-73E2-E111-B2D5-000C29CDB72E"), new ColumnSet(allColumns: true));

        }

    }
}
