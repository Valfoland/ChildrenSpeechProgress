using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс счетчик попыток+назначения балла за попытки
/// </summary>
public class AttemptCounter
{
    public static System.Action<int> onSetResult;
    
    private static Dictionary<string, float> rateDict = new Dictionary<string, float>()
    {
        {"perfect", 1},
        {"good", 0.5f},
        {"sat", 0.25f},
        {"bad", 0}
    };

    private static List<float> attemptResultList = new List<float>();
    private static int attempCount;
    private const float REQ_RESULT = 0.85f;
    
    public static void SetAttempt(bool isTrueAttempt)
    {
        if (isTrueAttempt)
        {
            switch (attempCount)
            {
                case 0 : 
                    attemptResultList.Add(rateDict["perfect"]);
                    break;
                case 1 :
                    attemptResultList.Add(rateDict["good"]);
                    break;
                case 2 :
                    attemptResultList.Add(rateDict["sat"]);
                    break;
                default:
                    attemptResultList.Add(rateDict["bad"]);
                    break;
            }
            attempCount = 0;
        }
        else
        {
            attempCount++;
        }
    }

    private static void SetResultList(float result)
    {
        /*Child.CurrentChildData.CurrentResultMissions[
            DataGame.IdSelectSection.ToString() + DataGame.IdSelectMission][DataGame.IdSelectLvl] = (int)(result * 100f);*/
        onSetResult?.Invoke((int)(result * 100f));
    }

    public static bool IsLevelPass()
    {
        float result = 0;
        attemptResultList.ForEach(x => result += x);
        result /= attemptResultList.Count;
        attemptResultList.Clear();
        SetResultList(result);
        return result >= REQ_RESULT;
    }
}
