using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sounds;
using UnityEngine;
using UnityEngine.UI;


namespace Section0.PatternsLevel
{
    public class PatternsLevel : LevelManager
    {
        [SerializeField] private GameObject circlePrefab;
        [SerializeField] private Animation[] animField;
        [SerializeField] private Transform[] circleParent;
        [SerializeField] private Button[] voiceBtns;
        [SerializeField] private Image[] checkerImages;

        [SerializeField] private Sprite trueSprite;
        [SerializeField] private Sprite falseSprite;
        private int countTrueWords;

        private void Start()
        {
            InitData();
            InitItems();
        }

        private void InitData()
        {
            ItemPatternsLevel.onClickBox += CheckBox;
            ILevelData data = new DataPatternsLevelManager();
            data.InitData();

            for (int i = 0; i < voiceBtns.Length; i++)
            {
                var i1 = i;
                voiceBtns[i].onClick.AddListener(() => Voice(DataPatternsLevelManager.WordsLevel[i1]));
            }
        }

        private void OnDestroy()
        {
            ItemPatternsLevel.onClickBox -= CheckBox;
        }

        private void InitItems()
        {
            InstanceCircles();
        }

        private void InstanceCircles()
        {
            var word = DataPatternsLevelManager.WordsWithoutSounds;
            for (int i = 0; i < DataPatternsLevelManager.WordsWithoutSounds.Count; i++)
            {
                for (int j = 0; j < word[i].Length; j++)
                {
                    var circleObject = Instantiate(circlePrefab, circleParent[i]);
                    if (word[i][j] != '_')
                    {
                        circleObject.GetComponent<ItemPatternsLevel>()
                            .SetDataBox(i, j, DataPatternsLevelManager.SoundsLevel,
                                word[i][j].ToString());
                    }
                    else
                    {
                        circleObject.GetComponent<ItemPatternsLevel>()
                            .SetDataBox(i, j, DataPatternsLevelManager.SoundsLevel);
                    }
                }
            }
        }

        private void Voice(string word)
        {
            SoundSource.VoiceSound(word);
        }

        private void CheckBox(ItemPatternsLevel prevBox, ItemPatternsLevel currentBox)
        {
            var word = DataPatternsLevelManager
                .WordsWithoutSounds[currentBox.Line]
                .Remove(currentBox.PosInWord, 1);
            DataPatternsLevelManager.WordsWithoutSounds[currentBox.Line] =
                word.Insert(currentBox.PosInWord, currentBox.CurrentSound.ToString());

            if (DataPatternsLevelManager.WordsWithoutSounds[currentBox.Line] ==
                DataPatternsLevelManager.WordsLevel[currentBox.Line])
            {

                SetCheckWordImage(currentBox.Line, true);
                currentBox.CallInteractable(currentBox.Line);
                AttemptCounter.SetAttempt(true);
                countTrueWords++;
                
                if (countTrueWords >= DataPatternsLevelManager.WordsLevel.Count)
                {
                    CheckWinLevel();
                }
            }

            if (prevBox != null && 
                prevBox.transform.parent != currentBox.transform.parent && 
                DataPatternsLevelManager.WordsWithoutSounds[prevBox.Line] !=
                DataPatternsLevelManager.WordsLevel[prevBox.Line])
            {
                AnimField(prevBox.Line);
                SetCheckWordImage(prevBox.Line, false);
                AttemptCounter.SetAttempt(false);
            }
        }

        private void CheckWinLevel()
        {
            onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
        }

        private void AnimField(int line)
        {
            animField[line].Play();
        }

        private void SetCheckWordImage(int line, bool isTrue)
        {
            if (checkerImages[line].gameObject.activeSelf == false)
            {
                checkerImages[line].gameObject.SetActive(true);
            }

            if (isTrue)
            {
                checkerImages[line].sprite = trueSprite;
                checkerImages[line].color = new Color(0,0.5f,0,1);
            }
            else
            {
                checkerImages[line].sprite = falseSprite;
                checkerImages[line].color = new Color(0.5f,0,0,1);
            }
        }
    }
}
