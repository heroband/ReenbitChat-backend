using Azure;
using Azure.AI.TextAnalytics;

namespace backend.Services;

/// <summary>
/// Service for sentiment analysis using Azure Cognitive Services
/// </summary>
public class SentimentService
{
    private readonly TextAnalyticsClient _client;

    public SentimentService(IConfiguration config)
    {
        var endpoint = new Uri(config["Azure:TextAnalytics:Endpoint"]);
        var apiKey = new AzureKeyCredential(config["Azure:TextAnalytics:Key"]);
        _client = new TextAnalyticsClient(endpoint, apiKey);
    }
    
    /// <summary>
    /// Analyzes the sentiment of the given text using Azure Text Analytics API
    /// </summary>
    /// <param name="text">The text to analyze</param>
    /// <returns>A string that shows the sentiment result (Positive, Negative or Neutral)</returns>
    public async Task<string> AnalyzeSentimentAsync(string text)
    {
        try
        {
            var result = await _client.AnalyzeSentimentAsync(text);
            return result.Value.Sentiment.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка в SentimentService: {ex.Message}");
            throw;
        }
    }
}