using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Chart.XChart;
using VictorDev.IAQ;
using VictorDev.UI.Comps;

public class DetailPanel_IAQ : MonoBehaviour
{
    [SerializeField] private SO_IAQ_Topic soTopicData;

    [SerializeField] private UIPanel uiPanel;

   /* [Header(">>>標題")]
    [SerializeField] private TextMeshProUGUI txtTitle;*/

    [Header(">>>溫度、濕度、IAQ指數")]
    [SerializeField] private Text txtRT;
    [SerializeField] private Text txtRH;
    [SerializeField] private Text txtIAQ;

    [Header(">>>臭氧、揮發性有機物、甲荃")]
    [SerializeField] private Text txtOzone;
    [SerializeField] private Text txtVOCs;
    [SerializeField] private Text txtIFormaldehyde;

    [Header(">>>圖表：CO2、CO")]
    [SerializeField] private XChartController chartCO2;
    [Header(">>>圖表：PM2.5、PM10")]
    [SerializeField] private XChartController chartPM25;
    
    [Header(">>>點選關閉按鈕時")]
    public UnityEvent OnClickCloseButton;

    private void Awake()
    {
        uiPanel.onClickCloseButton.AddListener((uiPanel) => OnClickCloseButton?.Invoke());
    }

    public void SetTopicData(SO_IAQ_Topic data)
    {
        soTopicData = data;
        //txtTitle.text = soTopicData.Topic;
        uiPanel.TitleText = $"[ {soTopicData.Topic.Split("/")[1]} ]";

        //最新一筆資料
        IAQ_DataSet latestData = data.LatestData;
        string timeStampe = latestData.timeStamp.ToString("HH:mm:ss");

        txtRT.text = latestData.DataSetDict["RT"].ToString();
        txtRH.text = latestData.DataSetDict["RH"].ToString();
        txtIAQ.text = latestData.DataSetDict["IAQ"].ToString();

        txtOzone.text = latestData.DataSetDict["Ozone"].ToString();
        txtVOCs.text = (latestData.DataSetDict["VOCs"] / 1000).ToString();
        txtIFormaldehyde.text = latestData.DataSetDict["Formaldehyde"].ToString();

        chartCO2.AddData(latestData.DataSetDict, timeStampe);
        chartPM25.AddData(latestData.DataSetDict, timeStampe);
    }
}
