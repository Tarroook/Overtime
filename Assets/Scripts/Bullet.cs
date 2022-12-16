using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public string[] tagsToIgnore;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(killAfterTime());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.gameObject.name);
        foreach (string tag in tagsToIgnore)
        {
            if (other.gameObject.CompareTag(tag))
                return;
        }

        if (other.gameObject.GetComponent<Health>() != null)
        {
            StopAllCoroutines();
            other.gameObject.GetComponent<Health>().takeDamage(5);
            GameObject.Destroy(gameObject);
        }
    }

    IEnumerator killAfterTime() // destroys the bullet after seconds
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
