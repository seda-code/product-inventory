using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Inventory.API.Models;
using RestSharp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // https://localhost:5001/api/token
    public class TokenController
    {
        private readonly ILogger<TokenController> logger;
        private readonly AppSettings appsettings;
        private readonly Endpoints endpoints;

        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly RestClient restClient = new RestClient() { Timeout = -1 };

        public TokenController(ILogger<TokenController> logger,
                               IOptions<Endpoints> endpointsOptions,
                               IOptions<AppSettings> appsettingsOptions)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            appsettings = appsettingsOptions.Value ?? throw new ArgumentNullException(nameof(AppSettings));
            endpoints = endpointsOptions.Value ?? throw new ArgumentNullException(nameof(Endpoints));
        }

        [HttpPost]
        public ActionResult AddProduct([FromQuery] string code)
        {
            Console.WriteLine(code);
            return new OkObjectResult(code);
        }

        [HttpGet]
        [Route("[action]")]
        public RedirectResult Login()
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                return new RedirectResult(@"https://github.com/login/oauth/authorize?client_id=" + appsettings.ClientId);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, null);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string code)
        {
            User someResult = default;
            try
            {
                var accessToken = await GetAccessTokenAsync(code);
                if (!string.IsNullOrEmpty(accessToken.AccessToken))
                    someResult = await GetAuthorizedQuery(accessToken.AccessToken);

                return new OkObjectResult(someResult);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, null);
                throw;
            }
        }

        private async Task<User> GetAuthorizedQuery(string accessToken)
        {
            User result = default;
            try
            {
                //result = await GetHttpClientQuery(accessToken);
                result = await GetRestSharpClientQuery(accessToken);
            }
            catch (JsonException exception)
            {
                logger.LogError(exception.Message, null);
            }
            catch (HttpRequestException exception)
            {
                logger.LogError(exception.Message, null);
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, null);
            }

            return result;
        }

        private async Task<User> GetRestSharpClientQuery(string accessToken)
        {
            restClient.BaseUrl = new Uri(endpoints.GithubUserProfileUrl);
            var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", $"Bearer {accessToken}"); //FUNCIONA TAMBIEN!!!
            request.AddHeader("Authorization", $"token {accessToken}");
            IRestResponse response = await restClient.ExecuteAsync(request);

            var currentUserProfile = JsonConvert.DeserializeObject<User>(response.Content);

            return currentUserProfile;

        }
        //[Obsolete("Do not use as it gives 403")]
        ////https://github.com/dotnet/runtime/issues/26475
        //private async Task<string> GetHttpClientQuery(string accessToken)
        //{
        //    string result = string.Empty;

        //    //httpClient.DefaultRequestHeaders.Accept.Clear();
        //    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(appsettings.MediaTypeGitHubJson));
        //    ////httpClient.DefaultRequestHeaders.Add("Authorization", $"token {accessToken}");
        //    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", accessToken);

        //    ////httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        //    //using (HttpResponseMessage response = await httpClient.GetAsync(new Uri(endpoints.GithubUserProfileUrl)))
        //    //{
        //    //    HttpStatusCode status = response.StatusCode;
        //    //    response.EnsureSuccessStatusCode();

        //    //    using HttpContent content = response.Content;
        //    //    result = await content.ReadAsStringAsync();
        //    //}

        //    return result;
        //}
        private async Task<Token> GetAccessTokenAsync(string code)
        {
            Token accessToken = default;
            Uri accessUri = new Uri(endpoints.AccessTokenUrl);

            var parameters = new Dictionary<string, string> {
                                                                { "client_id",  appsettings.ClientId},
                                                                { "client_secret", appsettings.ClientSecret} ,
                                                                { "code", code }
                                                            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(appsettings.MediaTypeJson));

            using (HttpResponseMessage response = await httpClient.PostAsync(accessUri, new FormUrlEncodedContent(parameters)))
            {
                HttpStatusCode status = response.StatusCode;
                response.EnsureSuccessStatusCode();
                using HttpContent content = response.Content;
                accessToken = await content.ReadAsAsync<Token>();
            }
            return accessToken;
        }
    }
}