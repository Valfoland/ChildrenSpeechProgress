using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildData
{
    public int Id;
    public string NameChild;
    public string AgeChild;
    public string GroupeChild;
}

public class ChildResultData
{
    public string TypeONR = "";
    public string TypeSection { get; set; }
    public string TypeMission { get; set; }
    public int PercentResult { get; set; }
}

public class ChildWithLocalData : ChildData
{
    public Dictionary<string, List<bool>> CompletedLevels;
}


[System.Serializable]
public class ChildViewData
{
    public Text Name;
    public Text Age;
    public Text GroupName;
    public Image ChildCheckMarkImage;
}