namespace MonBackendAspNet.Helpers;

public static class PasswordValidator
{
    public static bool IsPasswordValid(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 10)
            return false;

        bool hasUpper = password.Any(char.IsUpper);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));

        return hasUpper && hasDigit && hasSymbol;
    }
}
