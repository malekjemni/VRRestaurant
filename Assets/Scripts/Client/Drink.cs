using UnityEngine;

public class Drink : MonoBehaviour
{
    public GameObject drinkingFX; 
    private bool isDrinkConsumed = false;

    public void ConsumeDrink()
    {
        if (drinkingFX != null && !isDrinkConsumed)
        {
            GameObject drinkingEffect = Instantiate(drinkingFX, transform.position, Quaternion.identity);
            Destroy(drinkingEffect, 1f);

            isDrinkConsumed = true;
        }     
    }
}
