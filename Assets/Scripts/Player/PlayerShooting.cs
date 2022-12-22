using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletOut;
    [Space(10)]
    public readonly float defaultProjectileSpeed = 50f;
    public readonly float defaultFireRate = 5f;
    public readonly float defaultBulletsPerShot = 1f;
    public readonly float defaultSpread = 3f;
    public readonly float defaultRecoil = 5f;
    public readonly string defaultShootType = "single";
    [Space(20)]
    public float projectileSpeed = 50f;
    public float fireRate = 5; // bullets per seconds
    public float bulletsPerShot = 1;
    public float spread = 3f;
    public float recoil = 5f;
    public string shootType = "single";
    [Space(10)]
    private float nextTimeToFire = 0f;
    private Rigidbody2D playerRigidbody;
    public AudioClip gunShotSFX;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (shootType.ToLower())
        {
            case "single":
                if (Input.GetButtonDown("Fire1"))
                    SingleShot();
                break;
            case "burst":
                if (Input.GetButtonDown("Fire1"))
                    BurstShot();
                break;
            case "auto":
                if (Input.GetButton("Fire1"))
                    SingleShot();
                break;
            default:
                Debug.Log("Shoot type not recognized");
                break;
        }
    }

    void shoot()
    {
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(-spread, spread));
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletOut.position, bulletOut.rotation * randomRotation);
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        bulletScript.tagsToIgnore.Add(gameObject.tag);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletInstance.transform.up * projectileSpeed, ForceMode2D.Impulse);

        applyRecoil();
    }

    void applyRecoil()
    {
        Vector2 localUpDirection = transform.up;
        float dotProduct = Vector2.Dot(localUpDirection, playerRigidbody.velocity);

        if (dotProduct > -1)
        {
            playerRigidbody.AddForce(-bulletOut.up * recoil * 1000 * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void OnEnable()
    {
        Map.onNextRoom += resetShooting;
    }
    private void OnDisable()
    {
        Map.onNextRoom -= resetShooting;
    }

    private void resetShooting()
    {
        projectileSpeed = defaultProjectileSpeed;
        fireRate = defaultFireRate;
        bulletsPerShot = defaultBulletsPerShot;
        spread = defaultSpread;
        recoil = defaultRecoil;
        shootType = defaultShootType;
    }
    void SingleShot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            for (int i = 0; i < bulletsPerShot; i++)
            {
                shoot();
            }
            StartCoroutine(bulletOut.GetComponent<DynamicAudio>().play(gunShotSFX, .1f, 1));
        }
    }

    void BurstShot()
    {
        if (Time.time >= nextTimeToFire)
        {
            // nextTimeToFire is equals to the total time between shots * the number of projectiles + half of the time between shots so it's slower than the fire rate
            nextTimeToFire = Time.time + (1f / fireRate) * (bulletsPerShot + (bulletsPerShot * 0.5f));

            StartCoroutine(BurstShotCoroutine(1f / fireRate));
        }
    }

    IEnumerator BurstShotCoroutine(float interval)
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            shoot();
            StartCoroutine(bulletOut.GetComponent<DynamicAudio>().play(gunShotSFX, .1f, 1));
            yield return new WaitForSeconds(interval);
        }
    }
}