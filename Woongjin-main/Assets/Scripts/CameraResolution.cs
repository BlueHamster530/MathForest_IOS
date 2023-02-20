using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Camera cam = GetComponent<Camera>();
        Rect rect = cam.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)21.0f / 9.0f);
        float scalewight = 1.0f/ scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1.0f - scaleheight) / 2.0f;
        }
        else
        {
            rect.width = scalewight;
            rect.x = (1.0f - scalewight) / 2.0f;
        }
        cam.rect = rect;
    }
}
