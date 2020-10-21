using System;
using System.Collections;
using System.Collections.Generic;
using Section0.HomeLevels;
using UnityEngine;
using UnityEngine.UI;

public class VoiceLevels : MonoBehaviour
{
    private void Start()
    {
        LevelProduct.onVoice += VoiceCurrentString;
    }

    private void OnDestroy()
    {
        LevelProduct.onVoice -= VoiceCurrentString;
    }
    
    public void VoiceCurrentString(string letter)
    {
        //TODO запуск нужного аудиофайла
    }
}
