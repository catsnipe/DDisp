using System;
using UnityEngine;

public partial class DDisp : MonoBehaviour
{
    /// <summary>
    /// DDisp アクセス用シングルトンインスタンス
    /// </summary>
    public static DDisp Instance
    {
        get
        {
            if (instance == null)
            {
                Initialize();
            }
            return instance;
        }
    }
    static DDisp instance;

    /// <summary>
    /// 初期化。明示的に指定しない場合、Instance アクセス時に呼ばれる
    /// </summary>
    public static void Initialize()
    {
        Type t = typeof(DDisp);

        instance = (DDisp)FindObjectOfType(t, true);
    }

    /// <summary>
    /// 表示するグループの変更
    /// </summary>
    public static void ChangeCurrentGroup(string _group)
    {
        Instance?._ChangeCurrentGroup(_group);
        if (_group == DDisp.GROUP_OFF)
        {
            SetLock(false);
        }
    }

    /// <summary>
    /// 最後に選択された項目のタグを取得
    /// </summary>
    public static string GetLastTag()
    {
        return Instance?._GetLastTag();
    }

    /// <summary>
    /// グループ追加
    /// </summary>
    /// <param name="group"></param>
    public static void AddGroup(string group)
    {
        Instance?._AddGroup(group);
    }

    /// <summary>
    /// 
    /// </summary>
    public static string GetDisplayGroup()
    {
        return Instance?._GetCurrentGroup();
    }

    /// <summary>
    /// 表示するロググループ変更
    /// </summary>
    /// <returns>現在表示中のグループならtrue、それ以外ならfalse</returns>
    public static bool DisplayGroup(string _group)
    {
        if (Instance == null)
        {
            return false;
        }
        return Instance._DisplayGroup(_group);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void ResetDisplayGroup()
    {
        Instance?._ResetDisplayGroup();
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message)
    {
        Instance?._Log(message.ToString(), null, DDisp.GROUP_OFF);
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message, string group = DDisp.GROUP_OFF)
    {
        Instance?._Log(message.ToString(), null, group);
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tag">行選択時に取得可能なタグ情報</param>
    /// <param name="group">グループ直接指定. 指定しなければ ChangeLogGroup() に従う</param>
    public static void Log(object message, string tag = null, string group = DDisp.GROUP_OFF)
    {
        Instance?._Log(message.ToString(), tag, group);
    }

    /// <summary>
    /// ログビューのスクロールロック ON / OFF
    /// </summary>
    /// <param name="locked">true..操作禁止, false..操作許可</param>
    public static void SetLock(bool locked)
    {
        Instance?._SetLock(locked);
    }

}
