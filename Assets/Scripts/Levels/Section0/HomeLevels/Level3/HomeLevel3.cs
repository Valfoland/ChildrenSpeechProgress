using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.HomeLevels
{
    public class HomeLevel3 : LevelManager
    {
        [SerializeField] private BoxHomeLevel3[] boxLevel3;
        [SerializeField] private Text textMessage;
        
        private int currentIdPack;
        private int countNeedSprite;
        private string needWord;
        private string otherWord;
        private string currentSentence;
        
        private List<Sprite> spriteList = new List<Sprite>();
        
        private void Start()
        {
            InitData();
            Voice(DataHomeLevel3Manager.QueueSentenceses.Peek());
            ReshapeItems();
        }

        private void InitData()
        {
            BoxHomeLevel3.onClickBox += CheckBox;
            ILevelData data = new DataHomeLevel3Manager();
            data.InitData();
        }

        private void OnDestroy()
        {
            BoxHomeLevel3.onClickBox -= CheckBox;
        }

        private void ReshapeItems()
        {
            countNeedSprite = 0;

            if (currentIdPack < DataHomeLevel3Manager.QueueSentenceses.Count)
            {
                currentIdPack++;
                GetSentence();
                GetRandomSprites();

                int i = 0;
                foreach (var box in boxLevel3)
                {
                    box.SetDataBox(spriteList[i], spriteList[i].name);
                    i++;
                }
                
            }
            else
            {
                CheckWinLevel();
            }
        }

        private void Voice(string data)
        {
            onVoice?.Invoke(data);
        }

        private void GetSentence()
        {
            currentSentence = DataHomeLevel3Manager.QueueSentenceses.Dequeue();
            DataHomeLevel3Manager.QueueSentenceses.Enqueue(currentSentence);
            SetTextMessage(currentSentence);
        }

        private void GetRandomSprites()
        {
            spriteList  = DataHomeLevel3Manager.QueueSprites.Dequeue();
            DataHomeLevel3Manager.QueueSprites.Enqueue(spriteList);
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
                Voice(currentSentence + "/" + needWord);
                var newSentence = currentSentence.Replace("...", " " + needWord.ToLower());
                SetTextMessage(newSentence);
                StartCoroutine(WaitReshape(1f));
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                boxHomeLevel3.AnimBox();
                StartCoroutine(WaitReshape(0.5f));
            }
            
        }
        
        private void CheckWinLevel()
        {
            currentIdPack = 0;
            onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
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
