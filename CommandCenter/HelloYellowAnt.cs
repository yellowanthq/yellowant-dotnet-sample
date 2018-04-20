using System.Linq;
using yellowantSDK;
using Newtonsoft.Json;
using Sample.Models;

namespace Sample.CommandCenter
{
    /* 
     * Note how YellowAnt MessageClass message is built. You can also send out independent messages using 
     * 'SendMessage' method, send Webhook Notifications and more.
     */ 
    public class HelloYellowAnt
    {
        public dynamic Args { get; set; }
        public int IntegrationID { get; set; }
        private DefaultConnectionContext db = new DefaultConnectionContext();

        public HelloYellowAnt(dynamic Args, int IntegrationID)
        {
            this.Args = Args;
            this.IntegrationID = IntegrationID;


        }
        

        public string Process()
        {
            MessageClass message = new MessageClass();
            message.MessageText = "Hello, {0}";
            message.MessageText = string.Format(message.MessageText, Args["Name"]);

            MessageAttachmentsClass attachment = new MessageAttachmentsClass();
            attachment.Title = "This is Attachment Title";
            attachment.Text = "This is the text of the Message. For more on this please visit 'https://docs.yellowant.com/' ";
            
            ButtonCommandsClass Bcc = new ButtonCommandsClass();
            Bcc.CalledFunction = "398";
            Bcc.ServiceApplication = IntegrationID;
            Command Cmd = new Command();
            Cmd.Name = "Button";
            Bcc.Data = Cmd;

            MessageButtonClass button = new MessageButtonClass();
            button.Value = "value";
            button.Name = "name";
            button.Text = "Test Button";
            button.Command = Bcc;

            attachment.AttachButton(button);
            message.Attach(attachment);
            string ToReturn = JsonConvert.SerializeObject(message);

            MessageClass ToSendMessage = new MessageClass();
            ToSendMessage.MessageText = "Sending Message";
            var user = db.UserIntegrationContext.Where(a => a.IntegrationID == IntegrationID).FirstOrDefault();
            Yellowant ya = new Yellowant
            {
                AccessToken = user.YellowantIntegrationToken
            };

            ya.SendMessage(IntegrationID, ToSendMessage);
            
            return ToReturn;
        }
    }
}