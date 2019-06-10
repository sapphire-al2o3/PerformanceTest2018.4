using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ArrayTest : MonoBehaviour
{
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
        Profiler.BeginSample("List<int> 2");
        List<int> list2 = new List<int>();
        list2.Add(1);
        list2.Add(2);
        list2.Add(3);
        list2.Add(4);
        Profiler.EndSample();

        // 88byte
        Profiler.BeginSample("List<int>(4)");
        List<int> list3 = new List<int>(4);
        Profiler.EndSample();

        // 78.2KB
        // 32 + 40 + 10000 * 8
        Profiler.BeginSample("List<string>(10000)");
        List<string> list4 = new List<string>(10000);
        Profiler.EndSample();
    }

}
