using UnityEngine;

public class DebugWatch
{
    static System.Diagnostics.Stopwatch s_watch = new System.Diagnostics.Stopwatch();
    static string    s_groupName   = "";

    /// <summary>
    /// start from here
    /// </summary>
    public static void Start(string groupName = null)
    {
        s_groupName = groupName == null ? "(null): " : $"[{groupName}]: ";

        s_watch.Reset();
        s_watch.Start();
    }

    /// <summary>
    /// elapsed write line
    /// </summary>
    public static void End()
    {
        Debug.Log($"{s_groupName}{s_watch.ElapsedMilliseconds} msec.");
    }
}
