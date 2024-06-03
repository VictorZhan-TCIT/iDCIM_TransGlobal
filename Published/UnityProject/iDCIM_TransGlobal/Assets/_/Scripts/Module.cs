using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 功能模組
/// <para>+ 包含點擊模型物件事件</para>
/// <para>+ 包含主選單內容設定</para>
/// </summary>
public abstract class Module : MonoBehaviour
{
    /// <summary>
    /// 當點擊模型物件時
    /// </summary>
    [Header(">>> 當點擊模型物件時")]
    public UnityEvent<Transform> OnClickModelEvent;

    public abstract void SetContentVisible(bool isVisible);

}
