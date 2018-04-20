using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sample.CommandCenter
{
    /*
     * This is an example ButtonClass 'Command' constructor. Notice that ButtonCommandClass remains same for 
     * all buttons whereas Command can have any number of properties. You can also leverage dynamic input dialogue
     * boxes (like dialogue boxes in Slack) by defining respective properties in ButtonCommandClass and Commnd
     */ 
    public class ButtonCommandsClass
    {
        [JsonProperty(PropertyName = "called_function")]
        public string CalledFunction { get; set; }

        [JsonProperty(PropertyName = "service_application")]
        public int ServiceApplication  { get; set; }

        [JsonProperty(PropertyName = "data")]
        public Command Data { get; set; }
    }

    public class Command
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        
    }
}