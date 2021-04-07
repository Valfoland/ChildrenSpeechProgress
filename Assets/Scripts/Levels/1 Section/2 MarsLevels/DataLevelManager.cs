using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;
using Section1.MissionsDecorator;

namespace Section1.MarsLevels.Level0
{
    public class DataLevelManager : Section1.MissionsDecorator.DataLevelManager
    {
        public DataLevelManager(int countRounds) : base(countRounds)
        {
            dataLevel = new DataHome();
            InstantiateData();
            GetDataFromJson($"JsonDataMarsLevels/JsonDataMarsLevel{idLvl}");
            
            StartSentence = "Найди мне среди картинок, где ";
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            SpriteDict = dataLevel.SpriteDict;
        }
    }
}
