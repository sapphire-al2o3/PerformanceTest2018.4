﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Profiling;

public class ArrayTest : MonoBehaviour
{
    struct CompStruct : IComparer<int>
    {
        public int Compare(int x, int y) => x - y;
    }

    void Start()
    {
        // 40 byte
        Profiler.BeginSample("int[0]");
        int[] b = new int[0];
        Profiler.EndSample();

        // 44 byte
        Profiler.BeginSample("int[1]");
        int[] a = new int[1];
        Profiler.EndSample();

        // 440 byte
        Profiler.BeginSample("int[]");
        int[] array = new int[10 * 10];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                array[i * 10 + j] = 0;
            }
        }
        Profiler.EndSample();


        // 488 byte
        Profiler.BeginSample("int[,]");
        int[,] array0 = new int[10, 10];

        for (int i = 0; i < array0.GetLength(0); i++)
        {
            for (int j = 0; j < array0.GetLength(1); j++)
            {
                array0[i, j] = 0;
            }
        }
        Profiler.EndSample();

        // 0.9 Kbyte
        Profiler.BeginSample("int[][]");
        int[][] array1 = new int[10][];

        for (int i = 0; i < array1.Length; i++)
        {
            array1[i] = new int[10];
            for (int j = 0; j < array1[i].Length; j++)
            {
                array1[i][j] = 0;
            }
        }
        Profiler.EndSample();

        // 40byte
        Profiler.BeginSample("string[0]");
        string[] s = new string[0];
        Profiler.EndSample();

        // 48byte
        Profiler.BeginSample("string[1]");
        s = new string[1];
        Profiler.EndSample();

        // 32byte
        // int[] _items
        // int _size
        // int _version
        // (Object _syncRoot)
        Profiler.BeginSample("List<int>");
        List<int> list = new List<int>();
        Profiler.EndSample();

        // 76byte
        // 32(Listのサイズ) + 44(int[1]のサイズ)
        Profiler.BeginSample("List<int>(1)");
        list = new List<int>(1);
        Profiler.EndSample();

        // 88byte
        // デフォルトのキャパシティが4なので4つまではメモリ確保されない
        {
            Profiler.BeginSample("List<int> 2");
            List<int> list2 = new List<int>();
            list2.Add(1);
            list2.Add(2);
            list2.Add(3);
            list2.Add(4);
            Profiler.EndSample();
        }

        // 88byte
        Profiler.BeginSample("List<int>(4)");
        List<int> list3 = new List<int>(4);
        Profiler.EndSample();

        // 78.2KB
        // 32 + 40 + 10000 * 8
        Profiler.BeginSample("List<string>(10000)");
        List<string> list4 = new List<string>(10000);
        Profiler.EndSample();

        Func<int, int> orderFunc = x => x;
        // 488byte
        Profiler.BeginSample("OrderBy");
        array.OrderBy(orderFunc);
        Profiler.EndSample();

        // 10.9KB
        // Comparer.Compare -> Comparisionのキャストが発生する
        Profiler.BeginSample("Sort");
        for (int i = 0; i < 100; i++)
        {
            Array.Sort(array);
        }
        Profiler.EndSample();

        // 10.9KB
        Profiler.BeginSample("Sort Default");
        var comparer = Comparer<int>.Default;
        for (int i = 0; i < 100; i++)
        {
            Array.Sort(array, comparer);
        }
        Profiler.EndSample();

        // 112byte
        Profiler.BeginSample("Sort Lambda");
        for (int i = 0; i < 100; i++)
        {
            Array.Sort(array, (x, y) => x - y);
        }
        Profiler.EndSample();

        // boxingとComparisionへのキャストが発生する
        // 12.6KB
        {
            Profiler.BeginSample("Sort IComparer");
            for (int i = 0; i < 100; i++)
            {
                Array.Sort(array, new CompStruct());
            }
            Profiler.EndSample();
        }


        // 8.3Kbyte
        {
            Profiler.BeginSample("List default");
            List<int> list5 = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                list5.Add(i);
            }

            Profiler.EndSample();
        }

        // 4Kbyte
        {
            Profiler.BeginSample("List Captacity");
            List<int> list5 = new List<int>(1000);

            for (int i = 0; i < 1000; i++)
            {
                list5.Add(i);
            }

            Profiler.EndSample();
        }

        List<string> tmpList = new List<string>();
        for (int i = 0; i < 100; i++)
        {
            tmpList.Add("hoge");
        }

        // 0byte
        {
            Profiler.BeginSample("foreach List");
            string str = null;
            foreach (var e in tmpList)
            {
                str = e;
            }

            Profiler.EndSample();
        }

        // IEnumeratorへのボックス化がおこる
        // 40byte
        {
            Profiler.BeginSample("foreach IList");
            IList ilist = (IList)tmpList;
            string str = null;
            foreach (string e in ilist)
            {
                str = e;
            }

            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("for IList");
            IList ilist = (IList)tmpList;
            string str = null;
            for (int i = 0; i < ilist.Count; i++)
            {
                str = (string)ilist[i];
            }

            Profiler.EndSample();
        }
    }
}
