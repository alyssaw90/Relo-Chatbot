using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Models
{
    public class LodgingBot : LuisParser
    {
        // Spot the python programmer again...
        protected string api_endpoint = "";
        public LodgingBot(string query) : base(query) { }
    }
}