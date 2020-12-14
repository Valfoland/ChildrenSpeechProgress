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

            for (int a = 0; a < SectionDataList.Count; a++)
            {
                for (int b = 0; b < SectionDataList[a].MissionDataList.Count; b++)
                {
                    var levelsList = new List<T>();
                        for (int c = 0; c < SectionDataList[a].MissionDataList[b].LevelDataList.Count; c++)
                        {
                            levelsList.Add(item);
                        }
                        completionDict.Add(a.ToString() + b, levelsList);
                }
            }

        return completionDict;
    }
}