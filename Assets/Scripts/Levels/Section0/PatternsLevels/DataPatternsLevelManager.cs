using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Section0.PatternsLevel
{
    public class DataPatternsLevel
    {
        public int CountRows;
        public List<List<string>> Words = new List<List<string>>
        {
            new List<string>
            {
                "куст", "газон", "мука", "рука", "гора", 
                "кора", "книга", "звук", "казна", "знак", 
                "гроза", "заказ", "закат", "залог"
            },
            new List<string>
            {
                "куст", "гусь", "луг", "лук", "сок", 
                "квас", "звонок", "сапог", 
                "зрачок", "диск", "писк", "квас", 
                "вкус", "каркас", "скалолаз", "зубы"
            },
            new List<string>
            {
                "забор", "камень", 
                "капуста", "закладка", "зубр", "вздор", "дзюдо",
                "гудок", "город", "радуга", "подвиг"
            },
            new List<string>
            {
                "капуста", "кусок", "ветка", "стакан", 
                "компот", "кот", "горизонт", "доктор", "тайга",
                "тоска", "касатка", "карта", "закат", "салют", 
                "самокат", "такт", "такса", "проспект", "торг",
                "чистка", "топаз", "катод", "стыд", "кадет",
                "медпункт", "студент", "куртка", "стык", "топаз"
            }
        };

        private List<Dictionary<char, Color>> soundsDict = new List<Dictionary<char, Color>>
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
            
        };

        public Dictionary<char, Color> GetColorSound(int levelId)
        {
            Dictionary<char, Color> soundsDict = new Dictionary<char, Color>();
            soundsDict = this.soundsDict[0];

            for (var i = 1; i <= levelId; i++)
            {
                soundsDict = soundsDict
                    .Concat(this.soundsDict[i])
                    .ToDictionary(x => x.Key, x => x.Value);
            }

            return soundsDict;
        }
    }

    public class DataPatternsLevelManager : DataLevelManager
    {
        private DataPatternsLevel dataLevels;
        public List<string> WordsLevel;
        public List<string> WordsWithoutSounds;
        public Dictionary<char, Color> SoundsLevel;
        
        public DataPatternsLevelManager()
        {
            dataLevels = new DataPatternsLevel {CountRows = 4};
            idLvl = DataGame.IdSelectLvl;
            SoundsLevel = dataLevels.GetColorSound(idLvl);
            RemoveSoundsInWords();
        }

        private void RemoveSoundsInWords()
        {
            var shuffleNumbers = dataLevels.Words[idLvl].Count.ShuffleNumbers();
            
            WordsLevel = new List<string>();
            WordsWithoutSounds = new List<string>();
            
            for (int i = 0; i < dataLevels.CountRows; i++)
            {
                string wordLevel  = dataLevels.Words[idLvl][shuffleNumbers[i]];
                string wordWithoutSounds = wordLevel;
                foreach (var dataSound in SoundsLevel)
                {
                    wordWithoutSounds = wordWithoutSounds.Replace(dataSound.Key.ToString(), "_");
                }
                WordsLevel.Add(wordLevel);
                WordsWithoutSounds.Add(wordWithoutSounds);
            }
        }
        
        
    }
}
