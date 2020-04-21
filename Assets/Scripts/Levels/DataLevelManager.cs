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
    protected int idLvl;

    protected virtual void InstanceData()
    {
        DataLevelDict.Clear();
        DataNameList.Clear();
        idLvl = DataTasks.IdSelectLvl + 1;
        try
        {
            var dirLevel = Directory.GetDirectories("Assets/Resources/Home/Level" + idLvl);
            foreach (var dir in dirLevel)
            {
                var dirTemp = dir.Replace("Assets/Resources/Home/Level" + idLvl + @"\", "");
                DataNameList.Enqueue(dirTemp);
                DataLevelDict.Add(dirTemp, Resources.LoadAll<Sprite>($"Home/Level{idLvl}/{dirTemp}").ToList());
            }
        }
        catch (DirectoryNotFoundException)
        {
        }
    }
}
