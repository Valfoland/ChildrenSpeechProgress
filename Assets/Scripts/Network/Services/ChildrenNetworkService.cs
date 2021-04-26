using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;



public class ChildrenNetworkService : BaseNetworkService
{
    private const string CHILDREN_ROUTE = "Children";
    private const string CHILDREN_RESULT_ROUTE = "ChildrenResult"; 
    
    public static ChildrenNetworkService Instance;
     
    private void Start() {
        if(Instance == null) Instance = this;
    }
    
    public void GetChildren(Action<List<ChildData>> onGetResponse)
    {
        StartCoroutine(GetChildrenCoroutine(onGetResponse));
    }
    
    public void SetChildResult(ChildResultData childResultData, int idChild)
    {
        StartCoroutine(SetChildResultCoroutine(childResultData, idChild));
    }
    
    private IEnumerator GetChildrenCoroutine(Action<List<ChildData>> onGetResponse)
    {
        using (var req = UnityWebRequest.Get(URL + CHILDREN_ROUTE))
        {
            yield return req.SendWebRequest();
            
            try
            {
                var data = JsonConvert.DeserializeObject<ChildData[]>(req.downloadHandler.text);
                onGetResponse?.Invoke(data?.ToList());
            }
            catch
            {
                onGetResponse?.Invoke(null);
            }
        }
    }

    private IEnumerator SetChildResultCoroutine(ChildResultData childData, int idChild)
    {
        string json = JsonConvert.SerializeObject(childData);

        var req = new UnityWebRequest(URL + CHILDREN_RESULT_ROUTE + "/" + idChild, "POST")
        {
            uploadHandler = new UploadHandlerRaw(new UTF8Encoding().GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();
    }
}
