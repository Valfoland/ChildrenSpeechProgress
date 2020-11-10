using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class JsonParser : MonoBehaviour
{
    private void Start()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
        allFiles = allFiles.Where(x => x.Extension != ".meta").ToArray();
        
        foreach (FileInfo file in allFiles)
        {
            if (file.Name.StartsWith("JsonDataSounds"))
            {
                var hren = JsonConvert.DeserializeObject<Dictionary<string,List<string>>>(File.ReadAllText(file.FullName));
                
                foreach (var h in hren.Keys)
                {
                    Debug.Log(h);
                }
            }
        }
    }
}
