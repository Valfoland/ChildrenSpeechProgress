using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace Levels
{
    public class DialogueData
    {
        public int Id;
        public string DialogueText = "";
        public string AnswerText = "";
        public string NumberSentence = "";
    }

    public class DataLevelManager
    {
        protected int idLvl;

        protected virtual void InstantiateData()
        {
            idLvl = DataGame.IdSelectLvl;
        }

        protected Dictionary<string, Dictionary<string, Sprite>> LoadSprites(Dictionary<string, List<string>> nameDir)
        {
            Resources.UnloadUnusedAssets();
            var resultSpriteDict = new Dictionary<string, Dictionary<string, Sprite>>(); 
            
            foreach (var dir in nameDir)
            {
                var tempSpriteDict = new Dictionary<string, Sprite>();
                
                foreach (var spriteName in dir.Value)
                {
                    var sprite = Resources.Load<Sprite>($"Images/{spriteName}");
                    var templateSprite = Resources.Load<Sprite>("Images/TemplateSprite");
                    
                    tempSpriteDict.Add(spriteName, sprite != null ? sprite : templateSprite);
                    //if(sprite == null) Debug.Log(spriteName);
                }

                resultSpriteDict.Add(dir.Key, tempSpriteDict);
            }

            return resultSpriteDict;
        }
        
        protected Dictionary<string, Dictionary<string, Sprite>> LoadSampleData(Dictionary<string, List<string>> nameDir)
        {
            var resultSpriteDict = new Dictionary<string, Dictionary<string, Sprite>>(); 
            
            foreach (var dir in nameDir)
            {
                var tempSpriteDict = new Dictionary<string, Sprite>();
                
                foreach (var spriteName in dir.Value)
                {
                    tempSpriteDict.Add(spriteName, null);
                }

                resultSpriteDict.Add(dir.Key, tempSpriteDict);
            }

            return resultSpriteDict;
        }
        
        
        protected Dictionary<int, List<DialogueData>> LoadDialogueData(List<string> nameItemsList)
        {
            var resultDict = new Dictionary<int, List<DialogueData>>();

            foreach (var item in nameItemsList)
            {
                string[] arrayString = item.Split('*');
                DialogueData dialogueData = new DialogueData();

                try
                {
                    dialogueData.Id = int.Parse(arrayString[0]);
                    dialogueData.DialogueText = arrayString[1];
                    dialogueData.AnswerText = arrayString[2];
                }
                catch (IndexOutOfRangeException)
                { }

                CheckCorrespondStrings(resultDict, dialogueData);
            }

            return resultDict;
        }

        private void CheckCorrespondStrings(Dictionary<int, List<DialogueData>> resultDict, DialogueData dialogueData)
        {
            if (!resultDict.ContainsKey(dialogueData.Id))
            {
                resultDict.Add(dialogueData.Id, new List<DialogueData>());
            }
            resultDict[dialogueData.Id].Add(dialogueData);
        }
    }

#if UNITY_EDITOR
    public class DataLevelManagerEditor : EditorWindow
    {
        private Vector2 scrollPos;
        public static List<string> NameItems = new List<string>();
        private Dictionary<string, List<string>> dictFiles = new Dictionary<string, List<string>>();
        private static int countItems;

        [MenuItem("Window/DataLevel")]
        private static void Init()
        {
            DataLevelManagerEditor window = (DataLevelManagerEditor) GetWindow(typeof(DataLevelManagerEditor));
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
                countItems = int.Parse(EditorGUILayout.TextField("Количество путей: ", countItems.ToString(),
                    GUILayout.Height(20)));
                if (GUILayout.Button("Принять", GUILayout.Height(20), GUILayout.Width(150)))
                {
                    NameItems.Clear();
                    for (int i = 0; i < countItems; i++)
                        NameItems.Add("");
                }

                EditorGUILayout.EndHorizontal();
            }
            catch (FormatException)
            {
            }
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

                SetFiles(path);
                File.WriteAllText(path + "/JsonData.json", JsonConvert.SerializeObject(dictFiles));
                AssetDatabase.Refresh();
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
#endif
}