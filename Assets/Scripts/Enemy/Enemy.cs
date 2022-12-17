using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    DetectPlayer zone;
    Health hpScript;
    public float speed = 3f;
    bool isAwake = false;
    GameObject player; 

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
        zone = gameObject.GetComponentInChildren<DetectPlayer>();
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (!isAwake)
        {
            if (zone.awaike)
            {
                isAwake = true;
            }

        }else
        {
            //fonction poursuvre joueur 
            chasePlayer();
        }
    }



    void chasePlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }



    void damageTaken()
    {
        if (!isAwake)
        {
            isAwake = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().takeDamage(5);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().takeDamage(5);
        }
    }



}
