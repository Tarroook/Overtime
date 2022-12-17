using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletOut;

    public float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    void shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletOut.position, bulletOut.rotation);
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        bulletScript.tagsToIgnore.Add(gameObject.tag);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletOut.up * speed, ForceMode2D.Impulse);
    }
}