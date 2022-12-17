using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{

    
    
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
        //float step = speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

        rb.AddForce(transform.up * speed , ForceMode2D.Force);

        Vector2 lookDir = (Vector2)player.transform.position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(5);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(5);
        }
    }
    



}
