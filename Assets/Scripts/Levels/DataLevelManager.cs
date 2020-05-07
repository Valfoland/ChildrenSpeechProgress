using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public interface ILevelData
{
    void InitData();
}

public class DataLevelManager
{
    public static Dictionary<string, List<Sprite>> DataLevelDict = new Dictionary<string, List<Sprite>>();
    public static Queue<string> DataNameList = new Queue<string>();
    public static string StartSentence;
    protected int idLvl;

    protected virtual void InstanceData(List<string> nameDir, string nameMission, string startSentence = "")
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
