using UnityEngine;

public class FreezeItem : MonoBehaviour
{
    public bool isPlaced = false;


    public void VisualSetup(bool state) 
    {
        if (!isPlaced)
        {
            gameObject.SetActive(state);
        }
            
    }
}
