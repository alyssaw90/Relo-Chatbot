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
        const string RedirectMisc = "RedirectMisc";
        const string PositiveConfirmation = "PositiveConfirmation";
        const string NegativeConfirmation = "NegativeConfirmation";

        private LuisParser masterbot;
        private string userinput;

        private string reply;
        private Activity activity;

        //private bool ManualOverRide = false;

        public BotController(LuisParser masterbot, Activity activity)
        {
            this.masterbot = masterbot;
            this.activity = activity;
            this.userinput = this.activity.Text;

            // If the Intent is a Positive or Negative Confirmation, pass it on to the last bot that was talking.
            //this.ManualOverRide = (masterbot.Intent == PositiveConfirmation || masterbot.Intent == NegativeConfirmation);

            if (masterbot.RedirectRequired)
            {
                if (masterbot.Intent == RedirectLodging)
                {
                    //this.SetLastBotConversation(RedirectLodging);
                    //string test1 = this.GetLastBotConversation();
                    //bool test = this.GetLastBotConversation() == RedirectLodging;
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
            stateclient.BotState.SetUserData(this.activity.ChannelId, this.activity.From.Id, userdata);
        }

        public string LastBotConversation
        {
            get { return this.GetLastBotConversation(); }
            set { this.SetLastBotConversation(value); }
        }

        private void handle_RedirectLodging()
        {
            this.LastBotConversation = RedirectLodging;
            LodgingBot lobot = new LodgingBot(this.activity);
            lobot.Seed(this.masterbot);
            this.reply = lobot.Reply;
        }

        private void handle_RedirectMiscBot()
        {
            this.LastBotConversation = RedirectMisc;
        }

        public string Reply
        {
            get { return this.reply; }
        }

    }
}