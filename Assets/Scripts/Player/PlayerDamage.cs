using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class PlayerDamage : MonoBehaviour
{
    Health hpScript;
    public float iframes = 2; 

    private void OnEnable()
    {
        hpScript = gameObject.GetComponent<Health>();
        hpScript.onDamage += playerTakeDamage;
    }
    private void OnDisable()
    {
        hpScript.onDamage -= playerTakeDamage;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playerTakeDamage()
    {

        hpScript.noDamage = true;
        StartCoroutine(invulnerableTime());
    }

    IEnumerator invulnerableTime()
    {
        yield return new WaitForSeconds(iframes);
        hpScript.noDamage = false;
    }
}
