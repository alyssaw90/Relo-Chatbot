using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace ReloChatBot
{
    //Contains the code with the Google API URI and method that will convert the Json files to a.NET object. 
    public class Commute
    {
        public static async Task<CommuteInfo> GetBusCommuteInfoAsync(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) && string.IsNullOrWhiteSpace(destination))
                return null;

            string Origin = origin.Replace(" ", "+");
            string Destination = destination.Replace(" ", "+");

            //The website below has google api data which are all in json format.
            string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={Origin}&destination={Destination}&mode=transit&transit_mode=bus&key=AIzaSyDR7e6LShzVu7VDS5TOsGUwbs5aO5geJKU";

            string json;
            //Downloads all of the commute data and puts it in our variable called json.
            using (WebClient client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync(url).ConfigureAwait(false);
            }
            return JsonConvert.DeserializeObject<CommuteInfo>(json);

        }
        // CarCommuteInfo
    }
}