using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class CastTest : MonoBehaviour
{
	enum EnumNum
	{
		Num0,
		Num1,
		Num2,
		Num3
	}

	void Start()
	{
		EnumNum n = EnumNum.Num3;

		// 0byte
		Profiler.BeginSample("cast () enum -> int");
		int i = (int)n;
		Profiler.EndSample();

		// 40byte
		Profiler.BeginSample("cast Convert.ToInt32 enum -> int");
		i = System.Convert.ToInt32(n);
		Profiler.EndSample();

		// 40byte
		Profiler.BeginSample("cast Enum.ToObject int -> enum");
		n = (EnumNum)System.Enum.ToObject(typeof(EnumNum), i);
		Profiler.EndSample();

		// 0byte
		Profiler.BeginSample("cast () int -> enum");
		n = (EnumNum)i;
		Profiler.EndSample();

		// 20byte
		Profiler.BeginSample("cast () int -> object");
		object o = i;
		Profiler.EndSample();

		// 0byte
		Profiler.BeginSample("cast () object -> int");
		i = (int)o;
		Profiler.EndSample();
	}
}
