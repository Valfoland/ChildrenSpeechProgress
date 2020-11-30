using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestLoadData : MonoBehaviour
{
    public Dictionary<string, List<string>> NameItems0Dict = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> NameItems1Dict = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> NameItems2Dict = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> NameDirDict = new Dictionary<string, List<string>>();

    private void Start()
    {
        GetDataFromJson();
    }

    private void GetDataFromJson()
    {
        JsonParserGame<Dictionary<string, List<string>>> jsonData = new JsonParserGame<Dictionary<string, List<string>>>();

    }
}
