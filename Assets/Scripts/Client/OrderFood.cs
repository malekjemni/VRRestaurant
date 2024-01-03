using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFood : MonoBehaviour
{
    public GameObject[] dishList;
    public GameObject[] drinkList;
    public AudioClip[] dialogueClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDialogue(int pos)
    {
        if (pos >= 0 && pos < dialogueClips.Length)
        {
            audioSource.clip = dialogueClips[pos];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Invalid dialogue clip position!");
        }
    }
}
