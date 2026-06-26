namespace Chatty
{
    public class MemoryStore
    {
<<<<<<< HEAD
        public string UserName { get; set; } = string.Empty;

        public string FavouriteTopic { get; set; } = string.Empty;

        private Dictionary<string, string> UserInfo =
            new Dictionary<string, string>();
=======

        public string UserName { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; }= string.Empty;
        Dictionary<string, string> UserInfo = new Dictionary<string, string>();
>>>>>>> 5241f87e6666f7db359025a39ddd7a8cd8a53566


        public void Store(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) ||
                string.IsNullOrWhiteSpace(value))
            {
                return;
            }

<<<<<<< HEAD
            key = key.ToLower().Trim();

            switch (key)
=======
            switch (key.ToLower())
>>>>>>> 5241f87e6666f7db359025a39ddd7a8cd8a53566
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
<<<<<<< HEAD
        }

        public string Recall(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return "";
            }

            key = key.ToLower().Trim();

            switch (key)
=======

        }
        public string Recall(string key)
        {
            switch (key.ToLower())
>>>>>>> 5241f87e6666f7db359025a39ddd7a8cd8a53566
            {
                case "name":
                case "username":
                    return UserName;

                case "favouritetopic":
                case "topic":
                    return FavouriteTopic;

                default:
<<<<<<< HEAD
                    return UserInfo.ContainsKey(key)
                        ? UserInfo[key]
                        : "";
            }
        }
        public string GetPersonalisedOpener()
        {
            if (!string.IsNullOrEmpty(FavouriteTopic))
            {
                return $"Welcome back! Ready to learn more about {FavouriteTopic}?";
            }

            return "User is new.";
        }
    }
}
=======
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
>>>>>>> 5241f87e6666f7db359025a39ddd7a8cd8a53566
