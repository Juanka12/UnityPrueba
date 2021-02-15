using UnityEngine;

[CreateAssetMenu(fileName="New Equipment", menuName="Inventory/Equipment")]
public class Equipment : ItemObject
{
   public int armorModifier{get;set;}
   public int damageModifier{get;set;}

    public override bool Use(){
        base.Use();
        return true;
    }
}
