using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.PatternsLevel
{
    public class ItemLevel : MonoBehaviour
    {
        [SerializeField] private Button BtnBox;
        [SerializeField] private Image imageBox;
        [SerializeField] private Text letterBoxText;
        [HideInInspector] public int Line;
        [HideInInspector] public int PosInWord;
        [HideInInspector] public char CurrentLetter;
        
        private Dictionary<char, Color> availableLetters;
        private static ItemLevel prevItem;
        public static Action<ItemLevel, ItemLevel> onClickBox;
        private static Action<int> onSetInteractable;
        
        private int countLetters;
        
        private void Start()
        {
            BtnBox.onClick.AddListener(ClickBox);
            onSetInteractable += SetInteractable;
        }

        private void OnDestroy()
        {
            onSetInteractable -= SetInteractable;
        }

        public void SetDataBox(int line, int posInWord,  Dictionary<char, Color> availableLetters, string letterBox = "")
        {
            this.availableLetters = availableLetters;
            Line = line;
            PosInWord = posInWord;

            if (letterBox != "")
            {
                BtnBox.interactable = false;
                letterBoxText.text = letterBox;
            }
        }
        
        private void SetItem()
        {
            CurrentLetter = availableLetters.ToList()[countLetters].Key;
            imageBox.color = availableLetters.ToList()[countLetters].Value;
            letterBoxText.text = CurrentLetter.ToString();
            countLetters++;
            
            if (countLetters >= availableLetters.Count)
            {
                countLetters = 0;
            }
        }

        public void CallInteractable(int line)
        {
            onSetInteractable?.Invoke(line);
        }

        private void SetInteractable(int line)
        {
            if (Line == line)
            {
                BtnBox.interactable = false;
            }
        }
        
        private void ClickBox()
        {
            SetItem();
            onClickBox?.Invoke(prevItem, this);
            prevItem = this;
        }

    }
}
