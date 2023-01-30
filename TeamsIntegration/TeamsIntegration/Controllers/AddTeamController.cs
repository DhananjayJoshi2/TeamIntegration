using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace TeamsIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddTeamController : ControllerBase
    {
        readonly IConfiguration _configuration;

        public AddTeamController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       // [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddTeams()
        {
            //var options = new TokenCredentialOptions
            //{
            //    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            //};
            //var authProvider = new AuthorizationCodeCredential(
            //"common", "abc", "abcd", "auth", options);
            
                var ClientId = _configuration.GetValue<String>("AzureAD:ClientId");
                var TenantId = _configuration.GetValue<String>("AzureAD:TenantId");
                var ClientSecret = _configuration.GetValue<String>("AzureAD:ClientSecret");

                var authProvider = new ClientSecretCredential(TenantId, ClientId, ClientSecret);

            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var team = new Team
            {
                DisplayName = "My Team",
                Description = "My Sample Team’s Description",
                AdditionalData = new Dictionary<string, object>()
                {
                    {"template@odata.bind", "https://graph.microsoft.com/v1.0/teamsTemplates('standard')"}
                }
            };

            // 
            //            {
            //                "template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
            //    "displayName": "Architecture Team",
            //    "description": "The team for those in architecture design."
            //}

            await graphClient.Teams
                .Request()
                .AddAsync(team);

            return Ok(team);
        }
    }
}
