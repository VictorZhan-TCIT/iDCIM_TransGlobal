using UnityEngine;
using UnityEngine.EventSystems;
using VictorDev.ComponentUtils;

namespace VictorDev.InputUtils
{
    /// <summary>
    /// Drag拖曳UI目標對像
    /// </summary>
    public class DragUIHandler : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform dragTarget;

        public void OnDrag(PointerEventData eventData)
        {
            dragTarget.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        [ContextMenu("- OnValidate")]
        private void OnValidate()
        {
            if (canvas == null) canvas = ComponentHandler.FindTargetInParents<Canvas>(transform.parent);
        }
    }
}
