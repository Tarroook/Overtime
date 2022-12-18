using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public List<string> tagsToIgnore;


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
            StopAllCoroutines();
            other.gameObject.GetComponent<Health>().takeDamage(5);
            GameObject.Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("StopBullets"))
        {
            StopAllCoroutines();
            GameObject.Destroy(gameObject);
        }
    }

    IEnumerator killAfterTime() // destroys the bullet after seconds
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
