using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class StringTest : MonoBehaviour
{
    void Start()
    {
        string s0 = "aabbccddeeff";
        string s1 = "aa,bb,ccddee";

        // 90byte
        Profiler.BeginSample("split 1");
        string[] array = s0.Split(',');
        Profiler.EndSample();

        // 48byte
        Profiler.BeginSample("no split");
        if (s0.IndexOf(',') >= 0)
        {

        }
        else
        {
            array = new string[] { s0 };
        }
        Profiler.EndSample();

        // 204byte
        Profiler.BeginSample("split 3");
        array = s1.Split(',');
        Profiler.EndSample();

        string[] num = { "0", "1", "2", "3", "4", "5" };

        Profiler.BeginSample("concat3");
        string s = num[0] + num[1] + num[2];
        Profiler.EndSample();

        // GC.Alloc x1 34B
        Profiler.BeginSample("concat +");
        s = num[0] + num[1] + num[2] + num[3];
        Profiler.EndSample();

        // GC.Alloc x3 96B
        Profiler.BeginSample("concat +=");
        s = num[0];
        s += num[1];
        s += num[2];
        s += num[3];
        Profiler.EndSample();

        // 150Byte
        {
            Profiler.BeginSample("StringBuilder");
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < num.Length; i++)
            {
                sb.Append(num[i]);
            }
            s = sb.ToString();
            Profiler.EndSample();
        }

        // 50byte
        {
            Profiler.BeginSample("replace string");
            string r = s1.Replace(",", ".");
            Profiler.EndSample();
        }

        // 50byte
        {
            Profiler.BeginSample("replace char");
            string r = s1.Replace(',', '.');
            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("no replace");
            string r = s1.Replace('@', '.');
            Profiler.EndSample();
        }

        // 32byte
        {
            Profiler.BeginSample("int -> string (ToString)");
            int i0 = 100;
            s = i0.ToString();
            Profiler.EndSample();
        }

        // 84byte
        {
            Profiler.BeginSample("int -> string (Format)");
            int i0 = 100;
            s = string.Format("{0}", i0);
            Profiler.EndSample();
        }

        // 84byte
        {
            Profiler.BeginSample("int -> string ($)");
            int i0 = 100;
            s = $"{i0}";
            Profiler.EndSample();
        }

        // 28byte
        {
            Profiler.BeginSample("last char (SubString)");
            s = s0.Substring(s0.Length - 1);
            Profiler.EndSample();
        }

        // 28byte
        {
            Profiler.BeginSample("last char (ToString)");
            s = s0[s0.Length - 1].ToString();
            Profiler.EndSample();
        }

        // 48byte
        {
            Profiler.BeginSample("join");
            s = string.Join(",", num);
            Profiler.EndSample();
        }

        // 162byte
        {
            Profiler.BeginSample("StringBuilder join");
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < num.Length; i++)
            {
                sb.Append(num[0]);
                sb.Append(",");
            }
            s = sb.ToString();
            Profiler.EndSample();
        }

        {
            Profiler.BeginSample("concat null");
            s = s0 + null;
            Profiler.EndSample();
        }

        {
            Profiler.BeginSample("concat empty");
            s = s0 + "";
            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("String.ToString");
            s = s0.ToString();
            Profiler.EndSample();
        }
    }
}
