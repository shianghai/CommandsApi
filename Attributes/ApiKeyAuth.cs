using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace CommandsApi.Attributes
{
   

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class ApiKeyAuth : Attribute, IAuthorizationFilter
    {
        private const string API_KEY_HEADER_NAME = "X-API-Key";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = GetApiKey(context.HttpContext);
            var submittedApiKey = GetApiKeyFromRequest(context.HttpContext);

            if (!IsApiKeyValid(apiKey, submittedApiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private static string GetApiKeyFromRequest(HttpContext context)
        {
            return context.Request.Headers[API_KEY_HEADER_NAME];
        }

        private static string GetApiKey(HttpContext context)
        {
            var configuration = context.RequestServices.GetService<IConfiguration>();

            return configuration.GetValue<string>($"ApiKey");
        }

        private static bool IsApiKeyValid(string apiKey, string apiKeyFromRequest)
        {
            if (string.IsNullOrEmpty(apiKeyFromRequest)) return false;

            var apiKeySpan = MemoryMarshal.Cast<char, byte>(apiKey.AsSpan());

            var submittedApiKeySpan = MemoryMarshal.Cast<char, byte>(apiKeyFromRequest.AsSpan());

            return CryptographicOperations.FixedTimeEquals(apiKeySpan, submittedApiKeySpan);
        }
    }
}
