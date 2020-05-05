using System.Collections;
using System.Collections.Generic;
//using System.Text.Json;
using UnityEngine;

public class ChildrenDataSaver
{
    private Child child;
    public const string COUNT_CHILD = "countChild";
    public const string CHOOSE_CHILD = "chooseChild";
    private const string RESULT_CHILD = "resultChild";
    private const string CHILD = "child";

    public void ChildDataWrite(GameObject childObject, DataSetAddChildPanel dataSetAddChildPanel,
        ChildrenData childData)
    {
        child = childObject.GetComponent<Child>();

        if (childData != null)
        {
            child.ChildrenData = childData;
        }
        else
        {
            child.Name.text = child.ChildrenData.Name = dataSetAddChildPanel.NameField.text;
            child.Age.text = child.ChildrenData.Age = dataSetAddChildPanel.AgeField.text;
            child.GroupName.text = child.ChildrenData.GroupName = dataSetAddChildPanel.GroupField.text;
            child.ChildrenData.IdChild = Child.CountChildren;
            child.ChildrenData.ResultMission = Child.ResultDict;
        }

        if (childData != null) Debug.Log(childData.ResultMission.Count);

        if (PlayerPrefs.GetInt(COUNT_CHILD) == 0 ||
            child.ChildrenData.IdChild == PlayerPrefs.GetInt(CHOOSE_CHILD))
        {
            Child.CurrentChildrenData = child.ChildrenData;
        }
    }

    public Queue<ChildrenData> ChildDataRead()
    {
        Queue<ChildrenData> childrenDataQueue = new Queue<ChildrenData>();

        for (int i = 0; i <= PlayerPrefs.GetInt(COUNT_CHILD) && PlayerPrefs.HasKey(i + CHILD); i++)
        {
            /*childrenDataQueue.Enqueue(
                JsonSerializer.Deserialize<ChildrenData>(PlayerPrefs.GetString(i + CHILD)));*/
        }
        Debug.Log(childrenDataQueue.Peek().Name);
        return childrenDataQueue;
    }
    
    public static void InitResultDict()
    {
        Child.ResultDict.Clear();
        for (int i = 0; i < DataTasks.CountSections.Count; i++)
        {
            for (int j = 0; j < DataTasks.CountSections[i].CountMissions.Count; j++)
            {
                List<float> levelsList = new List<float>();
                for (int k = 0; k < DataTasks.CountSections[i].CountMissions[j].CountLevels; k++)
                {
                    levelsList.Add(-1);
                }
                
                Child.ResultDict.Add(i.ToString() + j, levelsList);
            }
        }
    }
    
    public void ChildDataSave(ChildrenData childrenData = null)
    {
        if (childrenData == null)
        {
            childrenData = child.ChildrenData;
        }
        PlayerPrefs.SetInt(COUNT_CHILD, childrenData.IdChild);
        //PlayerPrefs.SetString(childrenData.IdChild + CHILD, JsonSerializer.Serialize(childrenData));
    }
}

