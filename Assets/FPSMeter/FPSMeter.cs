using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMeter : MonoBehaviour
{
    [SerializeField]
    float targetFrameRate = 60.0f;
    [SerializeField]
    Color overColor;

    [SerializeField]
    float overFrameRate = 30.0f;

    float frameDeltaTime = 0.0f;
    float prevTime = 0.0f;
    Vector4 size;
    Material mat;

    [SerializeField]
    float interval = 0.2f;
    float elapsed = 0.0f;
    int frame = 0;

    Color defaultColor;

    int sizeID;
    int colorID;

    void Start()
    {
        sizeID = Shader.PropertyToID("_Size");
        mat = GetComponent<Renderer>().material;
        size = mat.GetVector(sizeID);
        size.x = 1.0f;
        mat.SetVector(sizeID, size);
        colorID = Shader.PropertyToID("_Color");
        defaultColor = mat.GetColor(colorID);
    }

    void Update()
    {
        float currentTime = Time.realtimeSinceStartup;
        frameDeltaTime = currentTime - prevTime;
        prevTime = currentTime;
        elapsed += frameDeltaTime;
        frame++;

        if (elapsed < interval)
        {
            return;
        }

        float time = elapsed / frame;

        size.x = time * targetFrameRate * 0.5f;
        mat.SetVector(sizeID, size);

        //Debug.Log(frame / elapsed);

        mat.SetColor(colorID, time > 1.0f / overFrameRate ? overColor : defaultColor);

        frame = 0;
        elapsed = 0.0f;
    }
}
