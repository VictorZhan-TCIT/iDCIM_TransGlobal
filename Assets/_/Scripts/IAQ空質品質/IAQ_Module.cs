using UnityEngine;
using VictorDev.IAQ;
using VictorDev.Net.MQTT;

public class IAQ_Module : Module
{
    [Header(">>>MQTT連線與IAQ資料處理")]
    [SerializeField] private IAQ_DataManager iaqDataManager;
    [SerializeField] private MQTT_Connecter mqttConnect;

    [Header(">>>IAQ互動物件管理器")]
    [SerializeField] private IAQ_InteractiveManager iaqInteractiveManager;
    [Header(">>>IAQ UI組件管理")]
    [SerializeField] private IAQ_UIManager iaqUIManager;

    private void Awake()
    {
        // MQTT斷線時
        mqttConnect.onDisconnected.AddListener((ip, port) => Debug.Log($">>> [ MQTT ] 已斷線 : {ip}:{port}"));
        // MQTT連線成功時, 立即訂閱主題
        mqttConnect.onConnectSuccessed.AddListener((ip, port) =>
        {
            Debug.Log($">>> [ MQTT ]  連線成功: {ip}:{port}");
            mqttConnect.SubscribeTopic(Config_IAQ.CurrentFloor_Topic, false);
        });

        //當接收到新的IAQ資訊時，通知互動管理器更新
        iaqDataManager.OnReceiveData.AddListener(iaqInteractiveManager.UpdateDisplayerData);

        //當點選到互動物件時，發送事件，將目標物件傳至GameManager
        iaqInteractiveManager.OnMouseClickInteractiveItemEvent.AddListener(OnClickModelEvent.Invoke);

        //當點擊顯示/隱藏指標圖示時，通知IAQ互動管理器
        iaqUIManager.OnClickToggleIndicator.AddListener(iaqInteractiveManager.SetDisplayerVisible);
    }

    /// <summary>
    ///  在主選單點擊IAQ選項，以設定IAQ圖標指示與其模式下的介面是否顯示，並進行訂閱
    ///  <para>若為顯示：進行目前該樓層的MQTT訂閱</para>
    ///  <para>若為不顯示：取消目前該樓層的MQTT訂閱</para>
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
