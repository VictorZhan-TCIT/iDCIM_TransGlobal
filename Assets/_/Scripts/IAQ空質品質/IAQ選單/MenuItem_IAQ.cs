using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.IAQ;
using VictorDev.UI.Comps;

/// <summary>
/// iaq
/// </summary>
[RequireComponent(typeof(UIPanel))]
public class MenuItem_IAQ : MonoBehaviour
{
    [Header(">>> IAQ�q�\�D�D���")]
    [SerializeField] private SO_IAQ_Topic soTopicData;

    [Header(">>> ��ܭȤ�r�ե�")]
    [SerializeField] private Text txtIAQValue;
    [SerializeField] private Text txtCO2Value;
    [SerializeField] private Text txtOzoneValue;
    [SerializeField] private Text txtPM25Value;

    [Header(">>> �����ե�")]
    [SerializeField] private UIPanel uiPanel;

    /// <summary>
    /// IAQ�q�\�D�D���
    /// </summary>
    public SO_IAQ_Topic TopicData => soTopicData;

    [Header(">>> �I��ƥ�")]
    public UnityEvent<SO_IAQ_Topic> OnClickTitleBar;

    private void Awake()
    {
        uiPanel.onClickScaleButton.AddListener((panel, isScaleOut) => OnClickTitleBar?.Invoke(soTopicData));
    }

    /// <summary>
    /// �]�mIAQ�q�\�D�D���
    /// </summary>
    public void SetData(SO_IAQ_Topic topicData)
    {
        soTopicData = topicData;
        uiPanel.TitleText = $"[ {topicData.Topic.Split("/")[1]} ]";

        float iaqValue = soTopicData.LatestData.DataSetDict["IAQ"];
        txtIAQValue.text = iaqValue.ToString();
        txtIAQValue.color = Config_IAQ.GetIAQColor(iaqValue);

        txtCO2Value.text = soTopicData.LatestData.DataSetDict["CO2"].ToString();
        txtPM25Value.text = soTopicData.LatestData.DataSetDict["PM2.5"].ToString();
        txtOzoneValue.text = soTopicData.LatestData.DataSetDict["Ozone"].ToString();
    }

    private void OnValidate()
    {
        uiPanel ??= GetComponent<UIPanel>();
    }
}
