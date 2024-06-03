using System;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.Common;
using VictorDev.MaterialUtils;

namespace VictorDev.IndicatorUtils
{
    /// <summary>
    /// �ϰv����Indicator
    /// <para>�i�ʺA�]�w�u������<</para>
    /// <para>�i�ۭq�ϮסB�j�p�B�C��P�u�����e<</para>
    /// </summary>
    public class LinePinIndicator : MonoBehaviour
    {
        [Header(">>> �]�wICON�˦�")]
        public Vector2 iconSize = new Vector2(150, 150);
        public Sprite iconSprite;
        public Color colorIcon = new Color(1, 1, 1, 1);
        public Color colorBkg = new Color(1, 1, 1, 1);

        [Header(">>> �]�w�u���˦�")]
        public Color colorLine = new Color(1, 1, 1, 1);
        public float intensity = 1;
        [Range(0.5f, 10)]
        public float lineLength;
        [Range(0.05f, 10)]
        public float lineWidth;

        [Space(10)]
        [SerializeField] private RectTransform uiCanvasRect;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Text txtTitle;

        public ToggleGroup toggleGroup { set => toggle.group = value; }

        public Toggle ToogleButton => toggle;

        /// <summary>
        /// �ƥ�G�I��Indicator��
        /// </summary>
        public Action onClickIndicator { get; set; }

        private void Start() => toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) onClickIndicator.Invoke();
        });

        /// <summary>
        /// �]�w���D��r
        /// </summary>
        public void SetTitle(string title) => txtTitle.text = title;

        /// <summary>
        /// �]�w�u������
        /// </summary>
        public float LineLength
        {
            set
            {
                lineLength = value;
                UpdateLineLength();
            }
        }

        /// <summary>
        /// ��s�u������
        /// </summary>
        private void UpdateLineLength()
        {
            uiCanvasRect.localPosition = new Vector3(0, lineLength, 0);
            lineRenderer.SetPosition(1, new Vector3(0, uiCanvasRect.localPosition.y + 0.1f, 0));
        }

        /// <summary>
        /// ��s�˦�
        /// </summary>
        public void UpdateStyle() => OnValidate();

        private void OnValidate()
        {
            uiCanvasRect ??= transform.GetChild(0).GetComponent<RectTransform>();
            uiCanvasRect.GetComponent<LookAtCamera>().ToFaceTarget();

            toggle ??= uiCanvasRect.Find("Toggle").GetComponent<Toggle>();
            txtTitle ??= toggle.transform.Find("Title").GetComponent<Text>();
            lineRenderer ??= GetComponent<LineRenderer>();

            Image bkgImg = toggle.GetComponent<Image>();
            Image iconImg = toggle.transform.Find("Icon").GetComponent<Image>();

            ColorBlock cb = toggle.colors;
            cb.normalColor = colorBkg;
            toggle.colors = cb;

            iconImg.color = colorIcon;
            iconImg.sprite = iconSprite;

            //�]�w�j�p
            uiCanvasRect.sizeDelta = bkgImg.GetComponent<RectTransform>().sizeDelta = iconSize;
            iconImg.GetComponent<RectTransform>().sizeDelta = iconSize * 0.5f;

            bkgImg.rectTransform.localPosition = new Vector3(0, iconSize.y * 0.5f, 0);

            lineRenderer.positionCount = 2;

            lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;

            Material mat = lineRenderer.sharedMaterial;
            MaterialHandler.SetEmissionColor(ref mat, colorLine, intensity);
            lineRenderer.material = mat;

            UpdateLineLength();
        }
    }
}
