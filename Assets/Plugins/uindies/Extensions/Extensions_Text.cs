using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// text extensions
/// </summary>
public static class Extensions_Text
{
    /// <summary>
    /// カラーのα値を変更します
    /// </summary>
    public static void SetAlpha(this TextMeshProUGUI self, float alpha)
    {
        self.color = new Color(self.color.r, self.color.g, self.color.b, alpha);
    }
    
    /// <summary>
    /// カラーの RGB 値を変更します
    /// </summary>
    public static void SetColor(this TextMeshProUGUI self, int r, int g, int b)
    {
        self.color = new Color(r / 256.0f, g / 256.0f, b / 256.0f, self.color.a);
    }

    /// <summary>
    /// カラーの RGB 値を変更します
    /// </summary>
    public static void SetColor(this TextMeshProUGUI self, Color col)
    {
        self.color = new Color(col.r, col.g, col.b, self.color.a);
    }

    /// <summary>
    /// カラーの RGB 値を変更します
    /// </summary>
    public static void SetColor(this TextMeshProUGUI self, float r, float g, float b)
    {
        self.color = new Color(r, g, b, self.color.a);
    }

    /// <summary>
    /// カラーの RGB 値を取得します
    /// </summary>
    public static Color GetColor(this TextMeshProUGUI self)
    {
        return self.color;
    }

    /// <summary>
    /// カラーの RGB 値を取得します
    /// </summary>
    public static void GetColor(this TextMeshProUGUI self, out int r, out int g, out int b)
    {
        r = (int)(self.color.a * 256.0f);
        g = (int)(self.color.g * 256.0f);
        b = (int)(self.color.b * 256.0f);
    }
}
