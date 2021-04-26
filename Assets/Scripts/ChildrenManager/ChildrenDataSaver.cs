using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;

public class ChildrenDataSaver
{
    private Action<List<ChildWithLocalData>> onGetChildren;

    public void StartChildDataRead(Action<List<ChildWithLocalData>> onGetChildren)
    {
        this.onGetChildren = onGetChildren;
        ChildrenNetworkService.Instance.GetChildren(ChildRead);
    }

    private void ChildRead(List<ChildData> remoteChildList)
    {
        List<ChildWithLocalData> childrenDataList =
            JsonConvert.DeserializeObject<List<ChildWithLocalData>>(PlayerPrefs.GetString(ChildDataConfig.CHILDREN));
        
        if (remoteChildList == null)
        {
            onGetChildren?.Invoke(childrenDataList);
            return;
        }
        
        var tempRemoteList = new List<ChildWithLocalData>();
        
        foreach (var childData in remoteChildList)
        {
            tempRemoteList.Add(new ChildWithLocalData
            {
                Id = childData.Id,
                NameChild = childData.NameChild,
                AgeChild = childData.AgeChild,
                GroupeChild = childData.GroupeChild
            });
        }

        if (childrenDataList != null)
        {
            foreach (var remoteChildData in tempRemoteList)
            {
                foreach (var localChildData in childrenDataList)
                {
                    if (remoteChildData.Id != localChildData.Id) continue;
                    remoteChildData.CompletedLevels = localChildData.CompletedLevels;
                    break;
                }
            }
        }

        ChildDataSave(tempRemoteList);
        onGetChildren?.Invoke(tempRemoteList);
    }

    public ChildWithLocalData DefaultChildDataRead()
    {
        var childWithLocalData =
            JsonConvert.DeserializeObject<ChildWithLocalData>(PlayerPrefs.GetString(ChildDataConfig.DEFAULT_CHILD));
        return childWithLocalData;
    }

    public void ChildCompletedLevelsSave(List<ChildWithLocalData> childDataList, ChildWithLocalData childWithLocalData)
    {
        ChildWithLocalData childWithLocalDataMain = childDataList?.FirstOrDefault(x => x.Id == childWithLocalData.Id);
        if (childWithLocalDataMain != null) childWithLocalDataMain.CompletedLevels = childWithLocalData.CompletedLevels;
        ChildDataSave(childDataList);
    }
    
    public void ChildResultsSend(ChildWithLocalData childWithLocalData, int result)
    {
        var currentSection = DataGame.SectionDataList[DataGame.IdSelectSection];
        var currentMission = currentSection.MissionDataList[DataGame.IdSelectMission];
        
        var childResult = new ChildResultData
        {
            TypeSection = currentSection.NameSection,
            TypeMission = currentMission.NameMission,
            PercentResult = result
        };
        
        ChildrenNetworkService.Instance.SetChildResult(childResult, childWithLocalData.Id);
    }

    private void ChildDataSave(List<ChildWithLocalData> childDataList)
    {
        PlayerPrefs.SetString(ChildDataConfig.CHILDREN, JsonConvert.SerializeObject(childDataList));
    }
    
    public void DefaultChildDataSave(ChildWithLocalData childWithLocalData)
    {
        PlayerPrefs.SetString(ChildDataConfig.DEFAULT_CHILD, JsonConvert.SerializeObject(childWithLocalData));
    }
}

