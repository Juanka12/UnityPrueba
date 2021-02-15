using UnityEngine;
using UnityEngine.EventSystems;

public class SlotsEvents : MonoBehaviour, IPointerClickHandler
{
    public Inventory inventory;
    public Inventory equipment;
    public GameObject playerInventoryUI;
    public GameObject shop_chest;

    void Start(){
        UpdateSlots();
    }

    public void UpdateSlots(){
        if (transform.parent.name != "Equipment")
        {
            inventory = transform.GetComponentInParent<StaticInterface>().inventory;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.pointerId)
        {
            case -1:
                Debug.Log("Left click");
                break;
            case -2:
                if(MouseData.interfaceMouseOver.name == "Inventory"){
                    if (shop_chest.activeSelf == true && shop_chest.GetComponent<StaticInterface>().inventory.type == InterfaceType.Shop)
                    {
                        Vender();
                    }else
                    {
                    Equipar(transform.GetSiblingIndex());
                    }
                }else
                if (MouseData.interfaceMouseOver.name == "Equipment")
                {
                    Unequip(transform.GetSiblingIndex());
                }else
                if (MouseData.interfaceMouseOver.inventory.type == InterfaceType.Shop)
                {
                    Comprar();
                }else
                if (MouseData.interfaceMouseOver.inventory.type == InterfaceType.Chest)
                {
                    AddFromChest();
                }
                break;
            case -3:
                Debug.Log("Middle click");
                break;
        }
    }

    private void Equipar(int i){
        if((int)inventory.GetSlots[i-2].item.type >= 1){
            int slot = (int)inventory.GetSlots[i-2].item.type - 1;
            inventory.swapItems(inventory.GetSlots[i-2], equipment.GetSlots[slot]);
        }
    }

    private void Unequip(int i){
        if((int)equipment.GetSlots[i-1].item.type >= 1){
            int slot = (int)equipment.GetSlots[i-1].item.type - 1;
            inventory.AddItem(equipment.GetSlots[i-1].item, 1);
            equipment.GetSlots[i-1].removeItem();
        }
    }

    private void AddFromChest(){
        Inventory playerInventory = playerInventoryUI.transform.GetComponent<StaticInterface>().inventory;
        InventorySlot slotTemp = inventory.GetSlots[transform.GetSiblingIndex()];
        if (slotTemp.ItemObject != null)
        {
            if(playerInventory.AddItem(slotTemp.item, slotTemp.amount))
            {
                slotTemp.removeItem();
            }
        }
    }

    private void Comprar(){
        Inventory playerInventory = playerInventoryUI.transform.GetComponent<StaticInterface>().inventory;
        InventorySlot slotTemp = inventory.GetSlots[transform.GetSiblingIndex()];
        if (slotTemp.ItemObject != null)
        {
            if (playerInventory.money.value >= slotTemp.ItemObject.price)
            {
                if(playerInventory.AddItem(slotTemp.item, slotTemp.amount))
                {
                    playerInventory.money.RestaMoney(slotTemp.ItemObject.price);
                    slotTemp.removeItem();
                }
            }
        }
    }

    private void Vender(){
        Inventory shopInventory = shop_chest.transform.GetComponent<StaticInterface>().inventory;
        InventorySlot slotTemp = inventory.GetSlots[transform.GetSiblingIndex()-2];
        if (slotTemp.ItemObject != null)
        {
            if (shopInventory.AddItem(slotTemp.item, slotTemp.amount))
            {
                inventory.money.AddMoney(slotTemp.ItemObject.price);
                slotTemp.removeItem();
            }
        }
    }
}
