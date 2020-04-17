using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataLevels
{
    public bool isCompleted;
}

[System.Serializable]
public class DataMission
{
    public int CountLevels;
    [NonSerialized] public List<DataLevels> CompletedLevels = new List<DataLevels>();
}

[System.Serializable]
public class DataSection
{
    public List<DataMission> CountMissions;
}

public class DataTasks : MonoBehaviour
{
    [SerializeField] private List<DataSection> countSections = new List<DataSection>();
    public static List<DataSection> CountSections;
    public static int IdSelectSection;
    public static int IdSelectMission;
    public static int IdSelectLvl;

    private void Start()
    {
        if (CountSections == null)
        {
            CountSections = SetCompletedLevels(countSections);
        }
    }

    private List<DataSection> SetCompletedLevels(List<DataSection> countSections)
    {
        foreach (var section in countSections)
        {
            foreach (var mission in section.CountMissions)
            {
                for (int i = 0; i < mission.CountLevels; i++)
                {
                    mission.CompletedLevels.Add(new DataLevels());
                }
            }
        }

        return countSections;
    }
}