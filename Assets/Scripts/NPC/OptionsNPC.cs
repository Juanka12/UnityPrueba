using UnityEngine;
using UnityEngine.UI;

public class OptionsNPC : MonoBehaviour
{
    public GameObject dialogUI;
    public GameObject shop_chest;
    private Button[] botones;
    private DialogNPC dialogo;
    private Inventory inventoryNPC;

    void Awake(){
        botones = transform.GetComponentsInChildren<Button>();
        agregarListeners();
    }

    private void agregarListeners(){
        botones[0].onClick.AddListener(Dialogo);
        botones[1].onClick.AddListener(Shop);
        botones[2].onClick.AddListener(Exit);
    }

    public void ActualizarNPC(DialogNPC _dialogo, Inventory inventario){
        dialogo = _dialogo;
        inventoryNPC = inventario;
        botones[1].interactable = true;
    }

    private void Dialogo(){
        // if (shop_chest.transform.childCount > 0)
        // {
        //     Clear();
        // }
        dialogUI.SetActive(true);
        dialogUI.GetComponent<DialogManager>().StartDialog(dialogo);

        gameObject.SetActive(false);
        shop_chest.SetActive(false);
    }

    private void Shop(){
        shop_chest.SetActive(true);
        shop_chest.GetComponent<StaticInterface>().inventory = inventoryNPC;
        shop_chest.GetComponent<StaticInterface>().Iniciar();
        Clear();
        gameObject.SetActive(false);
    }

    private void Exit(){
        // if (shop_chest.transform.childCount > 0)
        // {
        //     Clear();
        // }
        shop_chest.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Clear(){
        for (int i = 0; i < shop_chest.transform.childCount; i++)
        {
            shop_chest.transform.GetChild(i).GetComponent<SlotsEvents>().UpdateSlots();
        }
    }
}
