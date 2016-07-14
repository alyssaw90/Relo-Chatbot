using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace ReloChatBot
{
    public class LuisClient
    {
        //A method that will send the user's input as a query to Luis and returns the intents and entities.
        public async Task<LuisInfo> QueryLuis(string strInput)
        {
            string strRet = string.Empty;

            //EscapeDataString removes invalid Uri characters i.e. spaces.
            string strEscaped = Uri.EscapeDataString(strInput);

            using (var client = new HttpClient())
            {
                //The Luis URI will be inserted below.
                string uri = "https://api.projectoxford.ai/luis/v1/application?id=3f56e744-90ea-4850-bcd2-759eea1237e7&subscription-key=6171c439d26540d6a380208a16b31958&q=" + strEscaped;
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    string jsonResponse = await msg.Content.ReadAsStringAsync();
                    LuisInfo _Data = JsonConvert.DeserializeObject<LuisInfo>(jsonResponse);
                    return _Data;
                }
            }
            return null;
        }
    }
}