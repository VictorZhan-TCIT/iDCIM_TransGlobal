using System.Collections.Generic;
using UnityEngine;
using VictorDev.IAQ;

public class IAQ_UI_Landscape : IAQ_UI
{
    [Header(">>> 選單組件")]
    [SerializeField] private Menu_IAQ menuHandlerIAQ;

    [Header(">>> IAQ詳細資訊面板")]
    [SerializeField] private DetailPanel_IAQ detailsPanelIAQ;


    /// <summary>
    /// 目前點選的IAQ Topic
    /// </summary>
    private SO_IAQ_Topic currentSeletedTopic { get; set; } = null;

    private void Awake()
    {

        menuHandlerIAQ.onItemClicked.AddListener((soTopicData) =>
        {
            currentSeletedTopic = soTopicData;
            detailsPanelIAQ.gameObject.SetActive(true);
        });
        detailsPanelIAQ.OnClickCloseButton.AddListener(() => currentSeletedTopic = null);
    }

    /// <summary>
    /// 設定標題文字
    /// </summary>
    public override string Title { set => menuHandlerIAQ.Title = value; }

    /// <summary>
    /// 接收訂閱主題下的列資訊
    /// </summary>
    public override void SetTopicList(List<SO_IAQ_Topic> topicList)
    {
        if (menuHandlerIAQ.IsDataSetted == false) menuHandlerIAQ.SetDataList(topicList);
        else menuHandlerIAQ.UpdateTopicData(topicList);

        if (currentSeletedTopic != null)
        {
            for (int i = 0; i < topicList.Count; i++)
            {
                if (topicList[i].Topic == currentSeletedTopic.Topic)
                {
                    currentSeletedTopic = topicList[i];
                    detailsPanelIAQ.SetTopicData(currentSeletedTopic);
                }
            }
        }
    }
}