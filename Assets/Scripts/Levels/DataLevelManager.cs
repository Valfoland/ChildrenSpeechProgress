using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace  Levels
{
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
            idLvl = DataGame.IdSelectLvl;
        
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

    public class DataLevelManagerEditor : EditorWindow
    {
        private Vector2 scrollPos;
        public static List<string> NameItems = new List<string>();
        private Dictionary<string, List<string>> dictFiles =new Dictionary<string, List<string>>();
        private static int countItems;

        [MenuItem("Window/DataLevel")]
        private static void Init()
        {
            DataLevelManagerEditor window = (DataLevelManagerEditor)GetWindow(typeof(DataLevelManagerEditor));
            window.Show();

            
            countItems = PlayerPrefs.GetInt("DataCountLevelPath");
            NameItems = JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString("DataLevelPath"));

            if (NameItems == null)
            {
                NameItems = new List<string>();
            }
        }
        
        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            GetCountPath();
            GetNamesPath();
            ClickSaveDataToJson();
            EditorGUILayout.EndScrollView();
        }

        private void GetCountPath()
        {
            try
            {
                EditorGUILayout.BeginHorizontal("Box");
                countItems = int.Parse(EditorGUILayout.TextField("Количество путей: ",countItems.ToString(),  GUILayout.Height(20)));
                if (GUILayout.Button("Принять", GUILayout.Height(20), GUILayout.Width(150)))
                {
                    NameItems.Clear();
                    for(int i = 0 ; i < countItems; i++)
                        NameItems.Add("");
                }
                EditorGUILayout.EndHorizontal();
            }
            catch (FormatException)
            { }
        }

        private void GetNamesPath()
        {
            for (int i = 0; i < NameItems?.Count; i++)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal("box");
                
                NameItems[i] = EditorGUILayout.TextField("Путь к директории: ", NameItems[i],
                    GUILayout.Height(20));
                
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
        
        private void ClickSaveDataToJson()
        {
            if (GUILayout.Button("Save Data To Json", GUILayout.Height(40)))
            {
                bool isConfirm = EditorUtility.DisplayDialog(
                    "Warning",
                    "Are you sure?",
                    "Ok",
                    "Cancel");

                if (isConfirm)
                {
                    foreach (var s in NameItems)
                    {
                        SaveJsonFile();
                    }

                    PlayerPrefs.SetString("DataLevelPath", JsonConvert.SerializeObject(NameItems));
                    PlayerPrefs.SetInt("DataCountLevelPath", countItems);
                }
            }
        }

        private void SaveJsonFile()
        {
            foreach (var name in NameItems)
            {
                dictFiles.Clear();
                var path = Application.dataPath + "/Resources/" + name;
                var dir = Directory.GetDirectories(path);

                SetDirectories(path, dir);
                File.WriteAllText(path + "/JsonData.txt", JsonConvert.SerializeObject(dictFiles));
                AssetDatabase.Refresh();
            }
        }

        private void SetDirectories(string path, string[] dir)
        {
            if (dir.Length != 0)
            {
                foreach (var d in dir)
                {
                    SetFiles(d);
                }
            }
            else
            {
                SetFiles(path);
            }
        }

        private void SetFiles(string dir)
        {
            var dirName = GetName(dir);
            var files = Directory.GetFiles(dir).Where(x => Path.GetExtension(x) != ".meta").ToList();
            var listFiles = new List<string>();
            
            foreach (var f in files)
            {
                listFiles.Add(GetName(f));
            }
            
            dictFiles.Add(dirName, listFiles);
        }
        
        private string GetName(string oldString)
        {
            var name = oldString.Replace('\\', '/');
            name = name.Replace(".png", "");
            name = name.Replace(".mp3", "");
            return name.Substring(name.LastIndexOf('/') + 1);
        }
    }
}

