using System.Collections.Generic;

/// <summary>
/// list extensions
/// </summary>
public static class Extensions_List
{
    public static bool IsNullOrZero<T>(this IList<T> self)
    {
        return self == null || self.Count == 0;
    }
}

