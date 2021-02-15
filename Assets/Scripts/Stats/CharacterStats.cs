using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int evasion;
    public int critical;

    public Stat attack;
    public Stat defense;

    void Awake(){
        currentHealth = maxHealth;
    }

    public void TakeDamage (int damage){
        damage -= defense.getValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + " pilla "+ damage + " de daño");

        if(currentHealth <=0){
            Die();
        }
    }

    public virtual void Die(){
        Debug.Log(transform.name+" muere");
    }
}
