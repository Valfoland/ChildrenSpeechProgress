using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Newtonsoft.Json;

public class JsonParserGame<T>
{
    private T data;
    
    public T GetData(string path, string fileName)
    {
        var allFiles = new DirectoryInfo(Application.streamingAssetsPath + "/" + path).GetFiles("*.*").
            Where(x => x.Extension != ".meta").ToArray();
        
        foreach (FileInfo file in allFiles)
        {
            if (file.Name.StartsWith(fileName))
            {
                data =  JsonConvert.DeserializeObject<T>(File.ReadAllText(file.FullName));
            }
        }

        return data;
    }
}
