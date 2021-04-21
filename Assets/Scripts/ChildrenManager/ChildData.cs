using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildData
{
    public int IdChild;
    public string Name;
    public string Age;
    public string GroupName;
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