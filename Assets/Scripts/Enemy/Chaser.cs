using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class Chaser : Enemy
{
    private Seeker seeker;
    List<Vector3> currentPath;
    public float distanceTillRecalculate = .5f;

    private void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        calculatePathToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (!isAwake)
        {
            if (zone.awake)
            {
                isAwake = true;
            }
        }
        if(currentPath != null)
        {
            if (currentPath.Count == 0 || Mathf.Abs(currentPath[currentPath.Count - 1].x - player.transform.position.x) > distanceTillRecalculate || Mathf.Abs(currentPath[currentPath.Count - 1].y - player.transform.position.y) > distanceTillRecalculate)
            {
                calculatePathToPlayer();
            }
        }
    }

    private void FixedUpdate()
    {
        if(isAwake)
            chasePlayer();
    }


    public float rotationalDamping = 0.9f;
    void chasePlayer()
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

    private void calculatePathToPlayer()
    {
        seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
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
