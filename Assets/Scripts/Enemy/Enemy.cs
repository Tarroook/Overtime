using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public abstract class Enemy  : MonoBehaviour
{
    protected DetectPlayer zone;
    protected Health hpScript;
    public float speed = 4f;
    protected bool isAwake = false;

    protected GameObject player;
    protected Rigidbody2D rb;

    protected Seeker seeker;
    protected float distanceTillRecalculate = .5f;
    protected List<Vector3> currentPath;
    public float rotationalDamping = 0.9f;

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
    protected void Start()
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

    protected void walkAndRotateAlongPath()
    {
        if (currentPath != null && currentPath.Count > 0)
        {
            Vector2 direction = (currentPath[0] - transform.position).normalized;

            rb.AddForce(direction * speed, ForceMode2D.Force);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rotationalDamping);
            if (Vector2.Distance(transform.position, currentPath[0]) < 1f)
            {
                currentPath.RemoveAt(0);
            }
        }
    }

    protected void calculatePathTowards(Vector3 target)
    {
        seeker.StartPath(transform.position, target, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (p.error)
        {
            Debug.Log("Path not found");
        }
        else
        {
            currentPath = p.vectorPath;
        }
    }
}
