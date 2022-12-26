using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public readonly float defaultSpeed = 5f;
    public float speed = 5f;
    public float drag = 1;
    [SerializeField] private Vector2 movement;
    [SerializeField] private Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        movement = new Vector2(hor, ver);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * speed, ForceMode2D.Impulse);
        if (movement == Vector2.zero)
        {
            rb.AddForce(-rb.velocity * drag, ForceMode2D.Impulse);
        }


        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnEnable()
    {
        Map.onNextRoom += resetMovement;
    }
    private void OnDisable()
    {
        Map.onNextRoom -= resetMovement;
    }

    void resetMovement()
    {
        speed = defaultSpeed;
    }
}