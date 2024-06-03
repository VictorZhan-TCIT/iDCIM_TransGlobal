using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine;

namespace VictorDev.Common
{
    public abstract class StringHandler
    {
        /// <summary>
        /// 設置文字大小(HTML)
        /// </summary>
        public static string SetFontSizeString(string str, int fontSize) => $"<size='{fontSize}'>{str}</size>";

        /// <summary>
        /// 解碼Base64 byte[] 轉成UTF8字串
        /// </summary>
        public static string Base64ToString(byte[] data)
        {
            string base64String = JsonConvert.SerializeObject(data).Trim('\"');
            byte[] byteArray = Convert.FromBase64String(base64String);
            // 將 byte[] 解碼為字符串
            return Encoding.UTF8.GetString(byteArray);
        }

        /// <summary>
        /// float值轉換成字串
        /// <para>+ numOfDecimal: 小數點後幾位數</para>
        /// </summary>
        public static string FloatToString(float value, int numOfDecimal = 1)
        {
            // Check if the value is an integer
            if (value == Mathf.Floor(value))
            {
                // If the value is an integer, return it as a whole number string
                return value.ToString("0");
            }
            else
            {
                // If the value has decimal places, format it to two decimal places
                string dec = "#";
                for (int i = 0; i < numOfDecimal-1; i++)
                {
                    dec += dec;
                }
                return value.ToString($"0.{dec}");
            }
        }
    }
}
