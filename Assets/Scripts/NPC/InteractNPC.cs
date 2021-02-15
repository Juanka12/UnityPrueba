using UnityEngine;

public class InteractNPC : Interactable
{
    public GameObject optionsUI;
    public Inventory inventoryNPC;

    public DialogNPC dialogo;

    public override void interact(Inventory inventory){
        base.interact(inventory);
        optionsUI.SetActive(true);
        optionsUI.GetComponent<OptionsNPC>().ActualizarNPC(dialogo, inventoryNPC);
    }
}
