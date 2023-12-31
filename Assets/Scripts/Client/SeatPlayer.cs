using System.Collections;
using TMPro;
using UnityEngine;

public class SeatPlayer : MonoBehaviour
{
    public GameObject playerController;
    public GameObject seatPos;
    public GameObject seatPrep;
    public GameObject[] entrys;
    public OrderFood orderManager;
    public TextMeshProUGUI text;
    public GameObject napkinTimer;
    public Transform target;


    private GameObject meal;
    private GameObject drink;

    private bool canPlaceOrder = false;
    private bool canOrderDrink = true;


    public void EnterChair()
    {
        playerController.SetActive(false);
        seatPos.SetActive(true);
        foreach (var entry in entrys) { entry.SetActive(false); }       
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
            napkinTimer.SetActive(true);
            napkinTimer.GetComponent<Timer>().seconds = 8;
            napkinTimer.GetComponent<Timer>().StartTimer();
            text.text = "Preparing Your Table!";
            yield return new WaitForSeconds(6f);
            orderManager.PlayDialogue(6);
            seatPrep.SetActive(true);
            yield return new WaitForSeconds(4f);
            orderManager.PlayDialogue(2);
            yield return new WaitForSeconds(3f);
            canPlaceOrder = true;         
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
        Transform platPos = seatPrep.transform.Find("platPos");
        if (meal !=null) Destroy(meal);
        meal = Instantiate(orderManager.dishList[pos], platPos.position, Quaternion.identity, platPos);
        text.text = "Enjoy Your Food!";
        orderManager.PlayDialogue(5);
        yield return new WaitForSeconds(5f);
        canPlaceOrder = true;
    }

    IEnumerator DrinkOrderTimer(int pos)
    {
        yield return new WaitForSeconds(3f);
        Transform platPos = seatPrep.transform.Find("drinkPos");
        if (drink != null) Destroy(drink);
        drink = Instantiate(orderManager.drinkList[pos], platPos.position, Quaternion.identity, platPos);
    }

    IEnumerator LeaveSeatDelay()
    {
        yield return new WaitForSeconds(2f);
        foreach (var entry in entrys) { entry.SetActive(true); }
    }

}
