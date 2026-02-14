using System.Text.Json;

namespace GamePadCMS_Website.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<(ProblemDetails? Problem, string? RawContent)> ReadProblemDetailsAsync(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null || responseMessage.Content == null)
                return (null, null);

            string rawContent = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.Content.Headers.ContentType?.MediaType is "application/json" or "application/problem+json")
            {
                try
                {
                    var problem = JsonSerializer.Deserialize<ProblemDetails>(rawContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return (problem, rawContent);
                }
                catch (JsonException)
                {
                    // Fall back to raw content
                }
            }

            return (null, rawContent);
        }
    }

    public class ProblemDetails
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }
        public Dictionary<string, object> Extensions { get; set; } = new Dictionary<string, object>();
    }
}
