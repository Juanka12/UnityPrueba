using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public enum InterfaceType
{
    Inventory,
    Equipment,
    Chest,
    Shop
}

[CreateAssetMenu(fileName="Inventory", menuName="Inventory/inventario")]
public class Inventory : ScriptableObject
{
    public string savepath;
    public ItemDatabase database;
    public InterfaceType type;
    public Inventario Container;
    public Money money {get;} = new Money();
    public InventorySlot[] GetSlots { get { return Container.Slots;}}

    public bool AddItem(Item _item, int _amount){
        if (EmptySlotCount <= 0)
        {
            return false;
        }
        InventorySlot slot = findItemOnInventory(_item);
        if (!database.ItemObjects[_item.id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.id <= -1)
                {
                    counter ++;
                }
            }
            return counter;
        }
    }

    private InventorySlot findItemOnInventory(Item _item){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.id == _item.id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }
    private InventorySlot SetEmptySlot(Item _item, int _amount){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.id <= -1)
            {
                GetSlots[i].UpdateSlot( _item, _amount);
                return GetSlots[i];
            }
        }
        return null;
    }

    public void swapItems(InventorySlot item1, InventorySlot item2){
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    private void removeItem(Item _item){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item == _item)
            {
                GetSlots[i].UpdateSlot(null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save(){
        // string saveData = JsonUtility.ToJson(this, true);
        // BinaryFormatter bf = new BinaryFormatter();
        // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savepath));
        // bf.Serialize(file, saveData);
        // file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savepath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load(){
        if (File.Exists(string.Concat(Application.persistentDataPath, savepath)))
        {
             Debug.Log("Cargado"+ Application.persistentDataPath);
        //     BinaryFormatter bf = new BinaryFormatter();
        //     FileStream file = File.Open(string.Concat(Application.persistentDataPath, savepath), FileMode.Open);
        //     JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        //     file.Close();
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savepath), FileMode.Open, FileAccess.Read);
        Inventario newContainer = (Inventario)formatter.Deserialize(stream);
        for (int i = 0; i < GetSlots.Length; i++)
        {
            GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
        }
        stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear(){
        Container.Clear();
    }
}

[System.Serializable]
public class Inventario
{
    public InventorySlot[] Slots = new InventorySlot[12];
    public void Clear(){
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].removeItem();
        }
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
    public EquipmentSlot[] allowed = new EquipmentSlot[0];
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay;
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public Item item {get; set;}
    public int amount {get; set;}

    public ItemObject ItemObject
    {
        get
        {
            if (item.id >= 0)
            {
                return parent.inventory.database.ItemObjects[item.id];
            }
            return null;
        }
    }

    public InventorySlot(){
        UpdateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount){
        UpdateSlot(_item, _amount);
    }
    public void UpdateSlot(Item _item, int _amount){
        if (OnBeforeUpdate != null)
        {
            OnBeforeUpdate.Invoke(this);
        }
        item = _item;
        amount = _amount;
        if (OnAfterUpdate !=null)
        {
            OnAfterUpdate.Invoke(this);
        }
    }

    public void removeItem(){
        UpdateSlot(new Item(), 0);
    }

    public void AddAmount(int value){
        UpdateSlot(item, amount += value);
    }

    public bool CanPlaceInSlot(ItemObject _itemObject){
        if (allowed.Length <= 0 || _itemObject == null || _itemObject.data.id < 0)
        {
            return true;
        }
        for (int i = 0; i < allowed.Length; i++)
        {
            if (_itemObject.type == allowed[i])
            {
                return true;
            }
        }
        return false;
    }
}