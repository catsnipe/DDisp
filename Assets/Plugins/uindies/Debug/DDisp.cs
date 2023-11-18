using System;
using UnityEngine;

public partial class DDisp : MonoBehaviour
{
    static DebugDisp instance;

    void Awake()
    {
        instance = FindObjectOfType<DebugDisp>(true);
    }

    /// <summary>
    /// 表示するグループの変更
    /// </summary>
    public static void ChangeCurrentGroup(string _group)
    {
        instance?._ChangeCurrentGroup(_group);
        if (_group == DebugDisp.GROUP_OFF)
        {
            SetLock(false);
        }
    }

    /// <summary>
    /// 最後に選択された項目のタグを取得
    /// </summary>
    public static string GetLastTag()
    {
        return instance?._GetLastTag();
    }

    /// <summary>
    /// グループ追加
    /// </summary>
    /// <param name="group"></param>
    public static void AddGroup(string group)
    {
        instance?._AddGroup(group);
    }

    /// <summary>
    /// 
    /// </summary>
    public static string GetDisplayGroup()
    {
        return instance?._GetCurrentGroup();
    }

    /// <summary>
    /// 表示するロググループ変更
    /// </summary>
    /// <returns>現在表示中のグループならtrue、それ以外ならfalse</returns>
    public static bool DisplayGroup(string _group)
    {
        if (instance == null)
        {
            return false;
        }
        return instance._DisplayGroup(_group);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void ResetDisplayGroup()
    {
        instance?._ResetDisplayGroup();
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message)
    {
        instance?._Log(message.ToString(), null, DebugDisp.GROUP_OFF);
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message, string group = DebugDisp.GROUP_OFF)
    {
        instance?._Log(message.ToString(), null, group);
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tag">行選択時に取得可能なタグ情報</param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message, string tag = null, string group = DebugDisp.GROUP_OFF)
    {
        instance?._Log(message.ToString(), tag, group);
    }

    /// <summary>
    /// ログビューのスクロールロック ON / OFF
    /// </summary>
    /// <param name="locked">true..操作禁止, false..操作許可</param>
    public static void SetLock(bool locked)
    {
        instance?._SetLock(locked);
    }

}
