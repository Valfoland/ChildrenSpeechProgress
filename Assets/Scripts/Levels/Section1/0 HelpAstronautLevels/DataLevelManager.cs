using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;

namespace Section1.HelpAstronautLevels.Level0
{
    public class DataHome
    {
        public string NameMission = "Home";
        public List<string> NameDirList = new List<string>
        {
            "Признаки",
            "Прилагательные",
            "Существительные"
        };
        public List<List<string>> ListSentences = new List<List<string>>
        {
            new List<string>
            {
                "Лежит лентяй на раскладушке, грызет, похрустывая...",
                "Сушки",
                "Пушки"
            },
            
        };
    }

    public class DataLevelManager : Levels.DataLevelManager
    {
        private Sprite needSprite;
        private Sprite otherSprite;
        private DataHome dataHome;
        public Queue<string> QueueSentenceses;
        public Queue<List<Sprite>> QueueSprites;

        public DataLevelManager()
        {
            dataHome = new DataHome();
            InstantiateData(dataHome.NameDirList, dataHome.NameMission);
        }

        protected sealed override void InstantiateData(List<string> nameDirList, string NameMission, string startSentence = "")
        {
            base.InstantiateData(dataHome.NameDirList, NameMission);
            QueueSentenceses = new Queue<string>();
            QueueSprites = new Queue<List<Sprite>>();
            
            foreach (var data in dataHome.ListSentences)
            {
                if (FindElement(data[1], data[2]))
                {
                    QueueSentenceses.Enqueue(data[0]);
                    QueueSprites.Enqueue(new List<Sprite>()
                    {
                        needSprite,
                        otherSprite
                    });
                }
            }
        }

        private bool FindElement(string needItem, string otherItem)
        {
            bool isNeedItem = false;
            bool isOtherItem = false;
            needSprite = null;
            otherSprite = null;
            
            foreach (var data in DataLevelDict)
            {
                foreach (var dataSprite in data.Value)
                {
                    if (needItem == dataSprite.name)
                    {
                        needSprite = dataSprite;
                        isNeedItem = true;
                    }

                    if (otherItem == dataSprite.name)
                    {
                        otherSprite = dataSprite;
                        isOtherItem = true;
                    }

                    if (isNeedItem && isOtherItem)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
