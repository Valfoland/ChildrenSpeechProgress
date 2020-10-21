using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Section0.HomeLevels
{
    public class DataHomeLevel3
    {
        public string NameMission = "Home";
        public List<string> NameDirList = new List<string>
        {
            "images"
        };
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

    public class DataHomeLevel3Manager : DataLevelManager
    {
        private Sprite needSprite;
        private Sprite otherSprite;
        private DataHomeLevel3 dataHomeLevel3;
        public Queue<string> QueueSentenceses;
        public Queue<List<Sprite>> QueueSprites;

        public DataHomeLevel3Manager()
        {
            dataHomeLevel3 = new DataHomeLevel3();
            InstantiateData(dataHomeLevel3.NameDirList, dataHomeLevel3.NameMission);
        }

        protected sealed override void InstantiateData(List<string> nameDirList, string NameMission, string startSentence = "")
        {
            base.InstantiateData(dataHomeLevel3.NameDirList, NameMission);
            QueueSentenceses = new Queue<string>();
            QueueSprites = new Queue<List<Sprite>>();
            
            foreach (var data in dataHomeLevel3.ListSentences)
            {
                if (FindElement(data[1], data[2]))
                {
                    QueueSentenceses.Enqueue(data[0]);
                    QueueSprites.Enqueue(new List<Sprite>()
                    {
                        needSprite,
                        otherSprite
                    });
                }
            }
        }

        private bool FindElement(string needItem, string otherItem)
        {
            bool isNeedItem = false;
            bool isOtherItem = false;
            needSprite = null;
            otherSprite = null;
            
            foreach (var data in DataLevelDict)
            {
                foreach (var dataSprite in data.Value)
                {
                    if (needItem == dataSprite.name)
                    {
                        needSprite = dataSprite;
                        isNeedItem = true;
                    }

                    if (otherItem == dataSprite.name)
                    {
                        otherSprite = dataSprite;
                        isOtherItem = true;
                    }

                    if (isNeedItem && isOtherItem)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
