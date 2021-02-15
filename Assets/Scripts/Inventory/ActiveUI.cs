using UnityEngine;

public class ActiveUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public GameObject statsUI;

    void Start()
    {
        inventoryUI.GetComponent<StaticInterface>().Iniciar();
        equipmentUI.GetComponent<StaticInterface>().Iniciar();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            statsUI.SetActive(!statsUI.activeSelf);
        }
    }
}
