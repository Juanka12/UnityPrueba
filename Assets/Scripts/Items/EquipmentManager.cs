using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake(){
        instance = this;
    }
    #endregion

    public Spell[] currentSpells;
    public ItemObject[] currentItems;

    public Spell[] GetSpell(){
        return currentSpells;
    }

    public ItemObject[] GetItem(){
        return currentItems;
    }
}
