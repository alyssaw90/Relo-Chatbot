using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Models
{
    public class CommuteBot : LuisParser
    {
        public CommuteBot(Activity activity, string api_endpoint = "https://api.projectoxford.ai/luis/v1/application?id=2fb62e84-da20-4205-8dad-d6206e533681&subscription-key=a0794768387a459da34bab6f49878c1e&q=") : base(activity, api_endpoint) { }
        public override string Reply
        {
            get { return this.GetReply(); }
        }

        private string GetReply()
        {
            CommuteBotFunctionality functionality = new CommuteBotFunctionality(this.Intent, this.activity);
            return functionality.Reply;
        }

    }
    public class CommuteBotFunctionality
    {
        #region Properties
        private string intent;
        private Activity activity;
        #endregion


        #region Constructor
        public CommuteBotFunctionality(string Intent, Activity activity)
        {
            this.intent = Intent;
            this.activity = activity;
        }
        #endregion

        public string Reply
        {
            get { return this.intent; }
        }
    }
}