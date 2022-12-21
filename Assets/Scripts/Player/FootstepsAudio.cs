using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsAudio : MonoBehaviour
{
    public float stepDistance = 1f;
    private AudioSource audioSource;
    private Vector2 lastStep;
    // Start is called before the first frame update
    void Start()
    {
        lastStep = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(lastStep.x - transform.position.x) > stepDistance || Mathf.Abs(lastStep.y - transform.position.y) > stepDistance)
        {
            audioSource.Play();
            lastStep = transform.position;
        }
    }
}
