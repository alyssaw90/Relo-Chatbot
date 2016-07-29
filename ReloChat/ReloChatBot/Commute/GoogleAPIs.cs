using Newtonsoft.Json;
using ReloChatBot.Distance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace ReloChatBot
{
    public class GoogleAPIs
    {
        /// <summary>
        /// Gets Commute Time and Distance for driving From google map api
        /// </summary>
        /// <param name="origin"> origin location from LUIS API entity</param>
        /// <param name="destination">destination location from LUIS API entity</param>
        /// <returns>Object from DistanceInfo Object</returns>
        public static async Task<ReloChatBot.Distance.DistanceInfo> GetDistanceInfoAsync(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) && string.IsNullOrWhiteSpace(destination))
                return null;

            origin = Uri.EscapeUriString(origin);
            destination = Uri.EscapeUriString(destination);

            // Google API to get transportation time and distance between two locations

            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={origin}&destinations={destination}&key=AIzaSyAUJouFEp0JoNF1cBi5TqzRaMsLntWPizk";

            string json;
            using (WebClient client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync(url).ConfigureAwait(false);
            }
            return JsonConvert.DeserializeObject<ReloChatBot.Distance.DistanceInfo>(json);
        }

    }
}