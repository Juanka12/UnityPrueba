using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public int neededExp;
    public int lvl;
    public int actualExp;
    public int skillPoints;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddExp(20);
        }
    }

    public void AddExp(int exp){
        actualExp += exp;
        CheckLvlup();
    }

    private void CheckLvlup(){
        if (actualExp >= neededExp*lvl)
        {
            actualExp -=neededExp*lvl;
            lvl ++;
            skillPoints ++;
        }
    }
}
