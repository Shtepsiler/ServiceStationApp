using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using BlazorApp.Extensions.Exceptions;
using BlazorApp.Extensions.ViewModels;
using Newtonsoft.Json;

namespace BlazorApp.Extensions
{
    public static class StatusCodeHandler
    {

        private static readonly Dictionary<HttpStatusCode, Action<string>> statusCodesHandlers = new()
        {
            {
                HttpStatusCode.Forbidden,
                responseBody =>
                {
                    var errorViewModel = DeserializeResponse<ErrorViewModel>(responseBody);
                    if (errorViewModel != null)
                    {
                        throw new EntityNotFoundException(errorViewModel.Message);
                    }
                    throw new EntityNotFoundException("Resource not found");
                }
            },
            {
                HttpStatusCode.NotFound,
                responseBody =>
                {
                    var errorViewModel = DeserializeResponse<ErrorViewModel>(responseBody);
                    if (errorViewModel != null)
                    {
                        throw new EntityNotFoundException(errorViewModel.Message);
                    }
                    throw new EntityNotFoundException("Resource not found");
                }
            },
            {
                HttpStatusCode.BadRequest,
                responseBody =>
                {
                    var validationErrorViewModel = DeserializeResponse<AnunimusType>(responseBody);
                    if (validationErrorViewModel != null)
                    {
                        if(validationErrorViewModel.Error == "email not confirmed")
                        {
                        throw new EmailNotConfirmedException(validationErrorViewModel.Error);
                        }


                        throw new ValidationException(new Dictionary<string, List<string>>
                    {
                        { "Server", new List<string> { validationErrorViewModel.Error } }
                    });
                    }
                    else
                    throw new ValidationException(new Dictionary<string, List<string>>
                    {
                        { "General", new List<string> { "Bad request" } }
                    });
                }
            },
            {
                HttpStatusCode.InternalServerError,
                responseBody =>
                {
                    var errorViewModel = DeserializeResponse<ErrorViewModel>(responseBody);
                    if (errorViewModel != null)
                    {
                        throw new ServerResponseException(errorViewModel.Message);
                    }
                    throw new ServerResponseException("Internal server error");
                }
            },
            {
                HttpStatusCode.Unauthorized,
                responseBody =>
                {
                    throw new NotAuthorizedException();
                }
            }
        };

        public static void TryHandleStatusCode(HttpStatusCode statusCode, string responseBody)
        {
            if (statusCodesHandlers.TryGetValue(statusCode, out var statusCodeHandler))
            {
                statusCodeHandler.Invoke(responseBody);
            }
        }

        private static T? DeserializeResponse<T>(string responseBody) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                // Log or handle the error as needed
                return null;
            }
        }
    }
}
