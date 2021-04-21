using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;

public class ChildrenDataSaver
{
    public List<ChildData> ChildDataRead()
    {
        List<ChildData> childrenDataList = 
            JsonConvert.DeserializeObject<List<ChildData>>(PlayerPrefs.GetString(ChildDataConfig.CHILDREN));
        
        var hasNetwork = false; //TODO checkNetwork

        if (hasNetwork)
        {
            List<ChildData> remoteChildList = new List<ChildData>(); //TODO read from remoteDatabase
            
            for(int i = 0; i < remoteChildList.Count; i++)
            {
                for(int j = 0; j < childrenDataList.Count; j++)
                {
                    if (remoteChildList[i].IdChild == childrenDataList[j].IdChild)
                    {
                        remoteChildList[i].CompletedLevels = childrenDataList[j].CompletedLevels;
                        break;
                    }
                }
            }

            ChildDataWrite(remoteChildList);
            return remoteChildList;
        }

        return childrenDataList;
    }

    public ChildData DefaultChildDataRead()
    {
        ChildData childData = new ChildData();
        childData =
            JsonConvert.DeserializeObject<ChildData>(PlayerPrefs.GetString(ChildDataConfig.DEFAULT_CHILD));
        return childData;
    }

    public void ChildCompletedLevelsWrite(List<ChildData> childDataList, ChildData childData)
    {
        ChildData childDataMain = childDataList?.FirstOrDefault(x => x.IdChild == childData.IdChild);
        if (childDataMain != null) childDataMain.CompletedLevels = childData.CompletedLevels;
        ChildDataWrite(childDataList);
    }
    
    public void ChildResultsWrite(ChildData childData, int result)
    {
        //TODO команда отправки данных на сервер
    }

    private void ChildDataWrite(List<ChildData> childDataList)
    {
        PlayerPrefs.SetString(ChildDataConfig.CHILDREN, JsonConvert.SerializeObject(childDataList));
        
        //TODO Команда отправки данных на сервер
    }
    
    public void DefaultChildDataWrite(ChildData childData)
    {
        PlayerPrefs.SetString(ChildDataConfig.DEFAULT_CHILD, JsonConvert.SerializeObject(childData));
    }
}

