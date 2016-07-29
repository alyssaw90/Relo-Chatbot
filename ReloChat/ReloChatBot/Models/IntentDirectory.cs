using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ReloChatBot.Models
{
    /// <summary>
    /// IntentDirectory is a class for directing questions to the according intents 
    /// based on questions asked by users from Relo-ChatBot
    /// </summary>
    public static class IntentDirectory
    {
        /// <summary>
        /// master_actions is Dictionary including main intents about Relo Chat 
        /// </summary>
        public static Dictionary<string, string> master_actions = new Dictionary<string, string>()
        {
            { "RedirectTransportation", "Sounds like you have commute questions" },
            { "RedirectLodging", "Sounds like you have lodging questions" },
            { "WhatIsLeap", "Sounds like you have questions regarding what LEAP is" },
            { "WhereIsLeap", "Leap is located in Microsoft Building 86." },
            { "None", "I'm not sure if I understood you properly." },
            { "FriendlyUser", "Hello! I am Relo. A bot here to help you with all relocation questions. Ask me anything about relocating, commute or about living near Redmond." },
        };
    }
}
