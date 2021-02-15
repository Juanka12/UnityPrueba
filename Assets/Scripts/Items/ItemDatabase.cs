using UnityEngine;

[CreateAssetMenu(fileName="New Item Database", menuName = "Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] ItemObjects;

    public void OnAfterDeserialize(){
        for (int i = 0; i < ItemObjects.Length; i++)
        {
            ItemObjects[i].data.id = i;
        }
    }

    public void OnBeforeSerialize(){
    }
}
