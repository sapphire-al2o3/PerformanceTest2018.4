﻿using System.Collections;
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

        Profiler.BeginSample("concat4");
        s = num[0] + num[1] + num[2] + num[3];
        s += num[4];
        Profiler.EndSample();

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
    }
}
