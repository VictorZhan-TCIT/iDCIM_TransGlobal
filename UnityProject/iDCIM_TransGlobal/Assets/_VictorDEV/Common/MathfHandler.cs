using UnityEngine;

namespace VictorDev.Common
{
    public abstract class MathfHandler
    {
        /// <summary>
        /// 四捨五入至小數點第幾位
        /// </summary>
        /// <param name="numOfDecimal">小數點第幾位</param>
        public static float FloatToDecimal(float value, int numOfDecimal = 1)
        {
            float divideValue = 1;
            for (int i = 0; i < numOfDecimal; i++)
            {
                divideValue *= 10;
            }
            return Mathf.Round(value * divideValue) / divideValue;
        }
    }
}
