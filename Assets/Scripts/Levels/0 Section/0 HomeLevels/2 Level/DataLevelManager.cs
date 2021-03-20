using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;

namespace Section0.HomeLevels.Level2
{
    public class DataHome
    {
        public string NameMission = "Home";
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
    }

    public class DataLevelManager : Levels.DataLevelManager
    {
        private Sprite needSprite;
        private Sprite otherSprite;
        private DataHome dataLevel;
        public Queue<string> QueueSentenceses;
        public Queue<Dictionary<string, Sprite>> QueueSprites;

        public DataLevelManager()
        {
            dataLevel = new DataHome();
            GetDataFromJson();
            InstantiateData(dataLevel.NameDirDict, dataLevel.NameMission);
        }
        
        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels","JsonDataHomeLevel2");
            dataLevel.NameDirDict = dataText;
        }

        protected sealed override void InstantiateData(
            Dictionary<string, List<string>> nameDirDict, string startSentence = "")
        {
            base.InstantiateData(dataLevel.NameDirDict);
            QueueSentenceses = new Queue<string>();
            QueueSprites = new Queue<Dictionary<string, Sprite>>();
            
            foreach (var sprite in LevelSpriteDict)
            {
                QueueSentenceses.Enqueue(sprite.Key);

                try
                {
                    QueueSprites.Enqueue(sprite.Value);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    
                }
            }
        }
    }
}
