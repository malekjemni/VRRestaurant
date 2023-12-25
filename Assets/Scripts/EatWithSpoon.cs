using UnityEngine;

public class EatWithSpoon : MonoBehaviour
{
    public string spoonTag = "Spoon";
    public int maxSoupAmount = 5; 
    public GameObject spoonAttachPoint;
    public GameObject soupBits;
    public GameObject emptySoupPlatePrefab; 

    public GameObject eatingSoupFX;
    public AudioClip[] eatingClips;

    private int currentSoupAmount = 5;
    private bool isSoupEaten = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSoupEaten && other.CompareTag(spoonTag))
        {
            EatSoupWithSpoon();
            spoonAttachPoint = other.gameObject.transform.GetChild(1).gameObject;
        }
    }

    private void EatSoupWithSpoon()
    {
        if (spoonAttachPoint != null && currentSoupAmount > 0 && spoonAttachPoint.transform.childCount == 0)
        {
            audioSource.clip = eatingClips[0];
            audioSource.Play();

            GameObject eatingEffect = Instantiate(eatingSoupFX, transform.position, Quaternion.identity);

            currentSoupAmount--;

      
            GameObject smallSoup = Instantiate(soupBits, spoonAttachPoint.transform.position, Quaternion.identity);
            smallSoup.transform.parent = spoonAttachPoint.transform;

            Destroy(eatingEffect, 1f);
        }

        if (currentSoupAmount <= 0)
        {
            isSoupEaten = true;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            if (emptySoupPlatePrefab != null)
            {
                Instantiate(emptySoupPlatePrefab,transform.position, Quaternion.identity,transform);
            }
        }
    }
}
