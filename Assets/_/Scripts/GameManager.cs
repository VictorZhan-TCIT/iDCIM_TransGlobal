using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Enum_GameMode CurrentGameMode { get; internal set; } = Enum_GameMode.Landscape;
    public static Enum_Floor CurrentFloor { get; internal set; } = Enum_Floor.F1;

    [Header(">>> 功能模組")]
    [SerializeField] private List<Module> moduleList;

    [Header(">>> Camera管理器")]
    [SerializeField] private CameraManager cameraManager;

    private void Awake()
    {
        //當點擊模型物件時，運鏡注視目標物件
        for (int i = 0; i < moduleList.Count; i++)
        {
            moduleList[i].OnClickModelEvent.AddListener((target) => cameraManager.LookAt(target, CameraMode.Landscape));
        }

        // 暫時先顯示IAQ Module
        moduleList[0].SetContentVisible(true);
    }
}
