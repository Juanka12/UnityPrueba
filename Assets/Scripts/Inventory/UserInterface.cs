using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    public Inventory inventory;

    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    public void Iniciar()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate {OnEnterInterface(gameObject);});
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate {OnExitInterface(gameObject);});
        slotsOnInterface.UpdateSlotDisplay();
    }

    void Update()
    {
        if (inventory.type == InterfaceType.Inventory)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = inventory.money.value.ToString();
        }
    }

    private void OnSlotUpdate(InventorySlot _slot){
        if (_slot.item.id >=0)
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.icon;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,1);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
            }else
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,0);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
                
            }
    }
    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj){
        MouseData.slotHoveredOver = obj;
    }
    public void OnExit(GameObject obj){
        MouseData.slotHoveredOver = null;
    }
    public void OnEnterInterface(GameObject obj){
        MouseData.interfaceMouseOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj){
        MouseData.interfaceMouseOver = null;
    }
    public void OnDragStart(GameObject obj){
        //if (MouseData.interfaceMouseOver.name == "Inventory" || MouseData.interfaceMouseOver.name == "Equipment")
            MouseData.tempItemDragged = createTempObject(obj);
    }

    private GameObject createTempObject(GameObject obj){
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item.id >= 0)
        {   
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(64, 64);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.icon;
            img.raycastTarget = false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj){
        if (MouseData.tempItemDragged != null)
        {
            
            Destroy(MouseData.tempItemDragged);
            if (MouseData.interfaceMouseOver == null)
            {
                slotsOnInterface[obj].removeItem();
                return;
            }
            if (MouseData.slotHoveredOver && MouseData.interfaceMouseOver.inventory.type != InterfaceType.Shop)
            {
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseOver.slotsOnInterface[MouseData.slotHoveredOver];
                inventory.swapItems(slotsOnInterface[obj], mouseHoverSlotData);
            }
        }
    }
    public void OnDrag(GameObject obj){
        if (MouseData.tempItemDragged != null)
        {
            MouseData.tempItemDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnPointerClick(GameObject obj){

    }

}

public static class MouseData
{
    public static UserInterface interfaceMouseOver;
    public static GameObject tempItemDragged;
    public static GameObject slotHoveredOver;
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair <GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.item.id >=0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                
            }
        }
    }
}