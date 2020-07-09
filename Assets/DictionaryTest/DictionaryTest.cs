using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.Linq;

public enum EnumType
{
    A,
    B,
    C,
    D,
    E
}

public class EnumTypeComparer : IEqualityComparer<EnumType>
{
    public bool Equals(EnumType x, EnumType y)
    {
        return x == y;
    }

    public int GetHashCode(EnumType obj)
    {
        return (int)obj;
    }
}

public class DictionaryTest : MonoBehaviour
{

    void Start()
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();

        for (int i = 0; i < 10000; i++)
        {
            dic.Add(i, i);
        }

        int sum = 0;

        // 120byte
        Profiler.BeginSample("values");
        foreach (var e in dic.Values)
        {
            sum += e;
        }
        Profiler.EndSample();

        // 120byte
        Profiler.BeginSample("keys");
        foreach (var k in dic.Keys)
        {
            sum += dic[k];
        }
        Profiler.EndSample();

        // 96byte
        Profiler.BeginSample("pair");
        foreach (var pair in dic)
        {
            sum += pair.Value;
        }
        Profiler.EndSample();
        
        // pairでキャッシュが生成されているため0byte
        Profiler.BeginSample("enemurator");
        using (var itr = dic.GetEnumerator())
        {
            while (itr.MoveNext())
            {
                sum += itr.Current.Value;
            }
        }
        Profiler.EndSample();

        // 80B
        var comp = new EnumTypeComparer();
		Dictionary<EnumType, int> dic2 = new Dictionary<EnumType, int>();
		Profiler.BeginSample("dictionary<EnumType, int>");
        dic2 = new Dictionary<EnumType, int>();
        Profiler.EndSample();

		// 172B
		Profiler.BeginSample("dictionary<EnumType, int>.set_item");
		for (int i = 0; i < 100; i++)
		{
			dic2[EnumType.A] = 0;
			dic2[EnumType.B] = 1;
			dic2[EnumType.C] = 2;
		}
		Profiler.EndSample();

		Profiler.BeginSample("dictionary<EnumType, int>.get_item");
		int dic2_0 = dic2[EnumType.A];
		dic2_0 += dic2[EnumType.B];
		dic2_0 += dic2[EnumType.C];
		Profiler.EndSample();

		// 488byte
		Dictionary<int, int> dic1 = new Dictionary<int, int>();
		Profiler.BeginSample("dictionary<int, int>");
        dic1 = new Dictionary<int, int>();
        Profiler.EndSample();

		Profiler.BeginSample("dictionary<int, int>.set_item");
		for (int i = 0; i < 100; i++)
		{
			dic1[0] = 0;
		}
		Profiler.EndSample();

		Profiler.BeginSample("dictionary<int, int>.get_item");
		int dic1_0 = dic1[0];
		Profiler.EndSample();

		// 217.3KB
		Profiler.BeginSample("dictionary<int, int>(10000)");
        dic1 = new Dictionary<int, int>(10000);
        Profiler.EndSample();

        // 0.5KB
        Profiler.BeginSample("dictionary<int, string>");
        Dictionary<int, string> dic3 = new Dictionary<int, string>();
        Profiler.EndSample();

        // 0.5KB
        Profiler.BeginSample("dictionary<string, int>");
        var dicsi = new Dictionary<string, int>();
        Profiler.EndSample();

        // 0.6KB
        Profiler.BeginSample("dictionary<string, string>");
        Dictionary<string, string> dic4 = new Dictionary<string, string>();
        Profiler.EndSample();

        // 304.1KB
        Profiler.BeginSample("dictionary<string, string>(10000)");
        dic4 = new Dictionary<string, string>(10000);
        Profiler.EndSample();

		// 234.6KB
		Profiler.BeginSample("reset dictionary (ToArray)");
		//foreach (var key in dic.Keys.to)
		{
		//	dic[key] = 0;
		}
		Profiler.EndSample();

		// 234.6KB
		Profiler.BeginSample("reset dictionary (new List)");
		foreach (var key in new List<int>(dic.Keys))
		{
			dic[key] = 0;
		}
		Profiler.EndSample();

		Profiler.BeginSample("reset dictionary (values)");
		
		Profiler.EndSample();

		// 96B
		{
			Profiler.BeginSample("readonly dictionary");
			var rodic = new System.Collections.ObjectModel.ReadOnlyDictionary<int, int>(dic);
			foreach (var e in rodic)
			{
			}
			Profiler.EndSample();
		}
	}
}
