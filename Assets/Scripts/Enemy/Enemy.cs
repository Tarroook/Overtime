using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy  : MonoBehaviour
{
    protected DetectPlayer zone;
    protected Health hpScript;
    public float speed = 4f;
    protected bool isAwake = false;
    protected GameObject player;
    protected Rigidbody2D rb; 
    private void OnEnable()
    {
        hpScript = gameObject.GetComponent<Health>();
        hpScript.onDamage += damageTaken;
    }
    private void OnDisable()
    {
        hpScript.onDamage -= damageTaken;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        zone = gameObject.GetComponentInChildren<DetectPlayer>();
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    // Update is called once per frame


    void damageTaken()
    {
        if (!isAwake)
        {
            isAwake = true;
        }
    }


}
