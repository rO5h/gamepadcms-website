using GamePadCMS_Website.DTOs;
using GamePadCMS_Website.Extensions;

namespace GamePadCMS_Website.Services
{
    public class ApiCaller
    {
        private readonly HttpClient _http;

        public ApiCaller(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResult> PostAsync(string url, HttpContent? content = null, CancellationToken cancellationToken = default)
        {
            var response = await _http.PostAsync(url, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult { IsSuccess = true };
            }
            else
            {
                var message = await ReadErrorMessageAsync(response);
                return new ApiResult { IsSuccess = false, ErrorMessage = message };
            }
        }

        private async Task<string> ReadErrorMessageAsync(HttpResponseMessage response)
        {
            var (problem, raw) = await response.ReadProblemDetailsAsync();

            // Try problem details first
            if (!string.IsNullOrWhiteSpace(problem?.Detail))
                return problem.Detail;

            // Try raw response body
            if (!string.IsNullOrWhiteSpace(raw))
                return raw;

            // Fall back to status code + reason (this matches browser console style)
            return $"{(int)response.StatusCode} ({response.ReasonPhrase})";
        }
    }
}
