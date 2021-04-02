using System.Collections.Generic;
using Levels;
using UnityEngine;

namespace Section0.HomeLevels.Level0
{
    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private Dictionary<string, List<string>> nameDirDict = new Dictionary<string, List<string>>();
        
        public readonly Dictionary<string, Dictionary<string, Sprite>> SpriteDict;
        public readonly Queue<string> LevelKeySpriteQueue = new Queue<string>();
        public Dictionary<int, List<DialogueData>> DialogueDict = new Dictionary<int, List<DialogueData>>();
        public string StartSentence;
        
        public DataLevelManager()
        {
            GetDataFromJson();
            InstantiateData();
            
            SpriteDict = LoadSprites(nameDirDict);

            InitSpriteKeysQueueData();
        }
        
        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels","JsonDataHomeLevel0");
            DialogueDict = LoadDialogueData(dataText["LevelSentences"]);
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

