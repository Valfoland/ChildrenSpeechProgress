using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class ItemLevel : MonoBehaviour
    {
        [SerializeField] private Animation animBox;
        
        public void AnimBox()
        {
            animBox.Play();
        }
    }
}
