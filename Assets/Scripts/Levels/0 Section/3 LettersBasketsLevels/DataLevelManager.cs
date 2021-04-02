using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

namespace Section0.LettersBasketsLevels
{
    public class DataLevelManager : Levels.DataLevelManager
    {
        public Dictionary<int, List<DialogueData>> DialogueDict = new Dictionary<int, List<DialogueData>>();
        public Dictionary<int, List<DialogueData>> GameSentencesDict = new Dictionary<int, List<DialogueData>>();
        public Dictionary<string, Dictionary<string, Sprite>> ItemsDataDict;
        
        public DataLevelManager()
        {
            InstantiateData();
            GetDataFromJson();
        }
        
        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var nameItemsList = 
                jsonData.GetData("JsonDataLettersBasketsLevels", $"JsonDataLettersBasketsLevel{idLvl}");
            JsonParserGame<Dictionary<string, string>> jsonCommonData = new JsonParserGame<Dictionary<string, string>>();
            var levelsCommonDataList = 
                jsonCommonData.GetData("JsonDataLettersBasketsLevels", $"JsonDataLettersBasketsCommon");
            
            ReadDialogueData(nameItemsList);
            nameItemsList.Remove("LevelIntroSentences");
            nameItemsList.Remove("LevelSentences");

            if (levelsCommonDataList[$"ItemsWithSpritesLevel{idLvl}"] == "true")
            {
                ItemsDataDict = LoadSprites(nameItemsList);
            }
            else
            {
                ItemsDataDict = LoadSampleData(nameItemsList);
            }
        }

        private void ReadDialogueData(Dictionary<string, List<string>> nameItemsList)
        {
            DialogueDict = LoadDialogueData(nameItemsList["LevelIntroSentences"]);
            GameSentencesDict = LoadDialogueData(nameItemsList["LevelSentences"]);
        }
    }
}
