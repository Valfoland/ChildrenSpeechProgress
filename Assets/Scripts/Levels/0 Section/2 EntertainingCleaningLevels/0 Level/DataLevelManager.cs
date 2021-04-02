using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;
using Section0.EntertainingCleaningLevels.MissionsDecorator;

namespace Section0.EntertainingCleaningLevels.Level0
{
    public sealed class DataLevelManager : MissionsDecorator.DataLevelManager
    {
        public DataLevelManager(int countRounds) : base(countRounds)
        {
            dataLevel = new MissionsDecorator.DataLevel();
            InstantiateData();
            GetDataFromJson("JsonDataEntertainingCleaningLevels", $"JsonDataEntertainingCleaningLevel{idLvl}");
            
            dataLevel.SpriteDict = LoadSprites(dataLevel.NameDirDict);
            SpriteDict = dataLevel.SpriteDict;
        }
    }
}
