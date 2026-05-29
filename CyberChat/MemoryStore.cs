namespace Chatty
{
    public class MemoryStore
    {

        public string UserName { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; }= string.Empty;
        Dictionary<string, string> UserInfo = new Dictionary<string, string>();


       public void Store(string UserName, string FavouriteTopic )
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                return;
            }
            switch (UserName.ToLower())
            {
                case "name":
                case "username":
                    UserName = FavouriteTopic;
                    break;
                case "FavouriteTopic":
                case "topic":
                   FavouriteTopic = UserName;
                    break;
                default:
                    UserInfo[UserName] = FavouriteTopic;
                    break;
            }
            
        }


        public string Recall(string key)
        {
            return "";
                ;
        }
   
    public string GetPersonalisedOpener(string input)
        {
            input.ToLower().Trim();
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            if (input.Contains("interested"))
            {
                input = FavouriteTopic;
            }
            return "user is new";
        }
    }

    
}
