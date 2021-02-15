using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public ExpManager expManager;
    public Transform[] statsText;
    public Button[] botones;
    private Stat[] stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = new Stat[4];
       for (int i = 0; i < stats.Length; i++)
       {
           stats[i] = playerStats.attributes[i].value;
       }
           botones[0].onClick.AddListener(delegate{AddValue(0);});
           botones[1].onClick.AddListener(delegate{AddValue(1);});
           botones[2].onClick.AddListener(delegate{AddValue(2);});
           botones[3].onClick.AddListener(delegate{AddValue(3);});
    }

    // Update is called once per frame
    void Update()
    {
        if (expManager.skillPoints > 0)
        {
            ActiveButtons();
        }else
        {
            DisableButtons();
        }
        statsText[0].GetComponent<Text>().text = "HP " + playerStats.maxHealth.ToString();
        statsText[1].GetComponent<Text>().text = "Ataque " + playerStats.attack.getValue().ToString();
        statsText[2].GetComponent<Text>().text = "Defensa " + playerStats.defense.getValue().ToString();
        statsText[3].GetComponent<Text>().text = "Agilidad " + stats[0].getValue();
        statsText[4].GetComponent<Text>().text = "Intelecto " + stats[1].getValue();
        statsText[5].GetComponent<Text>().text = "Stamina " + stats[2].getValue();
        statsText[6].GetComponent<Text>().text = "Fuerza " + stats[3].getValue();
    }

    void AddValue(int i){
        stats[i].AddBaseValue();
        expManager.skillPoints --;
        playerStats.UpdateStats();
    }

    private void ActiveButtons(){
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].gameObject.SetActive(true);
        }        
    }

    private void DisableButtons(){
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].gameObject.SetActive(false);
        }
    }
}
