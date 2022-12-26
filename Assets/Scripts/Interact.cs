using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public List<Interactable> interactables = new List<Interactable>();

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && interactables.Count > 0)
        {
            Debug.Log(gameObject.name + " interacted with " + interactables[0].name);
            interactables[0].interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactables.Add(collision.gameObject.GetComponent<Interactable>());
            // Sort the interactables list based on the distance between the player and the Interactable object
            interactables = interactables.OrderBy(i => Vector2.Distance(transform.position, i.transform.position)).ToList();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactables.Remove(collision.gameObject.GetComponent<Interactable>());
            // Sort the interactables list based on the distance between the player and the Interactable object
            interactables = interactables.OrderBy(i => Vector2.Distance(transform.position, i.transform.position)).ToList();
        }
    }
}
