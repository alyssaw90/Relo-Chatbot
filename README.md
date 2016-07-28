# Relo-Chatbot

Website will be published here: https://leaprelochatbot.azurewebsites.net/

# Connecting your own custom bot
## Simple Intents
If you are trying to teach masterbot a new simple intent with a static reply
Open up `Models/IntentDirectory.cs` and edit the `master_actions` attribute.
Set the key as the string representation of the new Intent, and the value as the key as the static reply.

e.g.

```
public static Dictionary<string, string> master_actions = new Dictionary<string, string>()
        {
            { "RedirectTransportation", "Sounds like you have commute questions" },
            { "RedirectLodging", "Sounds like you have lodging questions" },
            { "WhatIsLeap", "Sounds like you have questions regarding what LEAP is" },
            { "WhereIsLeap", "Leap is located in Microsoft Building 86." },
            { "None", "I'm not sure if I understood you properly." },
            { "FriendlyUser", "Hello! I am Relo. A bot here to help you with all general questions. Ask a question!" },
        };
```

## Complex Bot
If you are building a new bot (New Luis.AI bot), you will need to create a new Bot under the `models/` directory. From there, you will need to create something like this:

```
public class LodgingBot : LuisParser
    {
        public LodgingBot(Activity activity, string api_endpoint = "www.luis_endpoint_here.com?q=") : base(activity, api_endpoint) { }
        public override string Reply
        {
            get { return this.GetReply(); }
        }

        private string GetReply()
        {
            // This will return the Intent that luis Determines.
            return this.Intent;
        }
    }
```

Customize the class to your liking, then in `Controllers/BotController.cs` add a method to construct and set the reply attribute.

For example:

```
    private void handle_RedirectLodging()
        {
            LodgingBot lobot = new LodgingBot(this.userinput);
            this.reply = lobot.Reply;
        }
```

Then invoke it in the constructor function like so:

```
if (masterbot.Intent == RedirectLodging)
      {
          this.handle_RedirectLodging();
      }
```

You can then access the `BotController.Reply` property (note the capital `R`) from anywhere.

# Development Setup
When loading the app, make sure to set `ReloChatBot` as the StartUp Project.
![image not showing?](http://i.imgur.com/WMKerVV.png)

# Bot Hierarchy 
Currently, the user input is fed through `MessagesController.cs` which is then passed to a `masterbot` which is a LUIS.ai bot that determines which bot to redirect to. The current redirects are:
- `RedirectMisc` (Redirects to MiscellaneousBot)
- `RedirectTransportation` (Redirects to CommuteBot)
- `RedirectLodging` (Redirects to LodgeBot)

These bots then determine a more specific intent.
