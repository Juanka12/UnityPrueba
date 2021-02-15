using UnityEngine;

public enum Attributes{Agility, Intellect, Stamina, Strenght}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
    new public string name = "New Item";
    public bool isDefaultItem = false;
    public int amount;
    public int price;
    public bool stackable;
    public bool keyObject = false;
    public EquipmentSlot type;
    public Sprite icon;
    [TextArea(15,20)]
    public string description;
    public Item data = new Item();
    public Item CreateItem(){
        Item newItem = new Item(this);
        return newItem;
    }

    public virtual bool Use(){
        Debug.Log("Usando "+ name);
        return false;
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int id = -1;
    public EquipmentSlot type;
    public ItemBuff[] buffs;
    public Item(){
        name="";
        id=-1;
    }
    public Item(ItemObject item){
        name = item.name;
        id = item.data.id;
        type = item.type;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
            buffs[i].attribute = item.data.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max){
        min = _min;
        max = _max;
        GenerateValue();
    }

    // public void AddValue(ref int baseValue){
    //     baseValue += value;
    // }

    private void GenerateValue(){
        value = UnityEngine.Random.Range(min, max);
    }
}

public enum EquipmentSlot {Food, Head, Chest, Legs, Feet, Weapon, Shield}