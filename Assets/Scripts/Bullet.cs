using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public List<string> tagsToIgnore;
    public int damage = 5;
    public float knockback = 3f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(killAfterTime());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided with " + other.gameObject.name);
        foreach (string tag in tagsToIgnore)
        {
            if (other.gameObject.CompareTag(tag))
                return;
        }

        //Debug.Log("Hit " + other.gameObject.name);
        if (other.gameObject.GetComponent<Health>() != null)
        {
            hitEntity(other);
        }
        else if (other.gameObject.CompareTag("StopBullets"))
        {
            hitEnv(other);
        }
    }

    void hitEntity(Collider2D other)
    {
        StopAllCoroutines();
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rb2D = other.gameObject.GetComponent<Rigidbody2D>();
            rb2D.AddForce(transform.up * Time.deltaTime * knockback * 100, ForceMode2D.Impulse); // 10 is the knockback force
        }
        if (other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().takeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }

    void hitEnv(Collider2D other)
    {
        StopAllCoroutines();
        GameObject.Destroy(gameObject);
    }

    IEnumerator killAfterTime() // destroys the bullet after seconds
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
