using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Levels;
using UnityEngine;

namespace Section0.PatternsLevel
{
    public class DataLevel
    {
        public int CountRows;

        public Dictionary<string, List<string>> WordDict = new Dictionary<string, List<string>>();

        private Dictionary<char, Color> soundsDict = new Dictionary<char, Color>
        {
            {'а', Color.red},
            {'о', new Color32(255, 128, 0, 255)},
            {'и', Color.green},
            {'е', Color.blue},
            {'ё', Color.yellow},
            {'э', Color.cyan},
            {'ы', Color.magenta},
            {'у', new Color32(200, 150, 150, 255)},
            {'ю', new Color32(200, 150, 250, 255)},
            {'я', new Color32(60, 160, 150, 255)},
        };
        
        /*private List<Dictionary<char, Color>> soundsDict = new List<Dictionary<char, Color>>
        {
            new Dictionary<char, Color>
            { 
                {'к', Color.red}, 
                {'г', new Color32(255,128, 0, 255)}, 
                {'з', Color.red},
            },
            new Dictionary<char, Color>
            {
                {'c', Color.blue},
            },
            new Dictionary<char, Color>
            {
                {'д', Color.yellow},
            },
            new Dictionary<char, Color>
            {
                {'т', new Color32(255,128, 0, 255)}
            },
            
        };*/

        public Dictionary<char, Color> GetColorLetter(int levelId)
        {
            Dictionary<char, Color> soundsDict = new Dictionary<char, Color>();
            soundsDict = this.soundsDict;

            /*for (var i = 1; i <= levelId; i++)
            {
                soundsDict = soundsDict
                    .Concat(this.soundsDict[i])
                    .ToDictionary(x => x.Key, x => x.Value);
            }*/

            return soundsDict;
        }
    }

    /// <summary>
    /// Заметки. Для звука нужно пронумеровать два слова, например, одежда 1, одежда 2 ( Чтобы можно было передать менеджеру
    /// звука параметры типа одежда-одежду.
    /// </summary>
    public class DataLevelManager : Levels.DataLevelManager
    {
        private DataLevel dataLevels;
        public List<string> WordsList;
        public List<string> WordsWithoutLetters;
        public Dictionary<char, Color> SelectableColorLetters;
        
        public DataLevelManager()
        {
            dataLevels = new DataLevel {CountRows = 4};
            idLvl = DataGame.IdSelectLvl;
            SelectableColorLetters = dataLevels.GetColorLetter(idLvl);
            GetDataFromJson();
            RemoveLettersInWords();
        }
        
        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataPatternLevels","JsonDataPatternLevels");
            dataLevels.WordDict = dataText;
        }

        private void RemoveLettersInWords()
        {
            var shuffleNumbers = dataLevels.WordDict["Level" + idLvl].Count.ShuffleNumbers();
            
            WordsList = new List<string>();
            WordsWithoutLetters = new List<string>();
            
            for (int i = 0; i < dataLevels.CountRows; i++)
            {
                string wordLevel  = dataLevels.WordDict["Level" + idLvl][shuffleNumbers[i]];
                string wordWithoutSounds = wordLevel;
                foreach (var dataSound in SelectableColorLetters)
                {
                    wordWithoutSounds = wordWithoutSounds.Replace(dataSound.Key.ToString(), "_");
                }
                WordsList.Add(wordLevel);
                WordsWithoutLetters.Add(wordWithoutSounds);
            }
        }
    }
}
