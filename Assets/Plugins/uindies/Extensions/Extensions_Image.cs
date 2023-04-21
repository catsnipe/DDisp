using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// image extensions
/// </summary>
public static class Extensions_Image
{
    /// <summary>
    /// サイズを設定します
    /// </summary>
    public static void SetNativeSize(this Image self)
    {
        if (self.sprite != null)
        {
            SetSize(self, self.sprite.rect.width, self.sprite.rect.height);
        }
        else
        {
            SetSize(self, 100, 100);
        }
    }

    /// <summary>
    /// サイズを設定します
    /// </summary>
    public static void SetSize(this Image self, float width, float height)
    {
        self.rectTransform.SetSize(width, height);
    }
    
    /// <summary>
    /// カラーのα値を変更します
    /// </summary>
    public static void SetAlpha(this Image self, float alpha)
    {
        self.color = new Color(self.color.r, self.color.g, self.color.b, alpha);
    }
    
    /// <summary>
    /// カラーの RGB 値を変更します
    /// </summary>
    public static void SetRGB(this Image self, int r, int g, int b)
    {
        self.color = new Color(r / 255.0f, g / 255.0f, b / 255.0f, self.color.a);
    }

    /// <summary>
    /// カラーの RGB 値を変更します
    /// </summary>
    public static void SetRGB(this Image self, float r, float g, float b)
    {
        self.color = new Color(r, g, b, self.color.a);
    }

    /// <summary>
    /// カラーの RGB 値を変更します
    /// </summary>
    public static void SetColor(this Image self, Color col)
    {
        self.color = new Color(col.r, col.g, col.b, self.color.a);
    }

    /// <summary>
    /// カラーの RGB 値を取得します
    /// </summary>
    public static Color GetColor(this Image self)
    {
        return self.color;
    }

    /// <summary>
    /// カラーの RGB 値を取得します
    /// </summary>
    public static void GetColor(this Image self, out float r, out float g, out float b)
    {
        r = self.color.r;
        g = self.color.g;
        b = self.color.b;
    }

    /// <summary>
    /// カラーの RGB 値を取得します
    /// </summary>
    public static void GetColor(this Image self, out int r, out int g, out int b)
    {
        r = (int)(self.color.r * 255.0f);
        g = (int)(self.color.g * 255.0f);
        b = (int)(self.color.b * 255.0f);
    }
}
