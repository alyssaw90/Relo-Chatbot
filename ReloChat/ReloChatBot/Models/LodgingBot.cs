using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Models
{
    public class LodgingBot : LuisParser
    {
        // Spot the python programmer again...
        protected new string api_endpoint = "https://api.projectoxford.ai/luis/v1/application?id=8547ee95-9496-43ca-a9a2-b583da92cd7e&subscription-key=6171c439d26540d6a380208a16b31958?q=";
        public LodgingBot(string query) : base(query) { }
        public new string Reply
        {
            get { return this.GetReply(); }
        }

        private string GetReply()
        {

            return this.Intent;
        }
    }

}
