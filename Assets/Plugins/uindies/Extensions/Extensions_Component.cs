using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// component extensions
/// </summary>
public static class Extensions_Component
{
    /// <summary>
    /// 親オブジェクトを設定
    /// </summary>
    public static void SetParent(this Component self, GameObject parent)
    {
        self.transform.SetParent(parent.transform);
    }

    /// <summary>
    /// 親オブジェクトを設定
    /// </summary>
    public static void SetParent(this Component self, Component parent)
    {
        self.transform.SetParent(parent.transform);
    }

    /// <summary>
    /// 子オブジェクト数を取得
    /// </summary>
    public static int GetChildCount(this Component self)
    {
        return self.transform.childCount;
    }

    /// <summary>
    /// 親オブジェクトを取得
    /// </summary>
    public static GameObject GetParent(this Component self)
    {
        var t = self.transform.parent;
        return t != null ? t.gameObject : null;
    }

    /// <summary>
    /// ルートオブジェクトを取得
    /// </summary>
    public static GameObject GetRoot(this Component self)
    {
        var t = self.transform.root;
        return t != null ? t.gameObject : null;
    }

    /// <summary>
    /// レイヤー設定
    /// </summary>
    public static void SetLayer(this Component self, string layerName)
    {
        self.gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    /// <summary>
    /// 子要素も含めてレイヤー設定
    /// </summary>
    /// <param name="self"></param>
    /// <param name="layer">LayerMask.NameToLayer() で取得するレイヤー番号</param>
    public static void SetLayerRecursively(this Component self, int layer)
    {
        self.gameObject.layer = layer;

        foreach (Transform n in self.gameObject.transform)
        {
            SetLayerRecursively(n, layer);
        }
    }

    /// <summary>
    /// 子要素も含めてレイヤー設定
    /// </summary>
    public static void SetLayerRecursively(this Component self, string layerName)
    {
        self.SetLayerRecursively(LayerMask.NameToLayer(layerName));
    }

    /// <summary>
    /// コンポーネントを返す。なければ作る
    /// </summary>
    public static void SafeGetComponent<T>(this Component self, out T component) where T : Component
    {
        component = self.GetComponent<T>();
        if (component == null)
        {
            component = self.gameObject.AddComponent<T>();
        }
    }

    /// <summary>
    /// コンポーネントを取得
    /// </summary>
    public static bool CheckGetComponent<T>(this Component self, out T component) where T : Component
    {
        component = self.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError("Missing Component");
            return false;
        }
        return true;
    }

    /// <summary>
    /// コンポーネントを取得
    /// </summary>
    public static bool CheckGetComponentInChildren<T>(this Component self, out T component, bool includeInactive = true) where T : Component
    {
        component = self.GetComponentInChildren<T>(includeInactive);
        if (component == null)
        {
            Debug.LogError("Missing Component");
            return false;
        }
        return true;
    }

    /// <summary>
    /// コンポーネントに紐づく GameObject を破棄
    /// </summary>
    public static void Destroy(this Component self)
    {
        Object.Destroy(self.gameObject);
    }

    /// <summary>
    /// コンポーネントに紐づく GameObject を即時破棄
    /// </summary>
    public static void DestroyImmediate(this Component self)
    {
        Object.DestroyImmediate(self.gameObject);
    }

    /// <summary>
    /// コンポーネントに紐づく GameObject のアクティベート
    /// </summary>
    public static void SetActive(this Component self, bool value)
    {
        self.gameObject.SetActive(value);
    }

    /// <summary>
    /// 座標を取得します
    /// </summary>
    public static float GetX(this Component self)
    {
        return self.gameObject.transform.localPosition.x;
    }

    /// <summary>
    /// 座標を取得します
    /// </summary>
    public static float GetY(this Component self)
    {
        return self.gameObject.transform.localPosition.y;
    }

    /// <summary>
    /// 座標を設定します
    /// </summary>
    public static void SetX(this Component self, float x)
    {
        Vector3 trans = self.gameObject.transform.localPosition;
        trans.x = x;
        self.gameObject.transform.localPosition = trans;
    }

    /// <summary>
    /// 座標を設定します
    /// </summary>
    public static void SetY(this Component self, float y)
    {
        Vector3 trans = self.gameObject.transform.localPosition;
        trans.y = y;
        self.gameObject.transform.localPosition = trans;
    }

    /// <summary>
    /// 座標を設定します
    /// </summary>
    public static void SetXY(this Component self, float x, float y)
    {
        Vector3 trans = self.gameObject.transform.localPosition;
        trans.x = x;
        trans.y = y;
        self.gameObject.transform.localPosition = trans;
    }

    /// <summary>
    /// 回転を取得します
    /// </summary>
    public static float GetRotateX(this Component self)
    {
        return self.gameObject.transform.localEulerAngles.x;
    }

    /// <summary>
    /// 回転を取得します
    /// </summary>
    public static float GetRotateY(this Component self)
    {
        return self.gameObject.transform.localEulerAngles.y;
    }

    /// <summary>
    /// 回転を取得します
    /// </summary>
    public static float GetRotateZ(this Component self)
    {
        return self.gameObject.transform.localEulerAngles.z;
    }

    /// <summary>
    /// 回転を設定します
    /// </summary>
    public static void SetRotateXY(this Component self, float x, float y)
    {
        Vector3 trans = self.gameObject.transform.localEulerAngles;
        trans.x = x;
        trans.y = y;
        self.gameObject.transform.localEulerAngles = trans;
    }

    /// <summary>
    /// 回転を設定します
    /// </summary>
    public static void SetRotateX(this Component self, float x)
    {
        Vector3 trans = self.gameObject.transform.localEulerAngles;
        trans.x = x;
        self.gameObject.transform.localEulerAngles = trans;
    }

    /// <summary>
    /// 回転を設定します
    /// </summary>
    public static void SetRotateY(this Component self, float y)
    {
        Vector3 trans = self.gameObject.transform.localEulerAngles;
        trans.y = y;
        self.gameObject.transform.localEulerAngles = trans;
    }

    /// <summary>
    /// 回転を設定します
    /// </summary>
    public static void SetRotateZ(this Component self, float z)
    {
        Vector3 trans = self.gameObject.transform.localEulerAngles;
        trans.z = z;
        self.gameObject.transform.localEulerAngles = trans;
    }

    /// <summary>
    /// スケールを取得します
    /// </summary>
    public static float GetScaleX(this Component self)
    {
        return self.gameObject.transform.localScale.x;
    }

    /// <summary>
    /// スケールを取得します
    /// </summary>
    public static float GetScaleY(this Component self)
    {
        return self.gameObject.transform.localScale.y;
    }

    /// <summary>
    /// スケールを設定します
    /// </summary>
    public static void SetScaleX(this Component self, float x)
    {
        Vector3 trans = self.gameObject.transform.localScale;
        trans.x = x;
        self.gameObject.transform.localScale = trans;
    }

    /// <summary>
    /// スケールを設定します
    /// </summary>
    public static void SetScaleY(this Component self, float y)
    {
        Vector3 trans = self.gameObject.transform.localScale;
        trans.y = y;
        self.gameObject.transform.localScale = trans;
    }

    /// <summary>
    /// スケールを設定します
    /// </summary>
    public static void SetScaleXY(this Component self, float x, float y)
    {
        Vector3 trans = self.gameObject.transform.localScale;
        trans.x = x;
        trans.y = y;
        self.gameObject.transform.localScale = trans;
    }
}
