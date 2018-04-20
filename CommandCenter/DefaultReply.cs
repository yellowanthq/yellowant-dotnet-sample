using yellowantSDK;
using Newtonsoft.Json;

namespace Sample.CommandCenter
{
    public class DefaultReply
    {
        /*
         * This is default reply in case you have defined function in Yellowant and not implemented here.
         */ 
        public string Process()
        {
            MessageClass message = new MessageClass();
            message.MessageText = "I didn't catch that message";
            string ToReturn = JsonConvert.SerializeObject(message);
            return ToReturn;
        }
    }
}