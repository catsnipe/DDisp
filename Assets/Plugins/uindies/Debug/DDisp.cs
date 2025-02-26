﻿using System;
using UnityEngine;

public partial class DDisp : MonoBehaviour
{
    static DebugDisp instance;

    void Awake()
    {
        instance = FindFirstObjectByType<DebugDisp>(FindObjectsInactive.Include);
#if MASTER_BUILD
        //if (instance != null)
        //{
        //    DestroyImmediate(instance.gameObject);
        //    instance = null;
        //}
#endif
    }

    /// <summary>
    /// 表示するグループの変更
    /// </summary>
    public static void ChangeCurrentGroup(string _group)
    {
        if (instance == null) return;

        instance._ChangeCurrentGroup(_group);
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
        if (instance == null) return null;
        return instance._GetLastTag();
    }

    /// <summary>
    /// グループ追加
    /// </summary>
    /// <param name="group"></param>
    public static void AddGroup(string group)
    {
        if (instance == null) return;
        instance._AddGroup(group);
    }

    /// <summary>
    /// 
    /// </summary>
    public static string GetDisplayGroup()
    {
        if (instance == null) return null;
        return instance._GetCurrentGroup();
    }

    /// <summary>
    /// 表示するロググループ変更
    /// </summary>
    /// <returns>現在表示中のグループならtrue、それ以外ならfalse</returns>
    public static bool DisplayGroup(string _group)
    {
        if (instance == null) return false;
        return instance._DisplayGroup(_group);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void ResetDisplayGroup()
    {
        if (instance == null) return;
        instance._ResetDisplayGroup();
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message)
    {
        Log(message.ToString(), null, DebugDisp.GROUP_OFF);
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message, string group = DebugDisp.GROUP_OFF)
    {
        Log(message.ToString(), null, group);
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tag">行選択時に取得可能なタグ情報</param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message, string tag = null, string group = DebugDisp.GROUP_OFF)
    {
        if (instance == null) return;
        instance._Log(message.ToString(), tag, group);
    }

    /// <summary>
    /// ログビューのスクロールロック ON / OFF
    /// </summary>
    /// <param name="locked">true..操作禁止, false..操作許可</param>
    public static void SetLock(bool locked)
    {
        if (instance == null) return;
        instance._SetLock(locked);
    }

    /// <summary>
    /// ログのフォントサイズ設定
    /// </summary>
    public static void SetFontSize(float size)
    {
        if (instance == null) return;
        instance._SetFontSize(size);
    }

    /// <summary>
    /// ログのフォントサイズ取得
    /// </summary>
    public static float GetFontSize()
    {
        if (instance == null) return 0;
        return instance._GetFontSize();
    }

    /// <summary>
    /// ログのフォントサイズ取得
    /// </summary>
    public static string GetSearchText()
    {
        if (instance == null) return null;
        return instance.GetSearchText();
    }
}
