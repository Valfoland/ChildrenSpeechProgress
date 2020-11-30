using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Levels;

namespace Section0.HomeLevels.Level2
{
    public class DataHome
    {
        public string NameMission = "Home";
        public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();

        public List<List<string>> ListSentences = new List<List<string>>
        {
            new List<string>
            {
                "Лежит лентяй на раскладушке, грызет, похрустывая...",
                "Сушки",
                "Пушки"
            },
            new List<string>
            {
                "Тает снег. Течет ручей. На ветвях полно...",
                "Грачей",
                "Врачей"
            },
            new List<string>
            {
                "Белокрылые хозяйки, над волной летают...",
                "Чайки",
                "Сайки"
            },
            new List<string>
            {
                "Положили в плошку...",
                "Кашку",
                "Каску"
            },
            new List<string>
            {
                "Пожарный надевает...",
                "Каску",
                "Кашку"
            },
            new List<string>
            {
                "Сладко спит в берлоге...",
                "Мишка",
                "Миска"
            },
            new List<string>
            {
                "На столе с салатом...",
                "Миска",
                "Мишка"
            },
            new List<string>
            {
                "Кличка нашей кошки...",
                "Машка",
                "Маска"
            },
            new List<string>
            {
                "Кашу следует...",
                "Солить",
                "Шалить"
            },
            new List<string>
            {
                "У борца большая...",
                "Сила",
                "Шило"
            },
            new List<string>
            {
                "В булочной большая...",
                "Сайка",
                "Шайка"
            },
            new List<string>
            {
                "По столу ползёт огромный...",
                "Жук",
                "лук"
            },
            new List<string>
            {
                "Баба Таня чистит...",
                "лук",
                "Жук"
            },
            new List<string>
            {
                "Город называем...",
                "Тверь",
                "Дверь"
            },
            new List<string>
            {
                "В доме открываем...",
                "Дверь",
                "Тверь"
            },
            new List<string>
            {
                "Вот тележка. Это...",
                "Тачка",
                "Дача"
            },
            new List<string>
            {
                "Дом в саду зовётся...",
                "Дача",
                "Тачка"
            },
            new List<string>
            {
                "Я в тетрадке ставлю...",
                "Точку",
                "Дочку"
            },
            new List<string>
            {
                "Мама любит свою...",
                "Дочку",
                "Точку"
            },
            new List<string>
            {
                "Зреет красная...",
                "Калина",
                "Галина"
            },
            new List<string>
            {
                "Мы пришли сегодня в...",
                "Гости",
                "Кости"
            },
            new List<string>
            {
                "Принесли собачке...",
                "Кости",
                "Гости"
            },
            new List<string>
            {
                "Ели вкусную...",
                "Икру",
                "Игру"
            },
            new List<string>
            {
                "Подарили нам...",
                "Игру",
                "Икру"
            },
            new List<string>
            {
                "Затвердела снега...",
                "Корка",
                "Горка"
            },
            new List<string>
            {
                "Будет скользкой наша...",
                "Горка",
                "Корка"
            },
            new List<string>
            {
                "Жаворонка слышен...",
                "Голос",
                "Колос"
            },
            new List<string>
            {
                "Зреет в поле спелый...",
                "Колос",
                "Голос"
            },
            new List<string>
            {
                "Распустились утром...",
                "Розы",
                "Росы"
            },
            new List<string>
            {
                "На цветах сверкают...",
                "Росы",
                "Розы"
            },
            new List<string>
            {
                "Щиплет травушку...",
                "Коза",
                "Коса"
            },
            new List<string>
            {
                "Косит травушку...",
                "Коса",
                "Коза"
            },
            new List<string>
            {
                "Лиза пробовала...",
                "Суп",
                "Зуб"
            },
            new List<string>
            {
                "Заболел у Лизы...",
                "Зуб",
                "Суп"
            }
        };
    }

    public class DataLevelManager : Levels.DataLevelManager
    {
        private Sprite needSprite;
        private Sprite otherSprite;
        private DataHome dataLevel;
        public Queue<string> QueueSentenceses;
        public Queue<List<Sprite>> QueueSprites;

        public DataLevelManager()
        {
            dataLevel = new DataHome();
            GetDataFromJson();
            InstantiateData(dataLevel.NameDirDict, dataLevel.NameMission);
        }
        
        private void GetDataFromJson()
        {
            JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();
            var dataText = jsonData.GetData("JsonDataHomeLevels","JsonDataHomeLevel2");
            dataLevel.NameDirDict = dataText;
        }

        protected sealed override void InstantiateData(
            Dictionary<string, List<string>> nameDirDict, string startSentence = "")
        {
            base.InstantiateData(dataLevel.NameDirDict);
            QueueSentenceses = new Queue<string>();
            QueueSprites = new Queue<List<Sprite>>();

            foreach (var sprite in LevelSpriteDict)
            {
                QueueSentenceses.Enqueue(sprite.Key);

                QueueSprites.Enqueue(new List<Sprite>()
                {
                    sprite.Value[0],
                    sprite.Value[1]
                });
            }
        }
    }
}
