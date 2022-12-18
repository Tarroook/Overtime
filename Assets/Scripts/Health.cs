using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // need to add default values and shit but fuck I'm tired
    public int health = 100;
    public delegate void damageAction();
    public event damageAction onDamage;
    public bool noDamage = false;

    public ParticleSystem healParticles;

    public delegate void dieAction(GameObject self);
    public event dieAction onDeath;


    public void takeDamage(int damage)
    {
        if (noDamage)
            return;
        
        health -= damage;
        if (onDamage != null)
        {
            onDamage();
        }
        if (health <= 0)
        {
            health = 0;
            kill();
        }
    }

    public void heal(int hp)
    {
        Debug.Log(gameObject.name + " was healed for " + hp);
        Instantiate(healParticles, transform.position, Quaternion.identity);
        health += hp;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void kill()
    {
        Debug.Log(gameObject.name + " has died.");
        if (onDeath != null)
            onDeath(gameObject);
        if(gameObject.tag.Equals("Player"))
            SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
