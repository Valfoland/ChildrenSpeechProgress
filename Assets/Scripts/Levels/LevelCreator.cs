using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class DataLevelObjectLevels
{
    public GameObject PrefabLevel;
}

[System.Serializable]
public class DataLevelObjectMissions
{
    [Header("Список уровней")]
    public List<DataLevelObjectLevels> DataLevels;
}

[System.Serializable]
public class DataLevelObjectSection
{
    [Header("Список миссий")]
    public List<DataLevelObjectMissions> DataLevelsOfMission;
}

public class LevelCreator : MonoBehaviour
{
    [Header("Список разделов")]
    [SerializeField] protected List<DataLevelObjectSection> dataLevelsOfSection;
    [SerializeField] protected Transform parentTransform;
    [SerializeField] protected Text textLvl;
    protected GameObject prefabLvl;
    
    public void CreateLevel()
    {
        if (prefabLvl != null)
        {
            Destroy(prefabLvl);
        }

        SetTextLvl();
        prefabLvl = Instantiate(
            dataLevelsOfSection[DataGame.IdSelectSection].
                DataLevelsOfMission[DataGame.IdSelectMission]
                .DataLevels[DataGame.IdSelectLvl].PrefabLevel,
            parentTransform);
    }
    
    private void SetTextLvl()
    {
        Regex regex = new Regex(@"\d");
        textLvl.text = regex.Replace(textLvl.text, $" {DataGame.IdSelectLvl + 1}");
    }
}
