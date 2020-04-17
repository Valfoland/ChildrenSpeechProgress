using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  HomeLevels.Level1
{
    public class BoxLevel1 : MonoBehaviour
    {
        [SerializeField] private Image imageBox;
        private char letterBox;
        public static System.Action<char, BoxLevel1> onClickBox;    
        
        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            
        }

        public void SetDataBox(Sprite spriteBox, char letterBox)
        {
            this.letterBox = letterBox;
            imageBox.sprite = spriteBox;
        }

        private void ClickBox()
        {
            onClickBox?.Invoke(letterBox, this);
        }
    }
}

