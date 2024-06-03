using UnityEngine;
using VictorDev.IAQ;
using VictorDev.Net.MQTT;

public class IAQ_Module : Module
{
    [Header(">>>MQTT�s�u�PIAQ��ƳB�z")]
    [SerializeField] private IAQ_DataManager iaqDataManager;
    [SerializeField] private MQTT_Connecter mqttConnect;

    [Header(">>>IAQ���ʪ���޲z��")]
    [SerializeField] private IAQ_InteractiveManager iaqInteractiveManager;
    [Header(">>>IAQ UI�ե�޲z")]
    [SerializeField] private IAQ_UIManager iaqUIManager;

    private void Awake()
    {
        // MQTT�_�u��
        mqttConnect.onDisconnected.AddListener((ip, port) => Debug.Log($">>> [ MQTT ] �w�_�u : {ip}:{port}"));
        // MQTT�s�u���\��, �ߧY�q�\�D�D
        mqttConnect.onConnectSuccessed.AddListener((ip, port) =>
        {
            Debug.Log($">>> [ MQTT ]  �s�u���\: {ip}:{port}");
            mqttConnect.SubscribeTopic(Config_IAQ.CurrentFloor_Topic, false);
        });

        //������s��IAQ��T�ɡA�q�����ʺ޲z����s
        iaqDataManager.OnReceiveData.AddListener(iaqInteractiveManager.UpdateDisplayerData);

        //���I��줬�ʪ���ɡA�o�e�ƥ�A�N�ؼЪ���Ǧ�GameManager
        iaqInteractiveManager.OnMouseClickInteractiveItemEvent.AddListener(OnClickModelEvent.Invoke);

        //���I�����/���ë��йϥܮɡA�q��IAQ���ʺ޲z��
        iaqUIManager.OnClickToggleIndicator.AddListener(iaqInteractiveManager.SetDisplayerVisible);
    }

    /// <summary>
    ///  �b�D����I��IAQ�ﶵ�A�H�]�wIAQ�ϼЫ��ܻP��Ҧ��U�������O�_��ܡA�öi��q�\
    ///  <para>�Y����ܡG�i��ثe�ӼӼh��MQTT�q�\</para>
    ///  <para>�Y������ܡG�����ثe�ӼӼh��MQTT�q�\</para>
    /// </summary>
    public override void SetContentVisible(bool isVisible)
    {
        iaqInteractiveManager.SetDisplayerVisible(isVisible);
        if (mqttConnect.isConnected)
        {
            if (isVisible) mqttConnect.SubscribeTopic(Config_IAQ.CurrentFloor_Topic);
            else mqttConnect.UnsubscribeTopic(Config_IAQ.CurrentFloor_Topic);
        }
        else mqttConnect.Connect(Config_IAQ.ipAddress, Config_IAQ.port);
    }

}
