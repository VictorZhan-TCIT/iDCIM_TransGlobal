using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.IAQ;
using VictorDev.Managers;

/// <summary>
/// IAQ機台互動管理器
/// <para>+ 一開始就要進行連線與訂閱，才有資訊可以顯示在IAQ模型上</para>
/// </summary> 
[RequireComponent(typeof(ToggleGroup))]
public class IAQ_InteractiveManager : InteractiveManager
{
    // [Header(">>> IAQ模型顯示資訊樣式Prefab ")]
    //  [SerializeField] private IAQ_ColumnValueDisplayer iaqDisplayerPrefab;
    [SerializeField] private ToggleGroup pinIndicatorToggleGroup;

    /// <summary>
    /// 儲存IAQ機臺
    /// </summary>
    public Dictionary<string, IAQ_ColumnValueDisplayer> IAQDisplayerDict { get; private set; } = new Dictionary<string, IAQ_ColumnValueDisplayer>();

    protected override void AddMoreComponentToObject(Collider target)
    {
        base.AddMoreComponentToObject(target);

        // 新增IAQ_Device到目標物件底下，依照IAQ欄位名稱儲存此IAQ機臺的Topic資料
        //  IAQ_ColumnValueDisplayer iaqDisplayer = Instantiate(iaqDisplayerPrefab, target.transform);
        IAQ_ColumnValueDisplayer iaqDisplayer = target.transform.GetChild(0).GetComponent<IAQ_ColumnValueDisplayer>();
        iaqDisplayer.columnName = target.name;
        iaqDisplayer.toggleGroup = pinIndicatorToggleGroup;
        IAQDisplayerDict[iaqDisplayer.columnName] = iaqDisplayer;
        iaqDisplayer.OnClickEvent.AddListener(OnMouseClickInteractiveItemEvent.Invoke);
    }

    /// <summary>
    /// 設定模型物件Displayer顯示/隱藏
    /// </summary>
    public void SetDisplayerVisible(bool isVisible)
    {
        foreach (IAQ_ColumnValueDisplayer displayer in IAQDisplayerDict.Values)
        {
            displayer.gameObject.SetActive(isVisible);
        }
    }

    /// <summary>
    /// 當接收到IAQ資訊時，更新至模型物件上
    /// </summary>
    public void UpdateDisplayerData(List<SO_IAQ_Topic> soTopicList)
    {
        //傳給IAQ Device進行模型上的顯示資訊，只抓第一個主題來更新
        foreach (IAQ_ColumnValueDisplayer displayer in IAQDisplayerDict.Values)
        {
            displayer.SetData(soTopicList[0]);
        }
    }

    private void OnValidate()
    {
        pinIndicatorToggleGroup ??= GetComponent<ToggleGroup>();
    }
}
