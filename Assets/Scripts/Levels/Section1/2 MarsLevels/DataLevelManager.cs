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
            GetDataFromJson($"JsonDataMarsLevels/JsonDataMarsLevel{idLvl}");
            StartSentence = "Найди мне среди картинок, где есть  ";
            InstantiateData(dataLevel.NameDirDict);
        }

        protected override void InstantiateData(Dictionary<string, List<string>> nameDirDict, string startSentence = "")
        {
            base.InstantiateData(nameDirDict, startSentence);
        }
    }
}
