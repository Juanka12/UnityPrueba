using UnityEngine;
using UnityEngine.UI;

public class SelectSpell : MonoBehaviour
{
    private Spell[] spells;
    private int selectedSpell = 0;
    private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        spells = EquipmentManager.instance.GetSpell();
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spells.Length > 0)
        {
            icon.enabled = true;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedSpell >= spells.Length - 1)
            {
                selectedSpell = 0;
            }
            else selectedSpell++;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedSpell <= 0)
            {
                selectedSpell = spells.Length - 1;
            }
            else selectedSpell--;
        }
        updateUI();
    }

    private void updateUI(){
        icon.sprite = spells[selectedSpell].sprite;
    }

    public Spell GetSpell(){
        return spells[selectedSpell];
    }

}
