using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChildrenDataInitializer
{
    public AuthenticationManager AuthenticationManager;
    public ChildrenObjectGenerator ChildrenObjectGenerator;
    public GameObject BlockObject;
}

public class ChildrenInitializer
{
    public static List<ChildWithLocalData> ChildDataList = new List<ChildWithLocalData>();
    private ChildrenDataSaver childrenDataSaver;
    private ChildrenDataInitializer childrenDataInitializer;
    private Child defaultChild;

    public void InitData(ChildrenDataInitializer childrenDataInitializer, ChildrenDataSaver childrenDataSaver)
    {
        AttemptCounter.onSetResult = SetResults;
        GameManager.onSetCompletionLevel = SetCompletedLevels;

        this.childrenDataInitializer = childrenDataInitializer;
        this.childrenDataSaver = childrenDataSaver;
    }

    private void SetResults(int result)
    {
        if (Child.CurrentChildWithLocalData != Child.DefaultChildWithLocalData)
        {
            childrenDataSaver.ChildResultsSend(Child.CurrentChildWithLocalData, result);
        }
    }

    private void SetCompletedLevels()
    {
        if (Child.CurrentChildWithLocalData == Child.DefaultChildWithLocalData)
        {
            childrenDataSaver.DefaultChildDataSave(Child.CurrentChildWithLocalData);
        }
        else
        {
            childrenDataSaver.ChildCompletedLevelsSave(ChildDataList, Child.CurrentChildWithLocalData);
        }
    }

    public void StartInitChildren(bool isSuccess)
    {
        childrenDataSaver.StartChildDataRead(InitChildren);
    }

    public void StartInitChildren()
    {
        childrenDataSaver.StartChildDataRead(InitChildren);
    }

    private void InitChildren(List<ChildWithLocalData> remoteChildList)
    {
        childrenDataInitializer.BlockObject.SetActive(false);
        ChildDataList = remoteChildList;
        childrenDataInitializer.ChildrenObjectGenerator.DestroyChildren();

        InitDefaultChildData();

        if (Child.DefaultChildWithLocalData.Id == PlayerPrefs.GetInt(ChildDataConfig.CHOOSE_CHILD) ||
            ChildDataList == null ||
            ChildDataList.Count == 0)
        {
            defaultChild.ClickChooseChild();
        }

        if (ChildDataList != null)
        {
            
            foreach (var childData in ChildDataList)
            {
                var childObject = childrenDataInitializer.ChildrenObjectGenerator.InstantiateChildObject();
                InitChildObject(childObject, childData);

                if (childData.Id == PlayerPrefs.GetInt(ChildDataConfig.CHOOSE_CHILD))
                {
                    childObject.ClickChooseChild();
                }
            }
        }
    }
    
    private void InitDefaultChildData()
    {
        Child.DefaultChildWithLocalData = childrenDataSaver.DefaultChildDataRead();

        if (Child.DefaultChildWithLocalData == null)
        {
            Child.DefaultChildWithLocalData = new ChildWithLocalData
            {
                Id = -1,
                NameChild = ChildDataConfig.DEFAULT_CHILD_NAME,
                GroupeChild = ChildDataConfig.DEFAULT_CHILD_AGE,
                AgeChild = ChildDataConfig.DEFAULT_CHILD_GROUP,
                CompletedLevels = DataGame.GetCompletionLevelsDict(false)
            };

            childrenDataSaver.DefaultChildDataSave(Child.DefaultChildWithLocalData);
        }

        defaultChild = childrenDataInitializer.ChildrenObjectGenerator.InstantiateChildObject();
        InitChildObject(defaultChild, Child.DefaultChildWithLocalData);
    }
    
    private void InitChildObject(Child childObject, ChildWithLocalData childWithLocalData)
    {
        childObject.ChildWithLocalData = childWithLocalData;
        childObject.ChildViewData.Name.text = childWithLocalData.NameChild;
        childObject.ChildViewData.Age.text = childWithLocalData.AgeChild;
        childObject.ChildViewData.GroupName.text = childWithLocalData.GroupeChild;

        if (childWithLocalData.CompletedLevels == null || childWithLocalData.CompletedLevels.Count == 0)
        {
            childWithLocalData.CompletedLevels = DataGame.GetCompletionLevelsDict(false);
        }
    }
}
