namespace MillionRegister.Core.Common;

public static class Utils
{
    public static string ThrowIfIsNullOrEmpty(string? value, string argument)
    {
        return string.IsNullOrEmpty(value) ? throw new ArgumentException($"{argument} is required") : value;
    }
}