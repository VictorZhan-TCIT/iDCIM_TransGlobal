using UnityEngine;

namespace VictorDev.MaterialUtils
{
    public abstract class MaterialHandler
    {
        /// <summary>
        /// �}�� / ���� Emission�\��
        /// </summary>
        public static void SetEmissionEnabled(ref Material material, bool isEnabled)
        {
            if (isEnabled) material.EnableKeyword("_EMISSION");
            else material.DisableKeyword("_EMISSION");
        }

        /// <summary>
        /// �]�wEmission�C��P�j��
        /// </summary>
        public static void SetEmissionColor(ref Material material, Color color, float internsity = 0)
        {
            SetEmissionEnabled(ref material, true);
            material.SetColor("_EmissionColor", color * internsity);
        }
    }
}
