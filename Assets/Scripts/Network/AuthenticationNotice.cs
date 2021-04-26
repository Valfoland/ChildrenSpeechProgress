using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AuthNoticeData : NoticeData
{
    public AuthenticationTypes AuthNoticeType;
}

public class AuthenticationNotice : MonoBehaviour
{
    [SerializeField] private List<AuthNoticeData> noticeList;
    [SerializeField] private InternetChecker internetChecker;
    [SerializeField] private UserNetworkService userNetworkService;

    public Action<List<AuthNoticeData>> onGetNotices;

    private void Start()
    {
        internetChecker.BackgroundCheckInternet(SetNotices);
        userNetworkService.BackgroundCheckLogin(SetNotices);
    }

    private void SetNotices(AuthenticationTypes noticeType, bool isSuccess)
    {
        var currentNoticeList = new List<AuthNoticeData>();
        
        foreach (var noticeData in noticeList)
        {
            if (noticeData.AuthNoticeType == noticeType)
            {
                noticeData.IsSuccess = isSuccess;
            }
            
            if (!noticeData.IsSuccess)
            {
                currentNoticeList.Add(noticeData);
            }
        }

        onGetNotices?.Invoke(currentNoticeList);
    }
}
