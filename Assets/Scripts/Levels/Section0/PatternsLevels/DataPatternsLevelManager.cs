using System.Collections;
using System.Collections.Generic;
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

        public List<Dictionary<char, Color>> Sounds = new List<Dictionary<char, Color>>
        {
            new Dictionary<char, Color>
            {
                {'к', Color.red}, 
                {'г', new Color32(255,128, 0, 255)}, 
                {'з', Color.green}
            },
            new Dictionary<char, Color>
            {
                {'к', Color.red}, 
                {'г', new Color32(255,128, 0, 255)}, 
                {'з', Color.green}, 
                {'c', Color.blue}
            },
            new Dictionary<char, Color>
            {
                {'к', Color.red}, 
                {'г', new Color32(255,128, 0, 255)}, 
                {'з', Color.red}, 
                {'c', Color.blue}, 
                {'д', Color.yellow}
            },
            new Dictionary<char, Color>
            {
                {'к', Color.red}, 
                {'г', new Color32(255,128, 0, 255)}, 
                {'з', Color.red}, 
                {'c', Color.blue}, 
                {'д', Color.yellow}, 
                {'т', new Color32(255,128, 0, 255)}
            }
        };
    }

    public class DataPatternsLevelManager : DataLevelManager, ILevelData
    {
        private DataPatternsLevel dataLevels;
        public static List<string> WordsLevel;
        public static List<string> WordsWithoutSounds;
        public static Dictionary<char, Color> SoundsLevel = new Dictionary<char, Color>();
        public void InitData()
        {
            dataLevels = new DataPatternsLevel();
            dataLevels.CountRows = 4;
            idLvl = DataTasks.IdSelectLvl;
            SoundsLevel = dataLevels.Sounds[idLvl];
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
                foreach (var dataSound in dataLevels.Sounds[idLvl])
                {
                    wordWithoutSounds = wordWithoutSounds.Replace(dataSound.Key.ToString(), "_");
                }
                WordsLevel.Add(wordLevel);
                WordsWithoutSounds.Add(wordWithoutSounds);
            }
        }
    }
}
