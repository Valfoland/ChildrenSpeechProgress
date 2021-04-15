using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section0.HomeLevels.Level2
{
    public class LevelManager : LevelProduct
    {
        [SerializeField] private ItemLevel[] itemLevel;
        [SerializeField] private Text textMessage;
        [SerializeField] private int countRounds;
        
        private int currentRound;
        private string needWord;
        private string currentSentence;
        
        private Dictionary<string, Sprite> spriteList = new Dictionary<string, Sprite>();
        private DataLevelManager dataLevelManager;
        
        protected override void Start()
        {
            base.Start();
            
            InitData();
            StartLevel();
        }

        protected override void StartLevel()
        {
            ReshapeItems();
        }

        private void OnDestroy()
        {
            ItemLevel.onClickBox -= CheckBox;
        }
        
        private void InitData()
        {
            ItemLevel.onClickBox += CheckBox;
            voiceButton.onClick.AddListener(ClickButtonVoice);
            dataLevelManager = new DataLevelManager();
        }

        private void SetTextMessage(string msg)
        {
            textMessage.text = msg;
        }
        
        private void ReshapeItems()
        {
            if (currentRound < dataLevelManager.QueueSentenceses.Count)
            {
                currentRound++;
                GetSentence();
                GetRandomSprites();
                Voice(currentSentence);
                int i = 0;
                foreach (var box in itemLevel)
                {
                    box.SetDataBox(spriteList.ToList()[i]);
                    i++;
                }
            }
            else
            {
                currentRound = 0;
                CheckWinLevel();
            }
        }
        
        private void CheckBox(string wordBox, ItemLevel itemLevel)
        {
            if (wordBox == needWord)
            {
                AttemptCounter.SetAttempt(true);
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                itemLevel.AnimBox();
            }
            var newSentence = currentSentence + " " + needWord.ToLower();
            SetTextMessage(newSentence);

            Voice(currentSentence);
            VoiceCallBack(needWord, ReshapeItems);
        }
        
        private void GetSentence()
        {
            currentSentence = dataLevelManager.QueueSentenceses.Dequeue();
            dataLevelManager.QueueSentenceses.Enqueue(currentSentence);
            SetTextMessage(currentSentence);
        }

        private void GetRandomSprites()
        {
            spriteList = dataLevelManager.QueueSprites.Dequeue();
            dataLevelManager.QueueSprites.Enqueue(spriteList);
            bool[] mixStates = {true, false};
            bool toMix = mixStates[Random.Range(0, 2)];

            if (toMix)
            {
                var tempList = spriteList.ToList();
                spriteList.Clear();
                spriteList.Add(tempList[1].Key, tempList[1].Value);
                spriteList.Add(tempList[0].Key, tempList[0].Value);
            }

            needWord = toMix ? spriteList.ToList()[1].Key: spriteList.ToList()[0].Key;
        }
        
        private void ClickButtonVoice()
        {
            Voice(currentSentence);
        }
    }
}
