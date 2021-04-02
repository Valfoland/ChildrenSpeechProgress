﻿using System.Collections;
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
            StartSentence = "Мне нужно заполнить рюкзак. Найди мне среди картинок, где есть ";
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            SpriteDict = dataLevel.SpriteDict;
        }
    }
}