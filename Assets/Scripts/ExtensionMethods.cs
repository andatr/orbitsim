using UnityEngine;

public static class ExtensionMethods
{
    // -------------------------------------------------------------------------------------------------------------------
    public static void SetParent(this GameObject child, GameObject parent)
    {
        if (parent)
            child.transform.SetParent(parent.transform);
    }
}
