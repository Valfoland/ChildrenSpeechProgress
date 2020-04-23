using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.PatternsLevel
{
    public class ItemPatternsLevel : MonoBehaviour
    {
        [SerializeField] private Button BtnBox;
        [SerializeField] private Image imageBox;
        [SerializeField] private Text letterBoxText;
        
        private Dictionary<char, Color> availableSounds;
        private static ItemPatternsLevel prevItem;

        private int countSound;
        [HideInInspector] public int Line;
        [HideInInspector] public int PosInWord;
        [HideInInspector] public char CurrentSound;
        public static System.Action<ItemPatternsLevel, ItemPatternsLevel> onClickBox;
        private static System.Action<int> onSetInteractable;
        
        private void Start()
        {
            onSetInteractable += SetInteractable;
        }

        private void OnDestroy()
        {
            onSetInteractable -= SetInteractable;
        }

        public void SetDataBox(int line, int posInWord,  Dictionary<char, Color> availableSounds, string letterBox = "")
        {
            this.availableSounds = availableSounds;
            Line = line;
            PosInWord = posInWord;
            BtnBox.onClick.AddListener(ClickBox);

            if (letterBox != "")
            {
                BtnBox.interactable = false;
                letterBoxText.text = letterBox;
            }
        }

        private void ClickBox()
        {
            SetItem();
            onClickBox?.Invoke(prevItem, this);
            prevItem = this;
        }

        private void SetItem()
        {
            CurrentSound = availableSounds.ToList()[countSound].Key;
            imageBox.color = availableSounds.ToList()[countSound].Value;
            letterBoxText.text = CurrentSound.ToString();
            countSound++;
            if (countSound >= availableSounds.Count)
            {
                countSound = 0;
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
    }
}
