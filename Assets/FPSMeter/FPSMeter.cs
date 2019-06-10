using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMeter : MonoBehaviour
{
	[SerializeField]
	float targetFrameTime = 0.0033f;
	[SerializeField]
	Color overColor;

	float frameDeltaTime = 0.0f;
	float prevTime = 0.0f;
	Vector4 size;
	Material mat;

	int sizeID;

	void Start()
	{
		sizeID = Shader.PropertyToID("_Size");
		mat = GetComponent<Renderer>().material;
		size = mat.GetVector(sizeID);
		size.x = 1.0f;
		mat.SetVector(sizeID, size);
	}

	void Update()
	{
		float currentTime = Time.realtimeSinceStartup;
		frameDeltaTime = currentTime - prevTime;
		prevTime = currentTime;

		size.x = frameDeltaTime / targetFrameTime;
		mat.SetVector(sizeID, size);
	}
}
