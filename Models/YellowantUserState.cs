
namespace Sample.Models
{
    /*
     * This stores the 'state' of user during OAuth cycle of the application. Refer to Yellowant api docs to know
     * more
     */ 
    public class YellowantUserState
    {   
        public int ID { get; set; }
        public string UserUniqueID { get; set; }
        public string UserState { get; set; }

    }
    
}