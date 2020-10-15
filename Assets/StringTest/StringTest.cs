﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class StringTest : MonoBehaviour
{

    public static class StringBuilderUtil
    {
        public static void AppendInt(System.Text.StringBuilder sb, int value)
        {
            int n = value;
            if (n < 0)
            {
                sb.Append('-');
                n *= -1;
            }
            int k = 1;
            int m = n;
            while (m / 10 != 0)
            {
                m /= 10;
                k *= 10;
            }
            while (k != 0)
            {
                int d = n / k;
                n = n - d * k;
                k /= 10;
                char c = (char)('0' + d);
                sb.Append(c);
            }
        }
    }

    void Start()
    {
        string s0 = "aabbccddeeff";
        string s1 = "aa,bb,ccddee";

        // 90byte
        Profiler.BeginSample("split 1");
        string[] array = s0.Split(',');
        Profiler.EndSample();

        // 48byte
        {
            Profiler.BeginSample("no split");
            if (s0.IndexOf(',') >= 0)
            {

            }
            else
            {
                array = new string[] { s0 };
            }
            Profiler.EndSample();
        }

        // 204byte
        {
            Profiler.BeginSample("split 3");
            array = s1.Split(',');
            Profiler.EndSample();
        }

        string[] num = { "0", "1", "2", "3", "4", "5" };

        {
            Profiler.BeginSample("concat3");
            string s = num[0] + num[1] + num[2];
            Profiler.EndSample();
        }

        // GC.Alloc x1 34B
        {
            Profiler.BeginSample("concat +");
            string s = num[0] + num[1] + num[2] + num[3];
            Profiler.EndSample();
        }

        // GC.Alloc x3 96B
        {
            Profiler.BeginSample("concat +=");
            string s = num[0];
            s += num[1];
            s += num[2];
            s += num[3];
            Profiler.EndSample();
        }

        // 180B
        {

            Profiler.BeginSample("concat + 5");
            // 5個以上の場合、配列が生成される
            string s = num[0] + num[1] + num[2] + num[3] + num[4];
            Profiler.EndSample();
        }

        // 180B
        {
            Profiler.BeginSample("concat new array");
            string s = string.Concat(new string[] { num[0], num[1], num[2], num[3], num[4] });
            Profiler.EndSample();
        }

        // 118B
        {
            Profiler.BeginSample("concat array");
            // Concatは配列がコピーされる(.NET Framework)
            string s = string.Concat(num);
            Profiler.EndSample();
        }

        // 38B
        {
            Profiler.BeginSample("join empty");
            string s = string.Join("", num);
            Profiler.EndSample();
        }


        // 150Byte
        {
            Profiler.BeginSample("StringBuilder");
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < num.Length; i++)
            {
                sb.Append(num[i]);
            }
            string s = sb.ToString();
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
            string s = i0.ToString();
            Profiler.EndSample();
        }

        // 84byte
        {
            Profiler.BeginSample("int -> string (Format)");
            int i0 = 100;
            string s = string.Format("{0}", i0);
            Profiler.EndSample();
        }

        // 84byte
        {
            Profiler.BeginSample("int -> string ($)");
            int i0 = 100;
            string s = $"{i0}";
            Profiler.EndSample();
        }

        // 28byte
        {
            Profiler.BeginSample("last char (SubString)");
            string s = s0.Substring(s0.Length - 1);
            Profiler.EndSample();
        }

        // 28byte
        {
            Profiler.BeginSample("last char (ToString)");
            string s = s0[s0.Length - 1].ToString();
            Profiler.EndSample();
        }

        // 48byte
        {
            Profiler.BeginSample("join");
            string s = string.Join(",", num);
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
            string s = sb.ToString();
            Profiler.EndSample();
        }

        {
            Profiler.BeginSample("concat null");
            string s = s0 + null;
            Profiler.EndSample();
        }

        {
            Profiler.BeginSample("concat empty");
            string s = s0 + "";
            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("String.ToString");
            string s = s0.ToString();
            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("String.ToString");
            object o = s0;
            string s = o.ToString();
            Profiler.EndSample();
        }

        {
            Profiler.BeginSample("IndexOf default");
            for (int i = 0; i < 1000; i++)
            {
                bool ret = s0.IndexOf("bbccddee") >= 0;
            }
            Profiler.EndSample();
        }

        {
            Profiler.BeginSample("IndexOf Ordinal");
            for (int i = 0; i < 1000; i++)
            {
                // Containsと同じ
                bool ret = s0.IndexOf("bbccddee", System.StringComparison.Ordinal) >= 0;
            }
            Profiler.EndSample();
        }

        int[] numbers =
        {
            0,
            1,
            12,
            123,
            -3,
            -32,
            -321
        };

        // .NET Frameworkの実装ではint.ToStringされる
        // 186byte
        {
            var sb = new System.Text.StringBuilder(100);
            Profiler.BeginSample("StringBuilder number 1");

            for (int i = 0; i < numbers.Length; i++)
            {
                sb.Append(numbers[i]);
            }

            Profiler.EndSample();
            string s = sb.ToString();
            Debug.Log(s);
        }

        // 0は同じリテラルを返す
        // 0byte
        {
            Profiler.BeginSample("int.ToString 0");

            int n = 0;
            string s = n.ToString();

            Profiler.EndSample();

            int m = 0;
            string ss = m.ToString();
            // true
            Debug.Log(object.ReferenceEquals(ss, s));
        }

        // 1は文字列を生成して返す(.NET Coreでは1桁の数値はリテラルを返す)
        // 28byte
        {
            Profiler.BeginSample("int.ToString 1");

            int n = 1;
            
            string s = n.ToString();

            Profiler.EndSample();

            int m = 1;
            string ss = m.ToString();
            // false
            Debug.Log(object.ReferenceEquals(ss, s));
        }


        // 0byte
        {
            var sb = new System.Text.StringBuilder(100);
            Profiler.BeginSample("StringBuilder number 2");

            for (int i = 0; i < numbers.Length; i++)
            {
                int n = numbers[i];
                StringBuilderUtil.AppendInt(sb, n);
            }

            Profiler.EndSample();
            string s = sb.ToString();
            Debug.Log(s);
        }

        // boolは文字列リテラルが使われる
        // 0byte
        {
            var sb = new System.Text.StringBuilder(100);
            Profiler.BeginSample("StringBuilder bool");
            bool t = true;
            bool f = false;
            for (int i = 0; i < 10; i++)
            {
                sb.Append(i % 2 == 0 ? t : f);
            }
            Profiler.EndSample();
            string s = sb.ToString();
            Debug.Log(s);
        }
    }
}
