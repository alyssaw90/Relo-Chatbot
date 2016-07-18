using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ReloChatBot.Models
{
    public static class IntentDirectory
    {
        public static Dictionary<string, string> master_actions = new Dictionary<string, string>()
        {
            { "RedirectTransportation", "Sounds like you have commute questions" },
            { "RedirectLodging", "Sounds like you have lodging questions" },
            { "WhatIsLeap", "Sounds like you have questions regarding what LEAP is" },
            { "WhereIsLeap", "Leap is located in Microsoft Building 86." },
            { "None", "I'm not sure if I understood you properly." },
            { "FriendlyUser", "Hello! I am Relo. A bot here to help you with all general questions. Ask a question!" },
        };
        public static Dictionary<string, string> commute_actions = new Dictionary<string, string>()
        {
            { "", "" },
        };

        public static Dictionary<string, string> lodging_actions = new Dictionary<string, string>()
        {
            { "", "" }
        };

        public static Dictionary<string, string> misc_actions = new Dictionary<string, string>()
        {
            { "GetWeather", "checkout this website for latest weather" }
        };
    }
}