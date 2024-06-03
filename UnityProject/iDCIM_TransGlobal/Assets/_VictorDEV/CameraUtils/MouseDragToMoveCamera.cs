using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.InputUtils;

namespace VictorDev.CameraUtils
{
    /// <summary>
    /// 直接掛載在Cinamachine Camera組件上就好
    /// <para>當滑鼠左鍵按下時，才能進行Cinemachine攝影機控制</para>
    /// </summary>
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class MouseDragToMoveCamera : MonoBehaviour
    {
        [Header(">>> 滑鼠移動最大限速")]
        [SerializeField] private int speed_X = 150;
        [SerializeField] private int speed_Y = 2;

        private enum EnumMouseType { LeftClick, RightClick };
        [Header(">>> 滑鼠按鍵行為")]
        [SerializeField] private EnumMouseType clickBehaviour = EnumMouseType.LeftClick;

        [SerializeField] private CinemachineFreeLook freeLookCamera;

        /// <summary>
        /// 在一開始MouseDown的時後，是否點擊在UI組件上
        /// </summary>
        private bool isClickOverUI { get; set; } = false;

        /// <summary>
        /// [Enum, 值] 滑鼠鍵行為 
        /// </summary>
        private Dictionary<EnumMouseType, int> mouseKeyDict { get; set; } = new Dictionary<MouseDragToMoveCamera.EnumMouseType, int>() {
            { EnumMouseType.LeftClick, 0 },
            { EnumMouseType.RightClick, 1 },
        };

        private void Update()
        {
            if (Input.GetMouseButtonDown(mouseKeyDict[clickBehaviour])) isClickOverUI = InputHandler.IsPointerOverUI;
            if (Input.GetMouseButtonUp(mouseKeyDict[clickBehaviour])) isClickOverUI = false;

            // 是否支援觸控
            if ((InputHandler.IsTouching || Input.GetMouseButton(mouseKeyDict[clickBehaviour])) && isClickOverUI == false)
            {
                freeLookCamera.m_YAxis.m_MaxSpeed = speed_Y;
                freeLookCamera.m_XAxis.m_MaxSpeed = speed_X;
            }
            else
            {
                freeLookCamera.m_YAxis.m_MaxSpeed = 0;
                freeLookCamera.m_XAxis.m_MaxSpeed = 0;
            }
        }

        private void OnValidate() => freeLookCamera = GetComponent<CinemachineFreeLook>();
    }
}
