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


    private bool isFoodCut = false;
    private bool isFoodEaten = false;

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
        // Your cutting logic here
        // For example, you might animate the food being cut or change its appearance

        // Play cutting effect
        if (cuttingFX != null)
        {
            GameObject cuttingEffect = Instantiate(cuttingFX, transform.position, Quaternion.identity);
            Destroy(cuttingEffect, 0.1f);
        }

        // Set the flag to indicate that the food has been cut
        isFoodCut = true;
    }

    private void EatWithFork()
    {
        if (eatingFX != null)
        {
            GameObject eatingEffect = Instantiate(eatingFX, transform.position, Quaternion.identity);
            Destroy(eatingEffect, 0.1f);
        }

        if (attachPoint != null && dishPieces != null && dishPieces.Length > 0)
        {
            GameObject pickedDishPiece = dishPieces[0];        
            pickedDishPiece.transform.parent = attachPoint.transform;
            pickedDishPiece.transform.localPosition = Vector3.zero;
            RemoveDishPiece(pickedDishPiece);
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
