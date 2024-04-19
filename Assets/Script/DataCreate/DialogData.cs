using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Data/DialogsData ")]
public class DialogData : ScriptableObject
{

    [TextArea(4,10)]
    public List<string> data = new List<string>();

    public bool switchChat;

    public string samuraiName = "âÂªÔâ¹ÃÔ";

    public NPCType npcType;

    [Header("ForAE")]
    public bool haveAE;
    public int aeOpenIndex;

    public enum NPCType
    {
        Uncle,
        Boy,
        Miko,
        Chef,
        Player,
    }
}
