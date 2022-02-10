using System;
using UnityEngine;

public partial class DDisp : MonoBehaviour
{
    /// <summary>
    /// DDisp �A�N�Z�X�p�V���O���g���C���X�^���X
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
    /// �������B�����I�Ɏw�肵�Ȃ��ꍇ�AInstance �A�N�Z�X���ɌĂ΂��
    /// </summary>
    public static void Initialize()
    {
        Type t = typeof(DDisp);

        instance = (DDisp)FindObjectOfType(t, true);
    }

    /// <summary>
    /// �\������O���[�v�̕ύX
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
    /// �Ō�ɑI�����ꂽ���ڂ̃^�O���擾
    /// </summary>
    public static string GetLastTag()
    {
        return Instance?._GetLastTag();
    }

    /// <summary>
    /// �O���[�v�ǉ�
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
    /// �\�����郍�O�O���[�v�ύX
    /// </summary>
    /// <returns>���ݕ\�����̃O���[�v�Ȃ�true�A����ȊO�Ȃ�false</returns>
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
    /// ���O�\��
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">�O���[�v���ڎw��. �w�肵�Ȃ���� ChangeLogGroup() �ɏ]��</param>
    public static void Log(object message)
    {
        Instance?._Log(message.ToString(), null, DDisp.GROUP_OFF);
    }

    /// <summary>
    /// ���O�\��
    /// </summary>
    /// <param name="message"></param>
    /// <param name="group">�O���[�v���ڎw��. �w�肵�Ȃ���� ChangeLogGroup() �ɏ]��</param>
    public static void Log(object message, string group = DDisp.GROUP_OFF)
    {
        Instance?._Log(message.ToString(), null, group);
    }

    /// <summary>
    /// ���O�\��
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tag">�s�I�����Ɏ擾�\�ȃ^�O���</param>
    /// <param name="group">�O���[�v���ڎw��. �w�肵�Ȃ���� ChangeLogGroup() �ɏ]��</param>
    public static void Log(object message, string tag = null, string group = DDisp.GROUP_OFF)
    {
        Instance?._Log(message.ToString(), tag, group);
    }

    /// <summary>
    /// ���O�r���[�̃X�N���[�����b�N ON / OFF
    /// </summary>
    /// <param name="locked">true..����֎~, false..���싖��</param>
    public static void SetLock(bool locked)
    {
        Instance?._SetLock(locked);
    }

}
