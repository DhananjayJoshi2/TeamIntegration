using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddTeamController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AddTeamController(IConfiguration _configuration)
        {
            _configuration = _configuration;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> AddTeams()
        {
            //var options = new TokenCredentialOptions
            //{
            //    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            //};
            //var authProvider = new AuthorizationCodeCredential(
            //"common", "abc", "abcd", "auth", options);

            var clientID = _configuration.GetValue<string>("AzureAD:ClientId");
            var TenantId = _configuration.GetValue<string>("AzureAD:TenantId");
            var ClientSecret = _configuration.GetValue<string>("AzureAD:ClientSecret");

            var authProvider = new ClientSecretCredential(TenantId, clientID, ClientSecret);

            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var team = new Team
            {
                DisplayName = "My Sample",
                Description = "My Sample Team’s Description",
                AdditionalData = new Dictionary<string, object>()
    {
                {"template@odata.bind", "https://graph.microsoft.com/v1.0/teamsTemplates('standard')"}
    }
            };

            await graphClient.Teams
                .Request()
                .AddAsync(team);

            return Ok(team);
        }
    }

    //[HttpPost]
    //public async Task<ActionResult> GetTeam()
    //{
        
    //}

}
