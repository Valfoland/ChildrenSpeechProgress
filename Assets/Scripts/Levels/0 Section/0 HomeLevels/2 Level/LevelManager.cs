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
        
        private int currentIdPack;
        private int countNeedSprite;
        private string needWord;
        private string currentSentence;
        
        private Dictionary<string, Sprite> spriteList = new Dictionary<string, Sprite>();
        private DataLevelManager dataLevelManager;
        
        protected override void Start()
        {
            base.Start();
            
            InitData();
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
            //textMessage.text = msg;
        }
        
        private void ReshapeItems()
        {
            countNeedSprite = 0;

            if (currentIdPack < dataLevelManager.QueueSentenceses.Count)
            {
                currentIdPack++;
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
                currentIdPack = 0;
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
            StartCoroutine(WaitReshape(6f));
            Voice(currentSentence);
            Voice(needWord);
        }
        
        private void GetSentence()
        {
            currentSentence = dataLevelManager.QueueSentenceses.Dequeue();
            dataLevelManager.QueueSentenceses.Enqueue(currentSentence);
            SetTextMessage(currentSentence);
        }

        private void GetRandomSprites()
        {
            spriteList  = dataLevelManager.QueueSprites.Dequeue();
            dataLevelManager.QueueSprites.Enqueue(spriteList);
            bool[] mixStates = {true, false};
            bool toMix = mixStates[Random.Range(0, 2)];
            
            if (toMix)
            {
                var temp = spriteList.ToList()[0];
                spriteList.ToList()[0] = spriteList.ToList()[1];
                spriteList.ToList()[1] = temp;
            }

            needWord = toMix ? spriteList.ToList()[1].Key: spriteList.ToList()[0].Key;
        }
        
        private void ClickButtonVoice()
        {
            Voice(currentSentence);
        }

        private IEnumerator WaitReshape(float time)
        {
            yield return  new WaitForSeconds(time); //TEMP
            ReshapeItems();
        }
    }
}
