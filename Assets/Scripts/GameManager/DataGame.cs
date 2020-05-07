using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataMission
{
    public int CountLevels;
}

[System.Serializable]
public class DataSection
{
    public List<DataMission> CountMissions;
}

public class DataGame : MonoBehaviour
{
    [SerializeField] private List<DataSection> countSections = new List<DataSection>();
    public static List<DataSection> CountSections;
    public static int IdSelectSection;
    public static int IdSelectMission;
    public static int IdSelectLvl;

    private void Start()
    {
        CountSections = countSections;
    }
    
    public static Dictionary<string, List<T>> GetCompletionLevelsDict<T>(T item)
    {
        var completionDict = new Dictionary<string, List<T>>();

        for (int i = 0; i < CountSections.Count; i++)
        {
            for (int j = 0; j < CountSections[i].CountMissions.Count; j++)
            {
                var levelsList = new List<T>();
                for (int k = 0; k < CountSections[i].CountMissions[j].CountLevels; k++)
                {
                    levelsList.Add(item);
                }
                
                completionDict.Add(i.ToString() + j, levelsList);
            }
        }

        return completionDict;
    }
}