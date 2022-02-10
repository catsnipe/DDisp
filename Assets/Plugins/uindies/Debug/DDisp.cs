using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class DDisp : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    ScrollRect           ViewRect = null;
    [SerializeField]
    RectTransform        ViewContent = null;
    [SerializeField]
    TextMeshProUGUI      Text = null;
    [SerializeField]
    TMP_InputField       Search = null;
    [SerializeField]
    Button               Group = null;
    [SerializeField]
    CanvasGroup          CanvasGroup = null;
    [SerializeField]
    Image                ViewImage = null;
    [SerializeField]
    Image                LockImage = null;
    [SerializeField]
    Button               LockButton = null;
    [SerializeField]
    Sprite[]             Sprites = null;
    [SerializeField]
    bool                 Locked;

    public const string COLOR_GREEN     = "<color=#00ff00>";
    public const string COLOR_YELLOW    = "<color=#ffff00>";
    public const string COLOR_RED       = "<color=#ff0000>";
    public const string COLOR_LIGHTGRAY = "<color=#a0a0a0>";
    public const string COLOR_GRAY      = "<color=#707070>";
    public const string COLOR_DARKGRAY  = "<color=#404040>";

    public const string GROUP_OFF       = "Off";
    public const string GROUP_DISPLAY   = "Display";
    public const string GROUP_CONSOLE   = "Console";

    class ConsoleLog
    {
        public string Trace;
        public string Text;
    }

    const int         CONSOLES_MAX = 100;

    List<string>      groups;

    string            currentGroup;
    string            group;

    List<Button>      buttons;
    Camera            targetCamera;
    StringBuilder     logsb;
    List<ConsoleLog>  consoles;

    int               cursorLine;
    int               addLine;
    int               maxLine;

    string            lastTag;
    string            searchFilter;

    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        initializeGroup();

        targetCamera    = Text.canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Text.canvas.worldCamera;
        logsb           = new StringBuilder();
        consoles        = new List<ConsoleLog>();
        currentGroup    = GROUP_OFF;

        refreshGroupButton();

        var rect = GetComponent<RectTransform>();
        
        float fontSize;

        if (Screen.width > Screen.height)
        {
            fontSize = rect.GetWidth() / 80;
        }
        else
        {
            fontSize = rect.GetHeight() / 80;
        }
        if (Text.fontSize != fontSize)
        {
            Text.fontSize = fontSize;
        }

        Application.logMessageReceived += logMessageReceived;
    }

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        refreshLock();

        Search.onValueChanged.AddListener((s) => refreshSearchFilter(s));
        LockButton.onClick.AddListener(() => toggleLock());
    }

    /// <summary>
    /// �O���[�v�ύX
    /// </summary>
    /// <param name="_group"></param>
    void onClick(string _group)
    {
        _ChangeCurrentGroup(_group);
    }

    /// <summary>
    /// LateUpdate
    /// </summary>
    void LateUpdate()
    {
        if (CanvasGroup.blocksRaycasts == true)
        {
            if (Padd.GetKeyDelay(ePad.UpArrow) == true)
            {
                this.addCursorLine(-1);
            }
            else
            if (Padd.GetKeyDelay(ePad.DownArrow) == true)
            {
                this.addCursorLine(1);
            }
        }

        if (currentGroup != GROUP_CONSOLE)
        {
            refreshText();
            logsb.Clear();
            maxLine = addLine;
            addLine = 0;
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // Warning ���
        UnityEditor.EditorApplication.delayCall += _OnValidate;
    }
 
    void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if(this == null) return;
        refreshLock();
    }
