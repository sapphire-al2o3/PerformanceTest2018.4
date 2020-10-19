﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.Text.RegularExpressions;


public class RegexText : MonoBehaviour
{
    void Start()
    {
        // 4.0KB
        {
            Profiler.BeginSample("Regex");
            Regex reg = new Regex("hoge");
            Profiler.EndSample();
        }

        // 3.7KB
        {
            Profiler.BeginSample("Regex 2");
            Regex reg = new Regex("[0-9]");
            Profiler.EndSample();
        }

        // 5.8KB
        {
            Profiler.BeginSample("Regex 3");
            Regex reg = new Regex(@"https?://[\w!?/+\-_~;.,*&@#$%()'[\]]+");
            Profiler.EndSample();
        }

        // 初回のみ
        // 0.8KB
        {
            Regex reg = new Regex("[0-9]");
            Profiler.BeginSample("Match");
            string text = "a1";
            bool result = reg.IsMatch(text);
            Profiler.EndSample();

            Profiler.BeginSample("Match 2");
            result = reg.IsMatch(text);
            Profiler.EndSample();
        }

        // 初回 1.9KB
        // 2回目 388B
        {
            Regex reg = new Regex("[0-9]");
            Profiler.BeginSample("Replace");
            string text = "abc0def";
            string result = reg.Replace(text, "*");
            Profiler.EndSample();

            Profiler.BeginSample("Replace 2");
            result = reg.Replace(text, "*");
            Profiler.EndSample();

            Debug.Log(result);
        }

        // 2.3KB
        {
            Regex reg = new Regex("([0-9])");
            Profiler.BeginSample("Replace 3");
            string text = "abc0def";
            string result = reg.Replace(text, "[$1]");
            Profiler.EndSample();

            Debug.Log(result);
        }

        // Regexオブジェクトを生成しなくてもRegex.CacheSizeまではキャッシュされる
        {
            Profiler.BeginSample("Regex.Replace");
            string text = "abc0def";
            string pattern = "[0-9]";
            string result = Regex.Replace(text, pattern, "*");
            Profiler.EndSample();

            Profiler.BeginSample("Regex.Replace 2");
            result = Regex.Replace(text, pattern, "*");
            Profiler.EndSample();

            Profiler.BeginSample("Regex.Replace 3");
            result = Regex.Replace(text, pattern, "/");
            Profiler.EndSample();
        }
    }
}
