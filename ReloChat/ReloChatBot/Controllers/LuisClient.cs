using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace ReloChatBot
{
    public class LuisClient
    {
        //A method that will send the user's input as a query to Luis and returns the intents and entities.
        public string QueryLuis(string strInput)
        {
            string strRet = string.Empty;

            //EscapeDataString removes invalid Uri characters i.e. spaces.
            string query = Uri.EscapeDataString(strInput);

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://api.projectoxford.ai/luis/v1/application?id=3f56e744-90ea-4850-bcd2-759eea1237e7&subscription-key=6171c439d26540d6a380208a16b31958&q=" + query).Result;

                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    return responseString;
                }
            }
            return null;
        }
    }
}