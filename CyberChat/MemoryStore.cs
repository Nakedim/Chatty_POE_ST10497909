namespace Chatty
{
    public class MemoryStore
    {

        public string UserName { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; }= string.Empty;
        Dictionary<string, string> UserInfo = new Dictionary<string, string>();


        public void Store(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) ||
                string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            switch (key.ToLower())
            {
                case "name":
                case "username":
                    UserName = value;
                    break;

                case "favouritetopic":
                case "topic":
                    FavouriteTopic = value;
                    break;

                default:
                    UserInfo[key] = value;
                    break;
            }

        }
        public string Recall(string key)
        {
            switch (key.ToLower())
            {
                case "name":
                case "username":
                    return UserName;

                case "favouritetopic":
                case "topic":
                    return FavouriteTopic;

                default:
                    if (UserInfo.ContainsKey(key))
                    {
                        return UserInfo[key];
                    }

                    return "";
            }
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
