using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedDict<T1, T2> : IEnumerable
{
    int count = 0;

    public int Count
    {
        get => count;
    }

    T1[] mass = new T1[1];
    T2[] mass1 = new T2[1];

    int position = -1;
    int pos = -1;

    public void Clear()
    {
        mass = new T1[1];
        mass1 = new T2[1];
        count = 0;
        pos = -1;
    }
    
    public void Add(T1 mass, T2 mass1)
    {
        count++;
        Array.Resize(ref this.mass, count);
        pos++;
        this.mass[pos] = mass;

    }

    public IEnumerator GetEnumerator()
    {
        return mass.GetEnumerator();
    }

    public T1 this[int index]
    {
        get { return mass[index]; }
        set { mass[index] = value; }
    }
}

