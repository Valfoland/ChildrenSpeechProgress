using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MathExtensions
{
    public static int[] ShuffleNumbers(this int countNumbers)
    {
        int[] perm = Enumerable.Range(0, countNumbers).ToArray(); 

        for (int i = countNumbers - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = perm[j];
            perm[j] = perm[i];
            perm[i] = temp;
        }
        
        return perm;
    }
}
