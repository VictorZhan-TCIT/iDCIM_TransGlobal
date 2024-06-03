using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VictorDev.InputUtils
{
    /// <summary>
    /// 輸入功能控制
    /// </summary>
    public abstract class InputHandler
    {
        /// <summary>
        /// 玩家是否正在觸控點擊行為
        /// <para>Web版本有支援觸控行為</para>
        /// </summary>
        public static bool IsTouching => (Input.touchSupported) ? (Input.touchCount == 1) : false;

        /// <summary>
        /// 目前滑鼠鼠標是否在UI組件上
        /// </summary>
        public static bool IsPointerOverUI
        {
            get
            {
                // 創建一個新的EventData，並使用當前EventSystem
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                // 設置滑鼠在螢幕上的位置
                eventDataCurrentPosition.position = Input.mousePosition;

                // 創建一個列表，用於存儲所有檢測到的UI物件
                List<RaycastResult> results = new List<RaycastResult>();
                // 對UI物件進行射線檢測
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

                // 如果列表中有元素，表示滑鼠點擊在UI上
                return results.Count > 0;
            }
        }
    }
}
