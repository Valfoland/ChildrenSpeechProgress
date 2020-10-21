using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.HomeLevels
{
    public class HomeLevel3 : LevelProduct
    {
        [SerializeField] private BoxHomeLevel3[] boxLevel3;
        [SerializeField] private Text textMessage;
        
        private int currentIdPack;
        private int countNeedSprite;
        private string needWord;
        private string otherWord;
        private string currentSentence;
        
        private List<Sprite> spriteList = new List<Sprite>();
        private DataHomeLevel3Manager dataHomeLevel3Manager;
        
        private void Start()
        {
            InitData();
            ReshapeItems();
        }

        private void InitData()
        {
            BoxHomeLevel3.onClickBox += CheckBox;
            dataHomeLevel3Manager = new DataHomeLevel3Manager();
        }

        private void OnDestroy()
        {
            BoxHomeLevel3.onClickBox -= CheckBox;
        }

        private void ReshapeItems()
        {
            countNeedSprite = 0;

            if (currentIdPack < dataHomeLevel3Manager.QueueSentenceses.Count)
            {
                currentIdPack++;
                GetSentence();
                GetRandomSprites();
                Voice(currentSentence);
                int i = 0;
                foreach (var box in boxLevel3)
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

        private void Voice(string word)
        {
            SoundSource.VoiceSound(word);
        }

        private void GetSentence()
        {
            currentSentence = dataHomeLevel3Manager.QueueSentenceses.Dequeue();
            dataHomeLevel3Manager.QueueSentenceses.Enqueue(currentSentence);
            SetTextMessage(currentSentence);
        }

        private void GetRandomSprites()
        {
            spriteList  = dataHomeLevel3Manager.QueueSprites.Dequeue();
            dataHomeLevel3Manager.QueueSprites.Enqueue(spriteList);
            bool[] mixStates = {true, false};
            bool toMix = mixStates[Random.Range(0, 2)];
            
            if (toMix)
            {
                var temp = spriteList[0];
                spriteList[0] = spriteList[1];
                spriteList[1] = temp;
            }

            needWord = toMix ? spriteList[1].name: spriteList[0].name;
            otherWord = toMix ? spriteList[0].name: spriteList[1].name;
        }

        private void CheckBox(string wordBox, BoxHomeLevel3 boxHomeLevel3)
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
                boxHomeLevel3.AnimBox();
            }
            StartCoroutine(WaitReshape(2f));
            Voice(currentSentence);
            Voice(needWord);
        }

        private void SetTextMessage(string msg)
        {
            textMessage.text = msg;
        }

        private IEnumerator WaitReshape(float time)
        {
            yield return  new WaitForSeconds(time); //TEMP
            ReshapeItems();
        }
    }
}
