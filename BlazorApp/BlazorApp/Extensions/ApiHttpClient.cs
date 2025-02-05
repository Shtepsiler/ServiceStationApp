using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace BlazorApp.Extensions
{
    public class ApiHttpClient
    {
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private HttpClient httpClient;
        private readonly NavigationManager navigationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieContainer cookieContainer;

        public ApiHttpClient(NavigationManager navigationManager, IHttpContextAccessor httpContextAccessor)
        {
            this.navigationManager = navigationManager;
            _httpContextAccessor = httpContextAccessor;
            cookieContainer = new CookieContainer();

            var handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer
            };
            httpClient = new HttpClient(handler);
  
        }

        public ApiHttpClient SetHttpClient(HttpClient client)
        {
            httpClient.BaseAddress = client.BaseAddress;

            AddRefererHeader();
            return this;
        }

        public Cookie? GetJwtToken()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }
            var token = context.Request.Cookies.FirstOrDefault(p => p.Key == "Bearer");
            return token.Value is not null && token.Key is not null ? new Cookie(token.Key, token.Value) : null;
        }


        public async Task SignOut()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }
            context.Response.Cookies.Delete("Bearer");
          await  context.SignOutAsync(IdentityConstants.ApplicationScheme);
        }


        public void SetJwtToken(string token)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }

            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(1)
            };
            context.Response.Cookies.Append("Bearer", token, options);

            var uri = httpClient.BaseAddress;
            cookieContainer.Add(new Cookie("Bearer", token, "/", uri.Host));
        }

        private void AddRefererHeader()
        {
            var referer = "https://localhost:7295/";
            if (!string.IsNullOrEmpty(navigationManager.BaseUri))
             referer = navigationManager.BaseUri;

            httpClient.DefaultRequestHeaders.Referrer = new Uri(referer);
        }
        public async Task<T> GetAsync<T>(string requestUri, IDictionary<string, string> parameters)
        {
            var uri = BuildUriWithParameters(requestUri, parameters);
            ValidateAndLogUri(uri);

            var jwtCookie = GetJwtToken();
            if (jwtCookie != null)
            {
                var fullUri = new Uri(httpClient.BaseAddress, uri);
                cookieContainer.Add(new Cookie("Bearer", jwtCookie.Value, "/", fullUri.Host));
            }

            var response = await httpClient.GetAsync(uri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);


            return JsonSerializer.Deserialize<T>(responseBody, options);
        }
        public async Task<string> GetStringAsync(string requestUri, IDictionary<string, string> parameters)
        {
            var uri = BuildUriWithParameters(requestUri, parameters);
            ValidateAndLogUri(uri);

            var jwtCookie = GetJwtToken();
            if (jwtCookie != null)
            {
                var fullUri = new Uri(httpClient.BaseAddress, uri);
                cookieContainer.Add(new Cookie("Bearer", jwtCookie.Value, "/", fullUri.Host));
            }

            var response = await httpClient.GetAsync(uri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
          

            return responseBody;
        }
        public async Task<T> GetAsync<T>(string requestUri)
        {
            ValidateAndLogUri(requestUri);

            var jwtCookie = GetJwtToken();
            if (jwtCookie != null)
            {
                var uri = httpClient.BaseAddress;
                cookieContainer.Add(new Cookie("Bearer", jwtCookie.Value, "/", uri.Host));
            }

            var response = await httpClient.GetAsync(requestUri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);

            return JsonSerializer.Deserialize<T>(responseBody, options);
        }
        public async Task PostAsync<T>(string requestUri, IDictionary<string, string> parameters, T? viewModel)
        {
            ValidateAndLogUri(requestUri);
            var uri = BuildUriWithParameters(requestUri, parameters);

            var response = await httpClient.PostAsJsonAsync(uri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        
        }
        public async Task PostAsync(string requestUri, IDictionary<string, string> parameters)
        {
            ValidateAndLogUri(requestUri);
            var uri = BuildUriWithParameters(requestUri, parameters);

            var response = await httpClient.PostAsJsonAsync(uri, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);

        }
        public async Task<TOut> PostLoginAsync<T, TOut>(string requestUri, T viewModel)
        {
            ValidateAndLogUri(requestUri);

            var response = await httpClient.PostAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
            ExtractBearerAndCookiesFromCookies(requestUri, response);

            return JsonSerializer.Deserialize<TOut>(responseBody, options);
        }

        public async Task<TOut> PostAsync<T, TOut>(string requestUri, T viewModel)
        {
            ValidateAndLogUri(requestUri);

            var response = await httpClient.PostAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);


            return JsonSerializer.Deserialize<TOut>(responseBody, options);
        }
        public async Task<string> PostAsyncGetString<T>(string requestUri, T viewModel)
        {
            ValidateAndLogUri(requestUri);

            var response = await httpClient.PostAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);


            return responseBody;
        }

        public async Task PostFormDataAsync(string requestUri, MultipartFormDataContent content)
        {
            ValidateAndLogUri(requestUri);

            var response = await httpClient.PostAsync(requestUri, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        }
       public async Task PutParametrsAsync(string requestUri, IDictionary<string, string> parameters)
        {
            ValidateAndLogUri(requestUri);
            var uri = BuildUriWithParameters(requestUri, parameters);

            var response = await httpClient.PutAsJsonAsync(uri, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);

        }
        public async Task PutAsync<T>(string requestUri, T viewModel)
        {
            ValidateAndLogUri(requestUri);

            var response = await httpClient.PutAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
   
        }
 

        public async Task PutAsync<T>(string requestUri, IDictionary<string, string> parameters, T viewModel)
        {
            var uri = BuildUriWithParameters(requestUri, parameters);
            ValidateAndLogUri(uri);

            var response = await httpClient.PutAsJsonAsync(uri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
    
        }

        public async Task DeleteAsync(string requestUri)
        {
            ValidateAndLogUri(requestUri);

            var response = await httpClient.DeleteAsync(requestUri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        
        }

        public async Task DeleteAsync(string requestUri, IDictionary<string, string> parameters)
        {
            var uri = BuildUriWithParameters(requestUri, parameters);
            ValidateAndLogUri(uri);

            var response = await httpClient.DeleteAsync(uri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);

        }

        private string BuildUriWithParameters(string requestUri, IDictionary<string, string> parameters)
        {
            var baseUri = new Uri(httpClient.BaseAddress, requestUri);
            var uriBuilder = new UriBuilder(baseUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var param in parameters)
            {
                query[param.Key] = param.Value;
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
        private void ValidateAndLogUri(string requestUri)
        {
            if ((requestUri is null) || !Uri.IsWellFormedUriString(requestUri, UriKind.RelativeOrAbsolute))
            {
                throw new UriFormatException($"Invalid URI: The format of the URI '{requestUri}' could not be determined.");
            }
        }

        public IEnumerable<Cookie> ExtractBearerAndCookiesFromCookies(string requestUri, HttpResponseMessage response)
        {
            var baseUri = httpClient.BaseAddress;
            var fullUri = new Uri(baseUri, requestUri);

            if (response.Headers.TryGetValues("Set-Cookie", out var cookieHeaders))
            {
                foreach (var cookieHeader in cookieHeaders)
                {
                    cookieContainer.SetCookies(fullUri, cookieHeader);
                }
            }

            List<Cookie> cookiesList = new List<Cookie>();
            foreach (Cookie cookie in cookieContainer.GetCookies(fullUri))
            {
                cookiesList.Add(cookie);
                if (cookie.Name == "Bearer")
                {
                    SetJwtToken(cookie.Value);
                }
            }
            return cookiesList;
        }
    }
}
