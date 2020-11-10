using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Sounds
{
    public class DataSounds
    {
        public List<string> soundNameList = new List<string>
        {
            //Level 1 HomeMission
            "а", "б", "п", "в", "ф", "г", "к", "н", "м", "д", "т",
            
            //Level 3 HomeMission
            "Привет! Все карточки",
            "Звук",
            "Лежит лентяй на раскладушке",
            "Сушки",
            "Пушки",
            "Тает снег",
            "Грачей",
            "Врачей",
            "Белокрылые хозяйки",
            "Чайки",
            "Сайки",
            "Положили в плошку",
            "Кашку",
            "Каску",
            "Пожарный надевает",
            "Сладко спит в берлоге",
            "Мишка",
            "Миска",
            "На столе с салатом",
            "Кличка нашей кошки",
            "Машка",
            "Маска",
            "Кашу следует",
            "Солить",
            "Шалить",
            "У борца большая",
            "Сила",
            "Шило",
            "В булочной большая",
            "Сайка",
            "Шайка",
            "По столу ползёт огромный",
            "Жук",
            "Лук",
            "Баба Таня чистит",
            "Город называем",
            "Тверь",
            "Дверь",
            "В доме открываем",
            "Вот тележка",
            "Тачка",
            "Дача",
            "Дом в саду зовётся",
            "Я в тетрадке ставлю",
            "Точку",
            "Дочку",
            "Мама любит свою",
            "Зреет красная",
            "Калина",
            "Галина",
            "Мы пришли сегодня в",
            "Гости",
            "Кости",
            "Принесли собачке",
            "Ели вкусную",
            "Икру",
            "Игру",
            "Подарили нам",
            "Затвердела снега",
            "Корка",
            "Горка",
            "Будет скользкой наша",
            "Жаворонка слышен",
            "Голос",
            "Колос",
            "Зреет в поле спелый",
            "Распустились утром",
            "Розы",
            "Росы",
            "На цветах сверкают",
            "Щиплет травушку",
            "Коза",
            "Коса",
            "Косит травушку",
            "Лиза пробовала",
            "Суп",
            "Зуб",
            "Заболел у Лизы",
            
            //Level 1-4 Pattern Mission
            "куст", "кора", "гора", "газон", "мука", "рука", 
            "книга", "звук", "казна", "знак", 
            "гроза", "заказ", "закат", "залог", 
            
            "гусь", "луг", "сок", 
            "квас", "звонок", "сапог", 
            "зрачок", "диск", "писк", 
            "вкус", "каркас", "скалолаз", "зубы",
            
            "забор", "камень", 
            "капуста", "закладка", "зубр", "вздор", "дзюдо",
            "гудок", "город", "радуга", "подвиг",
            
            "кусок", "ветка", "стакан", 
            "компот", "кот", "горизонт", "доктор", "тайга",
            "тоска", "касатка", "карта", "салют", 
            "самокат", "такт", "такса", "проспект", "торг",
            "чистка", "топаз", "катод", "стыд", "кадет",
            "медпункт", "студент", "куртка", "стык"
        };
    }
    
    /// <summary>
    /// Класс-дата аудиофайлов для приложения
    /// </summary>
    public class SoundSource: MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private static AudioSource audioSourceMain;
        private static Action<string> onPlayAudio;
        private DataSounds dataSounds;
        private bool canPlayAudio;
        
        private void Start()
        {
            onPlayAudio += StartAudio;
            dataSounds = new DataSounds();
        }

        private void OnDestroy()
        {
            onPlayAudio -= StartAudio;
        }
        
        public static void VoiceSound(string sentence)
        {
            onPlayAudio?.Invoke(sentence);
        }

        private void StartAudio(string  soundName)
        {
            StartCoroutine(WaitAudio(soundName));
            audioSourceMain = audioSource;
        }

        private IEnumerator WaitAudio(string soundName)
        {
            while (audioSourceMain != null && audioSourceMain.isPlaying)
            {
                yield return null;
            }

            PlayAudio(soundName);
        }

        private void PlayAudio(string soundName)
        {
            int percentMax = 0;
            string actualSound = "";
            
            foreach (var sound in dataSounds.soundNameList)
            {
                if (soundName.ToLower().StartsWith(sound.ToLower()))
                {
                    GetActualSound(ref actualSound, ref percentMax, sound, soundName);
                    
                    ResourceRequest resourceRequest = Resources.LoadAsync<AudioClip>($"Sounds/{soundName}");
                    audioSource.clip = resourceRequest.asset as AudioClip;
                    audioSource.Play();

                }
            }
        }
        
        private void GetActualSound(ref string actualSound, ref int percentMax, string sound, string soundName)
        {
            string soundTempBigger = sound.Length > soundName.Length ? sound : soundName;
            string soundTempSmaller = sound.Length <= soundName.Length ? sound : soundName;

            int counTrueSound = 0;

            for (int i = 0; i < soundTempBigger.Length; i++)
            {
                if (i < soundTempSmaller.Length &&
                    soundTempBigger[i] == soundTempSmaller[i])
                {
                    counTrueSound++;
                }
                else
                {
                    break;
                }
            }

            if (percentMax <= MathExtensions.CalculatePercent(counTrueSound, soundTempBigger.Length))
            {
                percentMax = MathExtensions.CalculatePercent(counTrueSound, soundTempBigger.Length);
                actualSound = sound;
            }
        }
    }
   
}
