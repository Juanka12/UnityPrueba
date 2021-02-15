using UnityEngine;

public class PlayerStats : CharacterStats
{
    public Inventory equipment;
    public Attribute[] attributes;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot){
        if (_slot.ItemObject == null)
        {
            return;
        }
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                RemoveStats(_slot);
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i].value);
                            UpdateStats();
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot){
        if (_slot.ItemObject == null)
        {
            return;
        }
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                AddStats(_slot);
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i].value);
                            UpdateStats();
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    private void RemoveStats(InventorySlot slot){
        Equipment item = (Equipment)slot.ItemObject;
        if (item != null)
        {
            attack.RemoveModifier(item.damageModifier);
            defense.RemoveModifier(item.armorModifier);
        }
    }

    private void AddStats(InventorySlot slot){
        Equipment item = (Equipment)slot.ItemObject;
        if (item != null)
        {
            attack.AddModifier(item.damageModifier);
            defense.AddModifier(item.armorModifier);
        }
    }

    public void UpdateStats(){
        attack.SetBaseValue(attributes[3].value.getValue()/2);
        defense.SetBaseValue(attributes[1].value.getValue()/3);
        maxHealth = 100 + attributes[2].value.getValue()*10;
        evasion = attributes[0].value.getValue();
        critical = attributes[0].value.getValue();
    }

    public void AttributeModified(Attribute attribute){
    }

}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerStats parent;
    public Attributes type;
    public Stat value;

    public void SetParent(PlayerStats _parent){
        parent = _parent;
        //value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified(){
        parent.AttributeModified(this);
    }
}
