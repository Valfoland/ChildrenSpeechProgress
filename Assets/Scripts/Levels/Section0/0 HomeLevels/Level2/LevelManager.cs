using System.Collections;
using System.Collections.Generic;
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
        
        private List<Sprite> spriteList = new List<Sprite>();
        private DataLevelManager dataLevelManager;
        
        private void Start()
        {
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
            dataLevelManager = new DataLevelManager();
        }

        private void SetTextMessage(string msg)
        {
            textMessage.text = msg;
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
                    box.SetDataBox(spriteList[i], spriteList[i].name);
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
                var newSentence = currentSentence.Replace("...", " " + needWord.ToLower());
                SetTextMessage(newSentence);
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                itemLevel.AnimBox();
            }
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
                var temp = spriteList[0];
                spriteList[0] = spriteList[1];
                spriteList[1] = temp;
            }

            needWord = toMix ? spriteList[1].name: spriteList[0].name;
        }

        private IEnumerator WaitReshape(float time)
        {
            yield return  new WaitForSeconds(time); //TEMP
            ReshapeItems();
        }
    }
}
