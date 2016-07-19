using Microsoft.Bot.Connector;
using ReloChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Controllers
{
    public class BotController
    {
        const string RedirectLodging = "RedirectLodging";

        private LuisParser masterbot;
        private string userinput;

        private string reply;
        private Activity activity;

        public BotController(LuisParser masterbot, Activity activity)
        {
            this.masterbot = masterbot;
            this.activity = activity;
            this.userinput = this.activity.Text;
            if (masterbot.RedirectRequired)
            {
                if (masterbot.Intent == RedirectLodging)
                {
                    this.handle_RedirectLodging();
                }
                else
                {
                    this.reply = masterbot.Reply;
                    // this.reply = "No handle created for \"" + this.masterbot.Intent + "\"!";
                    // throw new Exception("No handle created for \"" + this.masterbot.Intent + "\"!");
                }
            } else
            {
                this.reply = masterbot.Reply;
            }
        }

        private string GetLastBotConversation()
        {
            StateClient stateclient = this.activity.GetStateClient();
            BotData userData = stateclient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            return userData.GetProperty<string>("LastBotConversation");
        }

        private void SetLastBotConversation(string bot)
        {
            StateClient stateclient = this.activity.GetStateClient();
            BotData userdata = stateclient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            userdata.SetProperty<string>("LastBotConversation", bot);
        }

        public string LastBotConversation
        {
            get { return this.GetLastBotConversation(); }
            set { this.SetLastBotConversation(value); }
        }

        private void handle_RedirectLodging()
        {
            this.LastBotConversation = "lobot";
            LodgingBot lobot = new LodgingBot(this.activity);
            this.reply = lobot.Reply;
        }

        public string Reply
        {
            get { return this.reply; }
        }

    }
}