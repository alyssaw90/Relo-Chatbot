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
        //Gets Commute Time and Distance for car.
        public static async Task<ReloChatBot.Distance.DistanceInfo> GetDistanceInfoAsync(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) && string.IsNullOrWhiteSpace(destination))
                return null;

            origin = Uri.EscapeUriString(origin);
            destination = Uri.EscapeUriString(destination);

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