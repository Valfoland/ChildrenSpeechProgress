using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class DataLevelManager
{
    public Dictionary<string, List<Sprite>> DataLevelDict = new Dictionary<string, List<Sprite>>();
    public Queue<string> DataNameList = new Queue<string>();
    public string StartSentence;
    protected int idLvl;

    protected virtual void InstantiateData(List<string> nameDir, string nameMission, string startSentence = "")
    {
        StartSentence = startSentence;
        DataLevelDict.Clear();
        DataNameList.Clear();
        idLvl = DataGame.IdSelectLvl + 1;
        
        try
        {
            Resources.UnloadUnusedAssets();
            foreach (var dir in nameDir)
            {
                DataNameList.Enqueue(dir);
                DataLevelDict.Add(dir, Resources.LoadAll<Sprite>($"{nameMission}/Level{idLvl}/{dir}").ToList());
            }
        }
        catch (DirectoryNotFoundException)
        {
        }
    }
}
