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
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();
        public Dictionary<string, Dictionary<string, Sprite>> SpriteDict =
            new Dictionary<string, Dictionary<string, Sprite>>();
    }

    public class DataLevelManager : Levels.DataLevelManager
    {
        private Sprite needSprite;
        private Sprite otherSprite;
        private DataHome dataLevel;
        public Queue<string> QueueSentences;
        public Queue<Dictionary<string, Sprite>> QueueSprites;

        public DataLevelManager()
        {
            dataLevel = new DataHome();
            GetDataFromJson();
            InstantiateData();
        }

        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData =
                new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels", "JsonDataHomeLevel2");
            dataLevel.NameDirDict = dataText;
        }

        protected sealed override void InstantiateData()
        {
            base.InstantiateData();
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            QueueSentences = new Queue<string>();
            QueueSprites = new Queue<Dictionary<string, Sprite>>();

            var arrayShuffle = dataLevel.SpriteDict.Count.ShuffleNumbers();
            
            for (int i = 0; i < dataLevel.SpriteDict.Count; i++)
            {
                QueueSentences.Enqueue(dataLevel.SpriteDict.ToList()[i].Key);

                try
                {
                    QueueSprites.Enqueue(dataLevel.SpriteDict.ToList()[i].Value);
                }
                catch (ArgumentOutOfRangeException e)
                {

                }
            }
        }
    }
}
