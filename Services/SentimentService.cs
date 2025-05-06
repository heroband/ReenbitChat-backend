using Azure;
using Azure.AI.TextAnalytics;

namespace backend.Services;

public class SentimentService
{
    private readonly TextAnalyticsClient _client;

    public SentimentService(IConfiguration config)
    {
        var endpoint = new Uri(config["Azure:TextAnalytics:Endpoint"]);
        var apiKey = new AzureKeyCredential(config["Azure:TextAnalytics:Key"]);
        _client = new TextAnalyticsClient(endpoint, apiKey);
    }

    public async Task<string> AnalyzeSentimentAsync(string text)
    {
        var result = await _client.AnalyzeSentimentAsync(text);
        return result.Value.Sentiment.ToString();
    }
}