using System;
using System.Collections;
using System.Collections.Generic;
using Section0.HomeLevels.Level1;
using UnityEngine;
using UnityEngine.UI;

public class VoiceLevel : MonoBehaviour
{
    private void Start()
    {
        Level1.onVoice += VoiceCurrentString;
    }

    private void OnDestroy()
    {
        Level1.onVoice -= VoiceCurrentString;
    }
    
    public void VoiceCurrentString(string letter)
    {
        //TODO запуск нужного аудиофайла
    }
}
