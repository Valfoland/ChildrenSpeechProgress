using System;
using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.HomeLevels.Level0
{
    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private Dictionary<string, List<string>> nameDirDict = new Dictionary<string, List<string>>();
        public readonly Dictionary<string, Dictionary<string, Sprite>> SpriteDict;
        public readonly Queue<string> LevelKeySpriteQueue = new Queue<string>();
        public Dictionary<int, List<DialogueData>> DialogueIntroDict = new Dictionary<int, List<DialogueData>>();
        public Dictionary<int, List<DialogueData>> DialogueGameDict = new Dictionary<int, List<DialogueData>>();

        public DataLevelManager()
        {
            GetDataFromJson();
            InstantiateData();
            
            SpriteDict = LoadSprites(nameDirDict);

            InitSpriteKeysQueueData();
        }

        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData =
                new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels", "JsonDataHomeLevel0");

            DialogueIntroDict = LoadDialogueData(dataText["LevelIntroSentences"]);
            DialogueGameDict = LoadDialogueData(dataText["LevelSentences"]);

            dataText.Remove("LevelIntroSentences");
            dataText.Remove("LevelSentences");
            nameDirDict = dataText;
        }

        private void InitSpriteKeysQueueData()
        {
            foreach (var spriteKey in SpriteDict.Keys)
            {
                LevelKeySpriteQueue.Enqueue(spriteKey);
            }
        }
    }
}

