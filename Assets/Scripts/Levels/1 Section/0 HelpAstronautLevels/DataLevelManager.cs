using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;
using Section1.MissionsDecorator;

namespace Section1.HelpAstronautLevels.Level0
{
    public sealed class DataLevelManager : Section1.MissionsDecorator.DataLevelManager
    {
        public DataLevelManager(int countRounds) : base(countRounds)
        {
            dataLevel = new DataHome();
            InstantiateData();
            GetDataFromJson($"JsonDataHelpAstronautLevels/JsonDataHelpAstronautLevel{idLvl}");
            GetCommonDataFromJson("JsonDataHelpAstronautLevels", "JsonCommonDataHelpAstrLevels");
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            SpriteDict = dataLevel.SpriteDict;
        }
    }
}
