using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
        

public class PixelCamera : MonoBehaviour 
{
    float pixelsPerUnit;
    public int zoom = 64;
    Vector3 prevPos;
    float unitsPerPixel;
    int width;
    int height;

    [ContextMenu("Generate")]
    void Generate()
    {
        PixelSettings pixelSettings = (PixelSettings)ScriptableObject.CreateInstance<PixelSettings>();
        AssetDatabase.CreateAsset(pixelSettings, "Assets/Resources/PixelSettings.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void OnEnable()
    {
        PixelSettings pixelSettings = (PixelSettings)Resources.Load("PixelSettings");
        pixelsPerUnit = pixelSettings.pixelsPerUnit;
        unitsPerPixel = 1f / pixelsPerUnit;
        width = Screen.width;
        height = Screen.height;

    }

    void OnPreCull() 
    {
        prevPos = transform.position;
        unitsPerPixel = 1f / pixelsPerUnit;
        if (width != Screen.width || height != Screen.height)
        {
            width = Screen.width;
            height = Screen.height;
            transform.position = new Vector3(Round(transform.position.x, unitsPerPixel),
            Round(transform.position.y, unitsPerPixel),
            Round(transform.position.z, unitsPerPixel));
        }
        float scale = Mathf.Ceil(Screen.height / zoom);
        transform.position = new Vector3(Round(transform.position.x, unitsPerPixel),
            Round(transform.position.y, unitsPerPixel),
            Round(transform.position.z, unitsPerPixel));
        float horizontalFix = 0f;
        if (Screen.width % 2 == 1)
            horizontalFix = (Screen.height / 2 / scale * unitsPerPixel)/Screen.width;
        GetComponent<Camera>().orthographicSize = horizontalFix + (Screen.height / 2 / scale * unitsPerPixel);
	}

    void OnPostRender()
    {
        transform.position = prevPos;
    }

    public float Round(float value, float nearestValue)
    {
        float modValue = value % nearestValue;
        float flatValue = value - modValue;
        if (modValue >= nearestValue / 2)
            flatValue += nearestValue;
        return flatValue;
    }
}
