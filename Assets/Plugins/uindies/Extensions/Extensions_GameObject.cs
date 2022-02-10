using UnityEngine;

/// <summary>
/// game object extensions
/// </summary>
public static class Extensions_GameObject
{
    /// <summary>
    /// 親オブジェクトを設定
    /// </summary>
    public static void SetParent(this GameObject self, GameObject parent)
    {
        self.transform.SetParent(parent.transform);
    }

    /// <summary>
    /// 親オブジェクトを設定
    /// </summary>
    public static void SetParent(this GameObject self, Component parent)
    {
        self.transform.SetParent(parent.transform);
    }

    /// <summary>
    /// 子オブジェクト数を取得
    /// </summary>
    public static int GetChildCount(this GameObject self)
    {
        return self.transform.childCount;
    }

    /// <summary>
    /// 親オブジェクトを取得
    /// </summary>
    public static GameObject GetParent(this GameObject self)
    {
        var t = self.transform.parent;
        return t != null ? t.gameObject : null;
    }

    /// <summary>
    /// ルートオブジェクトを取得
    /// </summary>
    public static GameObject GetRoot(this GameObject self)
    {
        var t = self.transform.root;
        return t != null ? t.gameObject : null;
    }

    /// <summary>
    /// レイヤー設定
    /// </summary>
    public static void SetLayer(this GameObject self, string layerName)
    {
        self.layer = LayerMask.NameToLayer(layerName);
    }

    /// <summary>
    /// 子要素も含めてレイヤー設定
    /// </summary>
    /// <param name="self"></param>
    /// <param name="layer">LayerMask.NameToLayer() で取得するレイヤー番号</param>
    public static void SetLayerRecursively(this GameObject self, int layer)
    {
        self.layer = layer;

        foreach (Transform n in self.transform)
        {
            SetLayerRecursively(n.gameObject, layer);
        }
    }

    /// <summary>
    /// 子要素も含めてレイヤー設定
    /// </summary>
    public static void SetLayerRecursively(this GameObject self, string layerName)
    {
        self.SetLayerRecursively(LayerMask.NameToLayer(layerName));
    }
    
    /// <summary>
    /// 
    /// </summary>
    public static void SetX(this GameObject self, float x)
    {
        Vector3 trans = self.transform.localPosition;
        trans.x = x;
        self.transform.localPosition = trans;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetY(this GameObject self, float y)
    {
        Vector3 trans = self.transform.localPosition;
        trans.y = y;
        self.transform.localPosition = trans;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetXY(this GameObject self, float x, float y)
    {
        Vector3 trans = self.transform.localPosition;
        trans.x = x;
        trans.y = y;
        self.transform.localPosition = trans;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetScaleX(this GameObject self, float sx)
    {
        Vector3 trans = self.transform.localScale;
        trans.x = sx;
        self.transform.localScale = trans;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetScaleY(this GameObject self, float sy)
    {
        Vector3 trans = self.transform.localScale;
        trans.y = sy;
        self.transform.localScale = trans;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetScaleXY(this GameObject self, float sx, float sy)
    {
        Vector3 trans = self.transform.localScale;
        trans.x = sx;
        trans.y = sy;
        self.transform.localScale = trans;
    }
}

