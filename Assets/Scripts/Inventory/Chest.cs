using UnityEngine;

public class Chest : Interactable
{
    public Inventory inventoryChest;
    public GameObject shop_chest;
    
    public override void interact(Inventory inventory){
        base.interact(inventory);
        shop_chest.SetActive(true);
        shop_chest.GetComponent<StaticInterface>().inventory = inventoryChest;
        shop_chest.GetComponent<StaticInterface>().Iniciar();
        Clear();
    }

    void Clear(){
        for (int i = 0; i < shop_chest.transform.childCount; i++)
        {
            shop_chest.transform.GetChild(i).GetComponent<SlotsEvents>().UpdateSlots();
        }
    }
}
