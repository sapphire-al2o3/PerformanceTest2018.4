using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class AlignmentTest : MonoBehaviour
{
	class A
	{
		string a;
		byte b;
		string c;
		byte d;
	}

	// 40byte
	class B
	{
		string a;
		string b;
		byte c;
		byte d;
	}

	// 40byte
	class B1
	{
		string a;
		string b;
		byte c;
	}

	// 16byte
	class Empty
	{
	}

	// 24byte
	class A1
	{
		string a;
	}

	// 17byte
	class A2
	{
		byte a;
	}

	// 18byte
	class A3
	{
		byte a;
		byte b;
	}

	// 32byte
	class A4
	{
		byte a;
		string b;
	}

	// 32byte
	class A5
	{
		string b;
		int a;
	}

	// 32byte
	class A6
	{
		string b;
		string a;
	}

	void Start()
	{
		Profiler.BeginSample("A");
		var a = new A6();
		Profiler.EndSample();

		Profiler.BeginSample("B");
		B b = new B();
		Profiler.EndSample();
	}
}
