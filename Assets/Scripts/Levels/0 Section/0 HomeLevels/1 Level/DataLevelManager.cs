using System.Collections.Generic;
using Levels;
using UnityEngine;

namespace Section0.HomeLevels.Level1
{
    public class DataLevel
    {
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
        public Dictionary<string, Dictionary<string, Sprite>> SpriteDict = new Dictionary<string, Dictionary<string, Sprite>>();
        public Queue<string> LevelKeySpriteQueue = new Queue<string>();
    }

    public sealed class DataLevelManager : Levels.DataLevelManager
    {
        private DataLevel dataLevel;
        public Dictionary<string, Dictionary<string, Sprite>> SpriteDict;
        public Queue<string> LevelKeySpriteQueue;

        public DataLevelManager()
        {
            dataLevel = new DataLevel();
            GetDataFromJson();
            InstantiateData();
            
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            SpriteDict = dataLevel.SpriteDict;
            LevelKeySpriteQueue = dataLevel.LevelKeySpriteQueue;
            
            InitSpriteKeysQueueData();
        }

        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels","JsonDataHomeLevel1");
            dataLevel.NameDirDict = dataText;
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
