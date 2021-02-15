using UnityEngine;

[System.Serializable]
public class DialogNPC
{
    public string NPCname;

    [TextArea(1,5)]
    public string[] dialogo;
}
