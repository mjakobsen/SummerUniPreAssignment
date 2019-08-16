using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SummerUniClient
{
    class Program
    {
        private static AsyncPolicy exponentialRetryPolicy = Policy.Handle<Exception>().WaitAndRetryForeverAsync(attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)), (ex, _) => Console.WriteLine("YOU HAVE DONE GOOFD, IDIOT"));

        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var skip = 0;
                var take = 100;
                while (true)
                {
                    try
                    {
                        var data = await exponentialRetryPolicy.ExecuteAsync(() => client.GetAsync($"http://localhost:49998/api/Cart/Events?skip={skip}&take={take}"));

                        if (data != null && data.Content != null)
                        {
                            var events = JsonConvert.DeserializeObject<List<ItemEvent>>(await data.Content.ReadAsStringAsync());
                            if (events != null && events.Count != 0)
                            {
                                skip += events.Count;
                                foreach (var itemEvent in events)
                                {
                                    Console.WriteLine($"Id: {itemEvent.Id}, cartId: {itemEvent.CartId}, itemId: {itemEvent.ItemId}, action: {itemEvent.EventType}");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"FUCK YOU IDIOT: {e.Message}");
                    }
                    finally
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
