using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class PixelSettings : ScriptableObject 
{
#if UNITY_EDITOR
    [MenuItem("Edit/Project Settings/Pixel Settings")]
    static void SelectPixelSettings()
    { 
        Object pixelSettings = Resources.Load("PixelSettings");
        Selection.objects = new Object[] { pixelSettings };
    }
#endif
    public float pixelsPerUnit = 1f;
}
