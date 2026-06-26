public class MemoryStore
{
    // Use auto-properties with modern C# string initialization
    public string UserName { get; set; } = string.Empty;
    public string FavouriteTopic { get; set; } = string.Empty;

    // Made readonly to prevent accidental reassignment
    private readonly Dictionary<string, string> _userInfo = new Dictionary<string, string>();

    public void Store(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // Standardize key formatting across the entire class lifecycle
        var normalizedKey = key.Trim().ToLowerInvariant();

        switch (normalizedKey)
        {
            case "name":
            case "username":
                UserName = value.Trim();
                break;

            case "favouritetopic":
            case "topic":
                FavouriteTopic = value.Trim();
                break;

            default:
                _userInfo[normalizedKey] = value.Trim();
                break;
        }
    }

    public string Recall(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return string.Empty;
        }

        var normalizedKey = key.Trim().ToLowerInvariant();

        switch (normalizedKey)
        {
            case "name":
            case "username":
                return UserName;

            case "favouritetopic":
            case "topic":
                return FavouriteTopic;

            default:
                // Safe dictionary lookups via TryGetValue avoid errors
                return _userInfo.TryGetValue(normalizedKey, out var val) ? val : string.Empty;
        }
    }

    public string GetPersonalisedOpener()
    {
        return !string.IsNullOrEmpty(FavouriteTopic)
            ? $"Welcome back! Ready to learn more about {FavouriteTopic}?"
            : "User is new.";
    }

    public string GetPersonalisedOpener(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "User is new.";
        }

        var normalizedInput = input.Trim().ToLowerInvariant();

        if (normalizedInput.Contains("interested") && !string.IsNullOrEmpty(FavouriteTopic))
        {
            return $"Welcome back! Ready to learn more about {FavouriteTopic}?";
        }

        return "User is new.";
    }
}