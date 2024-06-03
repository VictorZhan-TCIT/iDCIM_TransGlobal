using System;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.IAQ;

public abstract class IAQ_UI : MonoBehaviour
{
    /// <summary>
    /// 設定標題文字
    /// </summary>
    public abstract string Title { set; }

    /// <summary>
    /// 設置訂閱主題的資料
    /// </summary>
    public abstract void SetTopicList(List<SO_IAQ_Topic> topicList);
}