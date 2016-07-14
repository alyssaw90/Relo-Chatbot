using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReloChatBot;

namespace ReloChatBot.Models
{
    public class MiscBot
    {
        private LuisClient client;
        private string raw_result;

        private string api_endpoint =
            "https://api.projectoxford.ai/luis/v1/application?id=024edd6d-8598-4a3c-b942-05705dfd39b7&subscription-key=ba3ed0b23b714c4f9a8ca0fe2eeddcea&q=";

        public MiscBot(string query)
        {
            this.client = new LuisClient();
            this.raw_result = this.client.QueryLuis(this.api_endpoint, query);
        }
    }
}

//using RelochatBot.models;
//var x = new MiscBot("hello world");
//x.raw_result;