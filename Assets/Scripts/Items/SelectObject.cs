using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour
{
    private ItemObject[] items;
    private int selectedObj = 0;
    private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        items = EquipmentManager.instance.GetItem();
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (items.Length > 0)
        {
            GetComponent<Image>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedObj >= items.Length - 1)
            {
                selectedObj = 0;
            }
            else selectedObj++;

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedObj <= 0)
            {
                selectedObj = items.Length - 1;
            }
            else selectedObj--;
        }
        updateUI();
    }

    private void updateUI(){
        icon.sprite = items[selectedObj].icon;
    }

    public ItemObject GetItem(){
        return items[selectedObj];
    }
}