#endif
    
    /// <summary>
    /// �N���b�N�Ń|�W�V�����I��
    /// </summary>
    public void OnPointerClick(PointerEventData e)
    {
        var padvec = Padd.GetMouse().Position;
        var pos    = new Vector3(padvec.x, padvec.y, 0);
        var index  = TMP_TextUtilities.FindIntersectingLink(Text, pos, targetCamera);

        if (index == -1) return;

        var linkInfo = Text.textInfo.linkInfo[index];
        var id = getLinkTag(linkInfo.GetLinkID());

        if (id != null)
        {
            lastTag = id[1];

            if (int.TryParse(id[0], out int position) == true)
            {
                cursorLine = position;
            }
        }
    }

    /// <summary>
    /// �w�肵���O���[�v���w��������
    /// </summary>
    public void _ChangeCurrentGroup(string _group)
    {
        foreach (var button in buttons)
        {
            var image = button.GetComponentInChildren<Image>();
            var text  = button.GetComponentInChildren<TextMeshProUGUI>();
            if (text.text == _group)
            {
                image.SetColor(1.0f,1.0f,0.0f);
            }
            else
            {
                image.SetColor(1.0f,1.0f,1.0f);
            }
        }

        currentGroup = _group;
        lastTag      = null;
        cursorLine   = -1;

        refreshConsole();
        refreshLock();
    }

    /// <summary>
    /// ���ݓo�^���̃f�B�X�v���C�O���[�v���擾
    /// </summary>
    public string _GetCurrentGroup()
    {
        return currentGroup;
    }

    /// <summary>
    /// �O���[�v�ǉ�
    /// </summary>
    public void _AddGroup(string group)
    {
        initializeGroup();

        groups.Add(group);
        groups.Sort(
            (a,b) =>
            {
                if (a == GROUP_OFF)        return -1;
                else if (b == GROUP_OFF)   return  1;
                else if (a == GROUP_DISPLAY) return -1;
                else if (b == GROUP_DISPLAY) return  1;
                else return string.Compare(b, a);
            }
        );
        refreshGroupButton();
    }

    /// <summary>
    /// �\�����郍�O�O���[�v�ύX
    /// </summary>
    /// <returns>���ݕ\�����̃O���[�v�Ȃ�true�A����ȊO�Ȃ�false</returns>
    public bool _DisplayGroup(string _group)
    {
        if (groups.Contains(_group) == false)
        {
            group = GROUP_OFF;
            return false;
        }
        if (currentGroup == GROUP_OFF)
        {
            return false;
        }

        group = _group;

        return true;
    }

    /// <summary>
    /// �\�����郍�O�O���[�v�̃��Z�b�g
    /// </summary>
    public void _ResetDisplayGroup()
    {
        group = GROUP_DISPLAY;
    }

    /// <summary>
    /// ���O�\��
    /// </summary>
    /// <param name="message"></param>
    /// <param name="tag">�s�I�����Ɏ擾�\�ȃ^�O���</param>
    /// <param name="group">�O���[�v���ڎw��. �w�肵�Ȃ���� ChangeLogGroup() �ɏ]��</param>
    public void _Log(string message, string tag = null, string _group = GROUP_OFF)
    {
        if (currentGroup == GROUP_OFF)
        {
            return;
        }
        if (_group == GROUP_OFF)
        {
            _group = group;
            if (_group == GROUP_OFF)
            {
                return;
            }
        }
        if (currentGroup != _group)
        {
            // �ʃO���[�v�̃��O�Ȃ̂œo�^����
            return;
        }
        
        if (string.IsNullOrEmpty(searchFilter) == false && message.IndexOf(searchFilter) < 0)
        {
            // �������[�h�ΏۊO
            return;
        }

        if (message.IndexOf("<color") >= 0)
        {
            message += "</color>";
        }

        if (addLine == cursorLine)
        {
            lastTag = tag;

            if (message.IndexOf("<color") >= 0)
            {
                message = $"<color=#00ff88>></color>{message}";
            }
            else
            {
                message = $"<color=#00ff88>>{message}</color>";
            }
        }
        else
        {
            message = $"{message}";
        }

        logsb.Append($"<link=\"{addLine}:{tag}\">");
        logsb.Append(message);
        logsb.AppendLine("</link>");
        addLine++;
    }

    /// <summary>
    /// �Ō�ɑI�����ꂽ�s�̃^�O���擾
    /// </summary>
    public string _GetLastTag()
    {
        return lastTag;
    }

    /// <summary>
    /// ���O�r���[�̃X�N���[�����b�N ON / OFF
    /// </summary>
    /// <param name="locked">true..����֎~, false..���싖��</param>
    public void _SetLock(bool locked)
    {
        Locked = locked;
        refreshLock();
    }

    /// <summary>
    /// �O���[�v�̏�����
    /// </summary>
    void initializeGroup()
    {
        if (groups != null)
        {
            return;
        }
        groups = new List<string>();
        groups.Add(GROUP_OFF);
        groups.Add(GROUP_DISPLAY);
        groups.Add(GROUP_CONSOLE);

        group = GROUP_DISPLAY;
    }

    /// <summary>
    /// Debug.Log �̃��V�[�o�[
    /// </summary>
    void logMessageReceived(string condition, string stackTrace, LogType type)
    {
        string datetime = System.DateTime.Now.ToLongTimeString();

        string color;
        if (type == LogType.Log)
        {
            color = "white";
        }
        else
        if (type == LogType.Warning)
        {
            color = "orange";
        }
        else
        {
            color = "#ee4444";
        }

        string tr = "";
        var sts = Regex.Matches(stackTrace, "at [^)]+");
        foreach (var st in sts)
        {
            string s = st.ToString();
            if (tr.Length > 0 && s.IndexOf("at Library") >= 0)
            {
                continue;
            }
            tr += "    " + s + "\n";
        }
        if (string.IsNullOrEmpty(tr) == true)
        {
            return;
        }
        tr = tr.Remove(tr.Length-1, 1);

        string trace = $"<color={color}><size=80%>" + tr + $"</size></color>";
        string text  = $"<color={color}>[{datetime}] {condition}</color>";

        var stack = new ConsoleLog() { Trace = trace, Text = text };
        consoles.Add(stack);
        if (consoles.Count > CONSOLES_MAX)
        {
            consoles.RemoveAt(0);
        }

        if (currentGroup == GROUP_CONSOLE)
        {
            Log(text, null, GROUP_CONSOLE);
            Log(trace, null, GROUP_CONSOLE);

            refreshTextAndScrollBottom();
        }
    }

    /// <summary>
    /// �I���s�̏㉺
    /// </summary>
    /// <param name="amount">�ω���</param>
    void addCursorLine(int amount)
    {
        cursorLine += amount;
        cursorLine  = Mathf.Clamp(cursorLine, -1, maxLine-1);
    }

    /// <summary>
    /// ���O�r���[�̃X�N���[�����b�N�ω�
    /// </summary>
    void toggleLock()
    {
        Locked = !Locked;
        refreshLock();
    }

    /// <summary>
    /// ���b�N�{�^���̍X�V
    /// </summary>
    void refreshLock()
    {
        if (currentGroup == GROUP_OFF)
        {
            Search.SetActive(false);
            LockImage.enabled = false;
            ViewImage.enabled = false;
            CanvasGroup.blocksRaycasts = false;
        }
        else
        if (Locked == true)
        {
            LockImage.sprite  = Sprites[1];
            ViewImage.color   = new Color(0, 0, 0, 0.4f);
            Search.SetActive(false);
            LockImage.enabled = true;
            ViewImage.enabled = true;
            CanvasGroup.blocksRaycasts = false;
        }
        else
        {
            LockImage.sprite  = Sprites[0];
            ViewImage.color   = new Color(0, 0, 0.2f, 0.6f);
            Search.SetActive(true);
            LockImage.enabled = true;
            ViewImage.enabled = true;
            CanvasGroup.blocksRaycasts = true;
        }
    }

    /// <summary>
    /// �O���[�v�{�^���̍X�V
    /// </summary>
    void refreshGroupButton()
    {
        if (buttons != null)
        {
            for (int i = 1; i < buttons.Count; i++)
            {
                GameObject.DestroyImmediate(buttons[i].gameObject);
                buttons[i] = null;
            }
        }

        buttons = new List<Button>();
        for (int i = 0; i < groups.Count; i++)
        {
            string  group  = groups[i];

            var     button = i == 0 ? Group : UnityEngine.Object.Instantiate(Group, this.transform);
            var     rect   = button.GetComponent<RectTransform>();
            var     text   = button.GetComponentInChildren<TextMeshProUGUI>();

            Vector3 trans  = button.gameObject.transform.localPosition;
            trans.x = Group.gameObject.transform.localPosition.x - (rect.sizeDelta.x * 1.1f) * i;
            button.gameObject.transform.localPosition = trans;

            button.name = group;
            text.SetText(group);

            button.onClick.AddListener(() => onClick(group));

            buttons.Add(button);
        }
    }

    /// <summary>
    /// �e�L�X�g���ύX����Ă�����X�V
    /// </summary>
    void refreshText()
    {
        string text = logsb.ToString();

        if (Text.text == text)
        {
            return;
        }
        Text.SetText(text);
        Vector2 sizeDelta = ViewContent.sizeDelta;
        sizeDelta.y = Text.preferredHeight;
        ViewContent.sizeDelta = sizeDelta;
    }

    /// <summary>
    /// �X�N���[���ʒu�����̕��ł���Ύ����I�ɍŉ��i��
    /// </summary>
    void refreshTextAndScrollBottom()
    {
        float pos = ViewRect.verticalNormalizedPosition;
        refreshText();
        if (pos <= 0.1f)
        {
            ViewRect.verticalNormalizedPosition = 0;
        }
    }

    /// <summary>
    /// �����t�B���^�X�V
    /// </summary>
    void refreshSearchFilter(string filter)
    {
        searchFilter = filter;
        refreshConsole();
    }
    
    /// <summary>
    /// �R���\�[�����O�X�V
    /// </summary>
    void refreshConsole()
    {
        if (currentGroup == GROUP_CONSOLE)
        {
            logsb.Clear();
            addLine = 0;

            foreach (var stack in consoles)
            {
                if (string.IsNullOrEmpty(searchFilter) == true || stack.Text.IndexOf(searchFilter) >= 0)
                {
                    Log(stack.Text, null, GROUP_CONSOLE);
                    Log(stack.Trace, null, GROUP_CONSOLE);
                }
            }

            refreshText();
            ViewRect.verticalNormalizedPosition = 0;
        }
    }

    /// <summary>
    /// �����N�^�O�擾 �s��:�^�O ��z��ɂ��ĕԂ�
    /// </summary>
    string[] getLinkTag(string tagstr)
    {
        if (string.IsNullOrEmpty(tagstr) == true || tagstr.IndexOf(':') < 0)
        {
            return null;
        }
        return tagstr.Split(':');
    }
}
