using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SeatPlayer : MonoBehaviour
{
    public GameObject playerController;
    public GameObject seatPos;
    public GameObject seatPrep;
    public GameObject entry;
    public OrderFood orderManager;
    public TextMeshProUGUI text;
    public GameObject napkinTimer;
    public Transform target;



    public GameObject meal;
    public GameObject drink;

    private bool canPlaceOrder = true;
    private bool canOrderDrink = true;

    public void EnterChair()
    {      
        playerController.SetActive(false);
        seatPos.SetActive(true);
        entry.SetActive(false);
        seatPos.transform.rotation = Quaternion.Euler(0, -target.rotation.eulerAngles.y, 0);

        StartCoroutine(showUiTimer());
    }
    public void ExitChair()
    {
        playerController.SetActive(true);
        seatPos.SetActive(false);
        StartCoroutine(LeaveSeatDelay());

    }

    public void OrderADish(int pos)
    {
        if (canPlaceOrder)
        {
            StartCoroutine(OrderTimer(pos));
        }
        else
        {
            Debug.Log("Please wait before placing another order.");
        }
    }
    public void OrderADrink(int pos)
    {
        if (canOrderDrink)
        {
            StartCoroutine(DrinkOrderTimer(pos));
        }
        else
        {
            Debug.Log("Please wait before placing another order.");
        }
    }
    IEnumerator showUiTimer()
    {
            orderManager.PlayDialogue(1);
            napkinTimer.GetComponent<Timer>().seconds = 8;
            napkinTimer.GetComponent<Timer>().StartTimer();
            text.text = "Preparing Your Table!";
            orderManager.PlayDialogue(2);
            yield return new WaitForSeconds(8f);
            seatPrep.SetActive(true);
            text.text = "You Can Place Order Now!";
            orderManager.PlayDialogue(3);
       
    }

    IEnumerator OrderTimer(int pos)
    {
        canPlaceOrder = false;
        napkinTimer.GetComponent<Timer>().seconds = 20;
        napkinTimer.GetComponent<Timer>().StartTimer();
        text.text = "Waiting For Your " + orderManager.dishList[pos].name;
        orderManager.PlayDialogue(4);
        yield return new WaitForSeconds(20f);
        Transform platPos = seatPrep.transform.GetChild(4).transform;
        if(meal !=null) Destroy(meal);
        meal = Instantiate(orderManager.dishList[pos], platPos.position, Quaternion.identity, platPos);
        text.text = "Enjoy Your Food!";
        orderManager.PlayDialogue(5);
        yield return new WaitForSeconds(5f);
        canPlaceOrder = true;
    }

    IEnumerator DrinkOrderTimer(int pos)
    {
        yield return new WaitForSeconds(5f);
        Transform platPos = seatPrep.transform.GetChild(5).transform;
        if (drink != null) Destroy(drink);
        drink = Instantiate(orderManager.drinkList[pos], platPos.position, Quaternion.identity, platPos);
    }

    IEnumerator LeaveSeatDelay()
    {
        yield return new WaitForSeconds(2f);
        entry.SetActive(true);
    }
}
