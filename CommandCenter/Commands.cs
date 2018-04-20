namespace Sample.CommandCenter
{
    /*
     * Redirect to your functions call here. This is like controller for rest of your functions.
     * Notice how depending on functions name respective class methods are called
     */ 

    public class Commands
    {
        public string FunctionName { get; set; }
        public int FunctionID { get; set; }
        public int IntegrationID { get; set; }
        public dynamic Args { get; set; }

        public string Process()
        {
            switch (FunctionName)
            {
                case "hello":
                    {
                        HelloYellowAnt cmd = new HelloYellowAnt(Args=Args, IntegrationID=IntegrationID);
                        cmd.Args = Args;
                        cmd.IntegrationID = IntegrationID;
                        return cmd.Process();
                    }
                default:
                    {
                        DefaultReply cmd = new DefaultReply();
                        return cmd.Process();
                    }
                    
            }
        }

    }
}