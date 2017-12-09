using UnityEngine;
using UnityEditor;
using System.Collections;

public class PixelSettings : ScriptableObject 
{
    [MenuItem("Edit/Project Settings/Pixel Settings")]
    static void SelectPixelSettings()
    { 
        Object pixelSettings = Resources.Load("PixelSettings");
        Selection.objects = new Object[] { pixelSettings };
    }

    public float pixelsPerUnit = 1f;
}
