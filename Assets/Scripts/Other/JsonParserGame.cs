using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonParserGame<T>
{
    private T data;

    public T GetData(string path, string fileName)
    {
#if UNITY_EDITOR
        var file = File.ReadAllText(Path.Combine (Application.streamingAssetsPath + "/" + path, fileName + ".json"));
        data = JsonConvert.DeserializeObject<T>(file);
#elif UNITY_ANDROID
        UnityWebRequest www = UnityWebRequest.Get(Path.Combine (Application.streamingAssetsPath + "/" + path, fileName + ".json"));
        www.SendWebRequest();
        while (!www.isDone) {}
        string fileText = www.downloadHandler.text;
        
        data = JsonConvert.DeserializeObject<T>(fileText);
#endif
        return data;
    }
}