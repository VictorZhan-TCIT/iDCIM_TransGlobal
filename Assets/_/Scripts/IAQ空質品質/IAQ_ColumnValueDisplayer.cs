using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Chart.XChart;
using VictorDev.Common;
using VictorDev.IAQ;

public class IAQ_ColumnValueDisplayer : MonoBehaviour
{
    public string columnName;

    [SerializeField] private Toggle toggle;
    [SerializeField] private TextMeshProUGUI txtValue, txtValue1;
    [SerializeField] private Sprite icon;
    [SerializeField] private Image imgIcon;
    [SerializeField] private GameObject chartPanel, displayer;
    [SerializeField] private XChartController xChartController;

    public ToggleGroup toggleGroup { set => toggle.group = value; }


    public UnityEvent<Transform> OnClickEvent;

    private void Awake()
    {
        toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) OnClickEvent?.Invoke(transform.parent);
            if(chartPanel !=null) chartPanel.SetActive(isOn);
            if(displayer != null) displayer.SetActive(!isOn);
        });
    }

    public void SetData(SO_IAQ_Topic soTopicData)
    {
        txtValue.text = StringHandler.FloatToString(soTopicData.LatestData.DataSetDict[columnName], 1);
        if(txtValue1 != null) txtValue1.text = StringHandler.FloatToString(soTopicData.LatestData.DataSetDict[columnName]);
        if (xChartController != null) xChartController.AddData(soTopicData.LatestData.DataSetDict, soTopicData.LatestData.timeStamp.ToString("HH:mm:ss"));
    }

    private void OnValidate()
    {
        toggle ??= GetComponent<Toggle>();
        if (imgIcon != null)
        {
            imgIcon.sprite = icon;
        }
    }
}
