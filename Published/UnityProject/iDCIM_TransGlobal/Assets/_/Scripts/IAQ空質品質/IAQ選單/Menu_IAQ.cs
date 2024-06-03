using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VictorDev.IAQ;

public class Menu_IAQ : ScrollRectList<MenuItem_IAQ, SO_IAQ_Topic>
{
    [Header(">>> 選單標題")]
    [SerializeField] private TextMeshProUGUI txtTitle;

    /// <summary>
    /// 是否已設置過資料
    /// </summary>
    public bool IsDataSetted { get; private set; } = false;

    private Dictionary<string, MenuItem_IAQ> menuItemDict = new Dictionary<string, MenuItem_IAQ>();

    /// <summary>
    /// 若已設定好資料，則進行更新
    /// <para>+ 先檢查列表上的項目是否已存在</para>
    /// <para>+ 若已存在，則進行更新</para>
    /// <para>+ 若不存在，則進行新增</para>
    /// </summary>
    public void UpdateTopicData(List<SO_IAQ_Topic> topicList)
    {
        SO_IAQ_Topic topicData;
        for (int i = 0; i < topicList.Count; i++)
        {
            topicData = topicList[i];
            //如果目標主題在列表上，則更新資料
            if (menuItemDict.ContainsKey(topicData.Topic))
            {
                menuItemDict[topicData.Topic].SetData(topicData);
            }
            else
            {
                //如果目標主題不在列表上，則新增資料
                CreateItem(topicData);
            }
        }
    }

    protected override void AddActionInSetDataListForLoop(MenuItem_IAQ item, SO_IAQ_Topic soData)
    {
        item.SetData(soData);
        item.OnClickTitleBar.AddListener((soData) => onItemClicked.Invoke(soData));

        menuItemDict[soData.Topic] = item;
        IsDataSetted = true;
    }

    /// <summary>
    /// 設定標題文字
    /// </summary>
    public string Title { set => txtTitle.SetText(value); }

    protected override void OnValidateAfter()
    {
        txtTitle = transform.Find("txtTitle").GetComponent<TextMeshProUGUI>();
    }
}
