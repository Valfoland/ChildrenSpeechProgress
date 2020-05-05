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
    public List<DataLevelObjectLevels> DataLevels;
}

[System.Serializable]
public class DataLevelObjectSection
{
    public List<DataLevelObjectMissions> DataLevelsOfMission;
}

public class LevelInstancer : MonoBehaviour
{
    [SerializeField] private List<DataLevelObjectSection> dataLevelsOfSection;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private Text textLvl;

    private GameObject prefabLvl;
    
    private void Start()
    {
        NextLvlPanel.onNextLvl += InstanceLevel;
        InstanceLevel();
    }

    private void OnDestroy()
    {
        NextLvlPanel.onNextLvl -= InstanceLevel;
    }

    private void InstanceLevel()
    {
        if (prefabLvl != null)
        {
            Destroy(prefabLvl);
        }

        SetTextLvl();
        prefabLvl = Instantiate(
            dataLevelsOfSection[DataTasks.IdSelectSection].
                DataLevelsOfMission[DataTasks.IdSelectMission]
                .DataLevels[DataTasks.IdSelectLvl].PrefabLevel,
            parentTransform);
    }
    
    
    private void SetTextLvl()
    {
        Regex regex = new Regex(@"\d");
        textLvl.text = regex.Replace(textLvl.text, $" {DataTasks.IdSelectLvl + 1}");

    }
}
