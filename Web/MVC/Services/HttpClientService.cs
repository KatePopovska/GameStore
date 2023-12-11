﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using MVC.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace MVC.Services
{
    public class HttpClientService : IHttpClientService
    {
        private ILogger<HttpClientService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientService(IHttpClientFactory clientFactory, ILogger<HttpClientService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content)
        {
            var client = _clientFactory.CreateClient();

            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (!string.IsNullOrEmpty(token))
            {
                client.SetBearerToken(token);
            }

            var httpMessage = new HttpRequestMessage();
            httpMessage.RequestUri = new Uri(url);
            httpMessage.Method = method;

            if (content != null)
            {
                httpMessage.Content =
                    new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            var result = await client.SendAsync(httpMessage);

            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                _logger.LogInformation($"RESULT CONTENT FROM HttpClentService: {resultContent}");
                var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
                return response!;
            }

            return default(TResponse)!;
        }
    }
}
