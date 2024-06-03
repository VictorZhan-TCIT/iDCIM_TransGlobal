using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

public abstract class Config_IAQ
{
    //public static string ipAddress = "192.168.0.101";
    public static string ipAddress { get; set; } = "220.135.33.221";
#if UNITY_EDITOR
    public static int port { get; set; } = 1883;
#elif UNITY_WEBGL
    public static int port{ get; set; } = 9001;
#endif

    private static Dictionary<Enum_Floor, string> topicChannelList = new Dictionary<Enum_Floor, string>()
    {
        { Enum_Floor.F1, "UNOnext/#"}
    };

    public static string CurrentFloor_Topic { get; set; } = topicChannelList[GameManager.CurrentFloor];


    /// <summary>
    /// 眔IAQ单︹布
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Color GetIAQColor(float value)
    {
        int index = 0;
        if (value > 400) index = 3;
        else if (value > 101) index = 2;
        else if (value > 81) index = 1;
        ColorUtility.TryParseHtmlString(IAQ_Color[index], out Color result);
        return result;
    }
    /// <summary>
    /// IAQ单︹布
    /// <para>厚:0~80</para>
    /// <para>独:81~100</para>
    /// <para>:101~400</para>
    /// <para>档:400+</para>
    /// </summary>
    private static string[] IAQ_Color = new string[]
    {
       "00FF00", "FFFF00", "FF0000", "FF00FF"
    };
}