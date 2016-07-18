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

        public BotController(LuisParser masterbot, string userinput)
        {
            this.masterbot = masterbot;
            this.userinput = userinput;
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
                this.reply = "";
            }
        }
        private void handle_RedirectLodging()
        {
            LodgingBot lobot = new LodgingBot(this.userinput);
            this.reply = lobot.Reply;
        }

        public string Reply
        {
            get { return this.reply; }
        }

    }
}