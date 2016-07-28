using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace ReloChatBot
{
    public class LuisClient
    {
        /// <summary>
        /// A method that will send the user's input as a query to Luis and returns the intents and entities.
        /// </summary>
        /// <param name="endpoint">LUIS API End Point</param>
        /// <param name="strInput">Questions asked by users</param>
        /// <returns></returns>

        public string query;

        public string QueryLuis(string endpoint, string strInput)
        {
            string strRet = string.Empty;

            //EscapeDataString removes invalid Uri characters i.e. spaces.
            string query = Uri.EscapeDataString(strInput);
            this.query = query;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(endpoint + query).Result;

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