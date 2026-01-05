using Lebetak.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Lebetak.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AIChatController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OpenAIOptions _options;

        public AIChatController(IHttpClientFactory httpClientFactory, IOptions<OpenAIOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (!request.Messages.Any())
                return BadRequest(new { error = "Prompt is required." });

            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                         ?? _options.ApiKey;

            if (string.IsNullOrEmpty(apiKey))
                return Problem("Missing OpenAI API key.");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.openai.com/v1/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new
            {
                model = _options.Model,
                messages = request.Messages
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("chat/completions", content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return Problem($"OpenAI error: {response.StatusCode}\n{responseJson}");

                var doc = JsonDocument.Parse(responseJson);

                string reply =
                    doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString() ?? "";

                return Ok(new ChatResponse { Reply = reply });
            }
            catch (Exception ex)
            {
                return Problem("Error: " + ex.Message);
            }
        }
    }
    public class ChatRequest
    {
        public List<AIChatMessageDTO> Messages { get; set; } = new();
    }

    public class AIChatMessageDTO
    {
        public string role { get; set; } = "";
        public string content { get; set; } = "";
    }
    public class ChatResponse
    {
        public string Reply { get; set; } = "";
    }
}
