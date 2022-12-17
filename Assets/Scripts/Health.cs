using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public delegate void damageAction();
    public event damageAction onDamage;
    public bool noDamage = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            kill();
    }

    public void kill()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
    }
}
