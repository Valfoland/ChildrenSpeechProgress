using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class ChildrenManager : MonoBehaviour
{
    private ChildrenDataSaver childSaver = new ChildrenDataSaver();
    public static List<ChildData> ChildDataList = new List<ChildData>();

    [SerializeField] private Child childPrefab;
    [SerializeField] private GameObject parentChild;

    private void Start()
    {
        AttemptCounter.onSetResult += SetResults;
        GameManager.onSetCompletionLevel += SetCompletedLevels;
        InitDefaultChildData();
        InitChildren();
    }

    private void OnDestroy()
    {
        AttemptCounter.onSetResult -= SetResults;
        GameManager.onSetCompletionLevel -= SetCompletedLevels;
    }

    private void SetResults(int result)
    {
        if (Child.CurrentChildData != Child.DefaultChildData)
        {
            childSaver.ChildResultsWrite(Child.CurrentChildData, result);
        }
    }
    
    private void SetCompletedLevels()
    {
        if (Child.CurrentChildData == Child.DefaultChildData)
        {
            childSaver.DefaultChildDataWrite(Child.CurrentChildData);
        }
        else
        {
            childSaver.ChildCompletedLevelsWrite(ChildDataList, Child.CurrentChildData);
        }
    }

    public void InitDefaultChildData()
    {
        Child.DefaultChildData = childSaver.DefaultChildDataRead();
        
        if (Child.DefaultChildData == null)
        {
            Child.DefaultChildData = new ChildData
            {
                IdChild = -1,
                Name = ChildDataConfig.DEFAULT_CHILD_NAME,
                Age = ChildDataConfig.DEFAULT_CHILD_AGE,
                GroupName = ChildDataConfig.DEFAULT_CHILD_GROUP,
                CompletedLevels = DataGame.GetCompletionLevelsDict(false)
            };
            
            childSaver.DefaultChildDataWrite(Child.DefaultChildData);
        }
        
        var childObject = InstantiateChildObject();
        InitChildObject(childObject, Child.DefaultChildData);

        if (ChildDataList == null || ChildDataList.Count == 0)
        {
            childObject.ClickChooseChild();
        }
    }
    
    public void InitChildren()
    {
        ChildDataList = childSaver.ChildDataRead();
        if(ChildDataList == null) return;

        foreach (var childData in ChildDataList)
        {
            var childObject = InstantiateChildObject();
            InitChildObject(childObject, childData);
            
            if (childData.IdChild == PlayerPrefs.GetInt(ChildDataConfig.CHOOSE_CHILD))
            {
                childObject.ClickChooseChild();
            }
        };
    }

    private void InitChildObject(Child childObject, ChildData childData)
    {
        childObject.ChildData = childData;
        childObject.ChildViewData.Name.text = childData.Name;
        childObject.ChildViewData.Age.text = childData.Age;
        childObject.ChildViewData.GroupName.text = childData.GroupName;
        
        if (childData.CompletedLevels == null)
        {
            childData.CompletedLevels = DataGame.GetCompletionLevelsDict(false);
        }
    }

    private Child InstantiateChildObject()
    {
       Child childObject = Instantiate(childPrefab, parentChild.transform);
       childObject.InitChild();
       return childObject;
    } 
}