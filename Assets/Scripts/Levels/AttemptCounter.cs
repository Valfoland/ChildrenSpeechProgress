using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс счетчик попыток+назначения балла за попытки
/// </summary>
public class AttemptCounter : MonoBehaviour
{
    private static Dictionary<string, float> rateDict = new Dictionary<string, float>()
    {
        {"perfect", 1},
        {"good", 0.5f},
        {"sat", 0.25f},
        {"bad", 0}
    };
    public static List<float> AttemptResultList = new List<float>();
    private static int attempCount;
    private const float REQ_RESULT = 0.85f;
    
    public static void SetAttempt(bool isTrueAttempt)
    {
        if (isTrueAttempt)
        {
            switch (attempCount)
            {
                case 0 : 
                    AttemptResultList.Add(rateDict["perfect"]);
                    break;
                case 1 :
                    AttemptResultList.Add(rateDict["good"]);
                    break;
                case 2 :
                    AttemptResultList.Add(rateDict["sat"]);
                    break;
                default:
                    AttemptResultList.Add(rateDict["bad"]);
                    break;
            }
            attempCount = 0;
        }
        else
        {
            attempCount++;
        }
    }


    public static bool IsLevelPass()
    {
        float result = 0;
        AttemptResultList.ForEach(x => result += x);
        result /= AttemptResultList.Count;
        Section0.ResultKeeper.SetResultSession(result);
        return result >= REQ_RESULT;
    }
}
