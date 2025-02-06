using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Company.Function.Models;
using Company.Function.Services;

namespace Company.Function
{
    public class FetchProcessStoreDataFunction
    {
        private readonly ILogger<FetchProcessStoreDataFunction> _logger;
        private readonly HttpClient _httpClient;

        private readonly DatabaseService _databaseService;
        public FetchProcessStoreDataFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FetchProcessStoreDataFunction>();
            _httpClient = new HttpClient();
        _databaseService = new DatabaseService(loggerFactory);
        }

        [Function("FetchProcessStoreDataFunction")]
        public async Task RunAsync([TimerTrigger("0 */10 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($" FetchProcessStoreDataFunction executed at: {DateTime.Now}");
              
            try
            {
                var response = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
                 _logger.LogInformation($"API Response: {response.Substring(0, 200)}...");

                var posts = JsonConvert.DeserializeObject<List<PostModel>>(response);
                if (posts != null)
                {
                    _logger.LogInformation($"{posts.Count} records fetched from the API.");
                    var filteredPosts = posts.Where(post => post.UserId == 1)
                                              .Select(post => {
                                                  post.Title = post.Title.ToUpper();
                                                  return post;
                                              }).ToList();
                    _databaseService.StoreData(filteredPosts);
                    _logger.LogInformation($"{filteredPosts.Count} records successfully processed and stored.");
                }
                else
                {
                    _logger.LogWarning("No records fetched from the API.");
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule for FetchProcessStoreDataFunction is at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
