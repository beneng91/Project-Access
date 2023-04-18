using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoop : MonoBehaviour
{
    private float timers = 0f;
    public float timer = 0f;

    public AudioClip[] audioSound;
    public AudioSource audioSource;


    private void Start()
    {
        timers = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timers -= Time.deltaTime;

        if (timers <= 0)
        {
            audioSource.clip = audioSound[0];
            audioSource.PlayOneShot(audioSource.clip);
            timers = timer;
        }
    }
}
