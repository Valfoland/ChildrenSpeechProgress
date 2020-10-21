using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;

public class ChildrenDataSaver
{
    private Child child;
    public const string COUNT_CHILD = "countChild";
    public const string CHOOSE_CHILD = "chooseChild";
    private const string RESULT_CHILD = "resultChild";
    private const string CHILD = "child";

    public void ChildDataWrite(GameObject childObject, DataAddChildPanel dataAddChildPanel,
        ChildrenData childData)
    {
        child = childObject.GetComponent<Child>();

        if (childData == null)
        {
            child.ChildrenData.IdChild = Child.CountChildren;
            child.ChildrenData.Name = dataAddChildPanel.NameField.text;
            child.ChildrenData.Age = dataAddChildPanel.AgeField.text;
            child.ChildrenData.GroupName = dataAddChildPanel.GroupField.text;
            child.ChildrenData.ResultMission = DataGame.GetCompletionLevelsDict(-1);
            child.ChildrenData.CompletedLevels = DataGame.GetCompletionLevelsDict(false);
        }
        else
        {
            child.ChildrenData = childData;
        }

        child.Name.text = 
            childData == null ? dataAddChildPanel.NameField.text : childData.Name;
        child.Age.text = 
            childData == null ? dataAddChildPanel.AgeField.text : childData.Age;
        child.GroupName.text = 
            childData == null ? dataAddChildPanel.GroupField.text : childData.GroupName;

        if (PlayerPrefs.GetInt(COUNT_CHILD) == 0 ||
            child.ChildrenData.IdChild == PlayerPrefs.GetInt(CHOOSE_CHILD))
        {
            Child.CurrentChildrenData = child.ChildrenData;
        }
    }
    public List<ChildrenData> ChildDataRead()
    {
        List<ChildrenData> childrenDataQueue = new List<ChildrenData>();

        for (int i = 0; i <= PlayerPrefs.GetInt(COUNT_CHILD) && PlayerPrefs.HasKey(i + CHILD); i++)
        {
            childrenDataQueue.Add(
                JsonConvert.DeserializeObject<ChildrenData>(PlayerPrefs.GetString(i + CHILD)));
        }
        
        return childrenDataQueue;
    }

    public void ChildCountSave()
    {
        PlayerPrefs.SetInt(COUNT_CHILD, PlayerPrefs.GetInt(COUNT_CHILD) + 1);
    }
    
    public void ChildDataSave(ChildrenData childrenData = null)
    {
        if (childrenData == null)
        {
            childrenData = child.ChildrenData;
        }

        PlayerPrefs.SetString(childrenData.IdChild + CHILD, JsonConvert.SerializeObject(childrenData));
    }
}

