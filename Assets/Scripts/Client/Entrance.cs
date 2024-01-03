using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string playerTag = "Player";
    public OrderFood orderManager;
    public GameObject directionFX;
    private bool isIn = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!isIn)
            {
                StartCoroutine(WelcomePlayer());
            }
           
        }
    }

    IEnumerator WelcomePlayer()
    {
        isIn = true;
        yield return new WaitForSeconds(1);
        directionFX.SetActive(true);
        orderManager.PlayDialogue(0);
        yield return new WaitForSeconds(30);
        directionFX.SetActive(false);
        isIn = false;
    }
}   
