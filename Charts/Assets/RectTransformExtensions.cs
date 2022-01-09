using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetAll(this RectTransform rt, RectOffset rect)
    {
        rt.SetTop(rect.top);
        rt.SetBottom(rect.bottom);
        rt.SetLeft(rect.left);
        rt.SetRight(rect.right);
    }
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
    public static float GetLeft(this RectTransform rt) => rt.offsetMin.x;
    public static float GetRight(this RectTransform rt) => -rt.offsetMax.x;
    public static float GetTop(this RectTransform rt) => -rt.offsetMax.y;
    public static float GetBottom(this RectTransform rt) => rt.offsetMax.y;
}
