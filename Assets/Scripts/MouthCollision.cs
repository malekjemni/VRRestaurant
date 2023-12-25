using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthCollision : MonoBehaviour
{
    public string foodTag = "Food";
    public string cupTag = "Drink";
    public AudioClip[] mouthSFX;

    private float collisionTime = 0f;
    private float consumptionDuration = 1f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(foodTag))
        {
            Destroy(other.gameObject);
            audioSource.clip = mouthSFX[0];
            audioSource.Play();
        }
        if (other.CompareTag(cupTag))
        {
            audioSource.clip = mouthSFX[1];
            audioSource.Play();
            collisionTime = Time.time;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(cupTag))
        {
            if (Time.time - collisionTime >= consumptionDuration)
            {
                other.gameObject.GetComponent<Drink>().ConsumeDrink();             
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collisionTime = 0f;
    }
}
