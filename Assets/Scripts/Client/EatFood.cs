using System.Linq;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    public string knifeTag = "Knife";
    public string forkTag = "Fork";
    public GameObject[] dishPieces;
    public GameObject attachPoint;

    public GameObject cuttingFX; 
    public GameObject eatingFX;

    public AudioClip[] eatingClips;


    private bool isFoodCut = false;
    private bool isFoodEaten = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isFoodEaten)
        {
            if (other.CompareTag(knifeTag) && !isFoodCut)
            {
                CutWithKnife();
            }
            else if (other.CompareTag(forkTag) && isFoodCut)
            {
                EatWithFork();
                attachPoint = other.gameObject.transform.GetChild(1).gameObject;
            }
        }
    }

    private void CutWithKnife()
    {
        if (cuttingFX != null)
        {
            audioSource.clip = eatingClips[0];
            audioSource.Play();
            GameObject cuttingEffect = Instantiate(cuttingFX, transform.position, Quaternion.identity);        
            Destroy(cuttingEffect, 1f);

        }
        isFoodCut = true;
    }

    private void EatWithFork()
    {
        if (attachPoint != null && dishPieces != null && dishPieces.Length > 0 && attachPoint.transform.childCount == 0)
        {
            audioSource.clip = eatingClips[1];
            audioSource.Play();

            GameObject eatingEffect = Instantiate(eatingFX, transform.position, Quaternion.identity);          
            GameObject pickedDishPiece = dishPieces[0];          
            pickedDishPiece.transform.parent = attachPoint.transform;
            pickedDishPiece.transform.localPosition = Vector3.zero;
            
            RemoveDishPiece(pickedDishPiece);
            Destroy(eatingEffect, 1f);
        }
        if (dishPieces.Length == 0)
        {
            isFoodEaten = true;
        }
    }
    private void RemoveDishPiece(GameObject dishPiece)
    {
        int index = System.Array.IndexOf(dishPieces, dishPiece);

        if (index != -1)
        {
            dishPieces = dishPieces.Where((item, i) => i != index).ToArray();
        }
    }

}
