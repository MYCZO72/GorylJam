using UnityEngine;
using System.Collections;

public class PixelRenderer : MonoBehaviour 
{
    bool canWillRender = true;
    bool canRenderObject = true;
    Vector3 prevPos;
    float pixelsPerUnit;
    int width;
    int height;
    float unitsPerPixel;

    void OnRenderObject()
    {
        if (!canRenderObject)
            return;
        canRenderObject = false;
        transform.position = prevPos;
	}

    void Update()
    {
        canWillRender = true;
        canRenderObject = true;
        PixelSettings pixelSettings = (PixelSettings)Resources.Load("PixelSettings");
        pixelsPerUnit = pixelSettings.pixelsPerUnit;
        width = Screen.width;
        height = Screen.height;
        unitsPerPixel = 1f / pixelsPerUnit;
    }

    void OnWillRenderObject()
    {
        if (!canWillRender)
            return;
        prevPos = transform.position;

        unitsPerPixel = 1f / pixelsPerUnit;
        canWillRender = false;

        if (width != Screen.width || height != Screen.height)
        {
            width = Screen.width;
            height = Screen.height;
        }

        transform.position = new Vector3(Round(transform.position.x, unitsPerPixel), Round(transform.position.y, unitsPerPixel), Round(transform.position.z, unitsPerPixel));
	}

    public float Round(float value, float nearestValue)
    { 
        float modValue = value % nearestValue;
        float flatValue = value - modValue;
        if(modValue >= nearestValue/2)
            flatValue += nearestValue;
        return flatValue;
    }
}
