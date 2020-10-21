using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelProduct: MonoBehaviour
{
    public static Action<string> onVoice;
    public static Action<bool> onEndLevel;

    protected virtual void CheckWinLevel()
    {
        onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
    }
}