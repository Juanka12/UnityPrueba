using UnityEngine;
using UnityEditor;

public class ItemPickUp : Interactable, ISerializationCallbackReceiver
{
    public ItemObject item;

    public override void interact(Inventory inventory){
        pickUp(inventory);
    }

    void pickUp(Inventory inventory){     
        Debug.Log("Cogiste "+item.name);
        if(inventory.AddItem(new Item(item), item.amount)){
            Destroy(gameObject);
        }
    }

    public void OnAfterDeserialize(){

    }

    public void OnBeforeSerialize(){
#if UNITY_EDITOR
        GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }
}
