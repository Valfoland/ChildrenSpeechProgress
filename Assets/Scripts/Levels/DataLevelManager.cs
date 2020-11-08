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
        private static int countItems;

        [MenuItem("Window/DataLevel")]
        private static void Init()
        {
            DataLevelManagerEditor window = (DataLevelManagerEditor)GetWindow(typeof(DataLevelManagerEditor));
            window.Show();
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
            for (int i = 0; i < NameItems.Count; i++)
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
                        SaveDataToJson();
                    }
                }
            }
        }

        private void SaveDataToJson()
        {
            var dir = Directory.GetDirectories(Application.dataPath + "/Resources" + "/Home");

            foreach (var d in dir)
            {
                var files = Directory.GetFiles(d).Where(x => Path.GetExtension(x) != ".meta").ToArray();
            }
        }
    }
}

