using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Inventory/Spell")]
public class Spell : ScriptableObject
{
    new public string name = "New Spell";
    public int fuerza;
    public int longitud;
    public int coste;
    public GameObject spellPrefab;
    public Sprite sprite;

    public virtual void lanzarSpell()
    {
        Debug.Log("Fuerza "+fuerza);
    }
}
