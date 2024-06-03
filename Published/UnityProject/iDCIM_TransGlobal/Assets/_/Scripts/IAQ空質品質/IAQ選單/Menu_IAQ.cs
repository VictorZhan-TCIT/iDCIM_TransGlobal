using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VictorDev.IAQ;

public class Menu_IAQ : ScrollRectList<MenuItem_IAQ, SO_IAQ_Topic>
{
    [Header(">>> �����D")]
    [SerializeField] private TextMeshProUGUI txtTitle;

    /// <summary>
    /// �O�_�w�]�m�L���
    /// </summary>
    public bool IsDataSetted { get; private set; } = false;

    private Dictionary<string, MenuItem_IAQ> menuItemDict = new Dictionary<string, MenuItem_IAQ>();

    /// <summary>
    /// �Y�w�]�w�n��ơA�h�i���s
    /// <para>+ ���ˬd�C��W�����جO�_�w�s�b</para>
    /// <para>+ �Y�w�s�b�A�h�i���s</para>
    /// <para>+ �Y���s�b�A�h�i��s�W</para>
    /// </summary>
    public void UpdateTopicData(List<SO_IAQ_Topic> topicList)
    {
        SO_IAQ_Topic topicData;
        for (int i = 0; i < topicList.Count; i++)
        {
            topicData = topicList[i];
            //�p�G�ؼХD�D�b�C��W�A�h��s���
            if (menuItemDict.ContainsKey(topicData.Topic))
            {
                menuItemDict[topicData.Topic].SetData(topicData);
            }
            else
            {
                //�p�G�ؼХD�D���b�C��W�A�h�s�W���
                CreateItem(topicData);
            }
        }
    }

    protected override void AddActionInSetDataListForLoop(MenuItem_IAQ item, SO_IAQ_Topic soData)
    {
        item.SetData(soData);
        item.OnClickTitleBar.AddListener((soData) => onItemClicked.Invoke(soData));

        menuItemDict[soData.Topic] = item;
        IsDataSetted = true;
    }

    /// <summary>
    /// �]�w���D��r
    /// </summary>
    public string Title { set => txtTitle.SetText(value); }

    protected override void OnValidateAfter()
    {
        txtTitle = transform.Find("txtTitle").GetComponent<TextMeshProUGUI>();
    }
}
