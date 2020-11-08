using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOnr
{
    Onr1,
    Onr2,
    Onr3
}

[System.Serializable]
public class DataLevel
{
    public string TextLevel;
}

[System.Serializable]
public class DataMission
{
    [SerializeField] private string nameMission;
    public string TextMission;
    public List<DataLevel> LevelDataList;
}

[System.Serializable]
public class DataSection
{
    public string NameSection;
    public string TextSection;
    public TypeOnr TypeOnr;
    public List<DataMission> MissionDataList;
}

public class DataGame : MonoBehaviour
{
    [SerializeField] private List<DataSection> sectionDataList = new List<DataSection>();
    public static List<DataSection> SectionDataList;
    public static int IdSelectSection;
    public static int IdSelectMission;
    public static int IdSelectLvl;
    public static TypeOnr TypeOnr;

    private void Start()
    {
        SectionDataList = sectionDataList;
    }

    public static Dictionary<string, List<T>>  GetCompletionLevelsDict<T>(T item)
    {
        var completionDict = new Dictionary<string, List<T>>();

            for (int b = 0; b < SectionDataList.Count; b++)
            {
                for (int c = 0; c < SectionDataList[b].MissionDataList.Count; c++)
                {
                    var levelsList = new List<T>();
                        for (int d = 0; d < SectionDataList[b].MissionDataList[c].LevelDataList.Count; d++)
                        {
                            levelsList.Add(item);
                        }
                        completionDict.Add(b.ToString() + c, levelsList);
                }
            }

        return completionDict;
    }
}