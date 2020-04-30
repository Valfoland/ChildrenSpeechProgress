﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Sounds
{
    public class DataSounds
    {
        public List<string> soundNameList = new List<string>
        {
            "а", "б", "п", "в", "ф", "г", "к", "н", "м", "д", "т",
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
            DontDestroy();
        }

        private void OnDestroy()
        {
            onPlayAudio -= StartAudio;
        }

        private void DontDestroy()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Sounds");

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
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

        private void PlayAudio(string  soundName)
        {
            foreach (var sound in dataSounds.soundNameList)
            {
                if (sound.ToUpper().StartsWith(soundName.ToUpper()) || 
                    sound.ToLower().StartsWith(soundName.ToLower()) ||
                    sound.ToLower().StartsWith(soundName.ToUpper()) || 
                    sound.ToUpper().StartsWith(soundName.ToLower()))
                {
                    Debug.Log($"{sound} {soundName}");
                    ResourceRequest resourceRequest = Resources.LoadAsync<AudioClip>($"Sounds/{sound}");
                    audioSource.clip = resourceRequest.asset as AudioClip;
                    audioSource.Play();
                }
            }
        }
    }

}