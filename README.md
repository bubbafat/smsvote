#smsvote

##A simple Twilio app for free-form SMS based voting.

You need to add your own Twilio auth token.  Create the missing file PrivateData.cs and add a this content:

    namespace SmsVote.Web
    {
       public static class PrivateData
       {
           public const string TwilioAuthToken = "YOURAUTHTOKEN";
       }
    }

###Twilio POSTs messages to:
<http://your.domain/sms/vote>

###You can stats in JSON at (GET)
<http://your.domain/sms/stats>

###You can reset the stats at (POST)
<http://your.domain/sms/reset>

You want security?  Go ahead and add it!
