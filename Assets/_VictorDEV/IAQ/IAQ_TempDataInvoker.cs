using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.IAQ;
using VictorDev.Net.MQTT;
using Debug = VictorDev.Common.DebugHandler;

/// <summary>
/// 模擬MQTT Broker發送假資料進行測試
/// <para>+ 直接掛載在MQTT_Connecter底下的子物件就好</para>
/// </summary>  
public class IAQ_TempDataInvoker : MonoBehaviour
{
    [Header(">>> 每隔幾秒發送資料")]
    [SerializeField] private float timeInterval = 2;

    [Header(">>> 假資料供測試")]
    [SerializeField] private List<TempDataFormat> tempDataFormatList;
    [SerializeField] private MQTT_Connecter mqttConnecter;
    [SerializeField] private IAQ_DataManager iaqDataManager;

    private int counter { get; set; } = -1;

    private void Start()
    {
        Debug.Log($"[IAQ_TempData] 假資料發送 運作中…");
        //不管連線成功或失敗，都針對IAQ_DataManager進行發送訊息
        mqttConnecter?.onConnectSuccessed.AddListener((ip, port) => StartInovke());
        mqttConnecter?.onConnectFailed.AddListener((ip, port) => StartInovke());
        mqttConnecter?.onDisconnected.AddListener((ip, port) => StopInovke());
    }

    private void StartInovke() => StartCoroutine(InvokeData());
    private void StopInovke() => StopCoroutine(InvokeData());

    private IEnumerator InvokeData()
    {
        Action invokeAction = () =>
        {
            CounterJumper();
            iaqDataManager.SetData(tempDataFormatList[counter].Topic, tempDataFormatList[counter].JsonString);
        };

        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            invokeAction.Invoke();
            invokeAction.Invoke();
        }
    }

    private void OnDestroy() => StopInovke();

    private int CounterJumper()
    {
        if (++counter > tempDataFormatList.Count - 1) counter = 0;
        return counter;
    }

    private void OnValidate()
    {
        if (transform.parent != null)
        {
            mqttConnecter ??= transform.parent.GetComponent<MQTT_Connecter>();
            iaqDataManager ??= mqttConnecter.transform.parent.GetComponent<IAQ_DataManager>();
        }
    }

    [Serializable]
    public class TempDataFormat
    {
        [SerializeField] private string topic;
        [TextArea(3, 5)]
        [SerializeField] private string jsonString;
        public string Topic => topic;
        public string JsonString => jsonString;
    }
}
