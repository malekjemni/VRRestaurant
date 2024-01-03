using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEditor.Progress;

public class TableQuest : MonoBehaviour
{
    public int ChairSocket = 0;
    public int napkinSocket = 0;
    public GameObject FX_Chair;
    public GameObject[] Chairs;
    public GameObject[] Napkins;
    public GameObject Tablecloth;

    public GameObject QuestUi;

    public Button myButton;

    private bool isPlaced = false;
    private bool isCloth = false;
    private bool isNapkin = false;

    public ResolveInteractor resolver;
    public List<GameObject> ChairSlots;
    public GameObject Tableclot_slot;
    public List<GameObject> NapkinSlots;

    private void Start()
    {
       myButton.onClick.AddListener(StartTablePrep);
    }
    public void StartTablePrep()
    {
        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(ChairPlacing);
    }
    public void ChairPlacing()
    {
        ChairSocket = 0;
        foreach (GameObject c in Chairs)
        {
            c.SetActive(true);
            SpawnFX(c, true);
        }
        QuestUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Task 1 : Place all Four chairs";
        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(TableclothPrep);
        myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "On Going...";
        myButton.interactable = false;
    }
    public void TableclothPrep()
    {
        isPlaced = true;
        foreach (GameObject cs in ChairSlots)
        {
           cs.GetComponent<XRGrabInteractable>().enabled = false;
        }      
        Tablecloth.SetActive(true);
        QuestUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Task 2 : Place table cloth";
        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(NapkinsPlacing);
        myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "On Going...";
        myButton.interactable = false;
    }

    public void NapkinsPlacing()
    {
        isCloth = true;
        Tableclot_slot.GetComponent<XRGrabInteractable>().enabled = false;
        napkinSocket = 0;
        foreach (GameObject n in Napkins)
        {
            n.SetActive(true);         
        }
        QuestUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Task 3 : Place all Four Napkins";
        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(TableIsDone);
        myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "On Going...";
        myButton.interactable = false;
    }
    public void TableIsDone()
    {
        isNapkin = true;
        foreach (GameObject ns in NapkinSlots)
        {
            ns.GetComponent<XRGrabInteractable>().enabled = false;
        }      
        QuestUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Task Completed!";
        myButton.onClick.RemoveAllListeners();
        myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Done!";
        myButton.interactable = false;
    }

    public void ValidateSocket(GameObject item)
    {
        if (item.name.Equals("Chair"))
        {
            ChairSocket++;
            SpawnFX(item, false);           
            AddToSlots(ChairSlots, resolver.interactor);
            CheckForCompletion(ChairSocket);
        }
        else if (item.name.Equals("Napkin"))
        {
            napkinSocket++;
            AddToSlots(NapkinSlots, resolver.interactor);
            resolver.UnfoldItem();
            FoldMeshRenderer(item, false);
            CheckForCompletion(napkinSocket);
        }
        else if (item.name.Equals("TableCloth"))
        {
            Tableclot_slot = resolver.interactor;
            myButton.interactable = true;
            resolver.UnfoldItem();
            myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Completed!";
            item.GetComponent<MeshRenderer>().enabled = false;

        }
        else Debug.Log("tag is not found");

    }
    public void RemoveSocket(GameObject item)
    {
        if (item.name.Equals("Chair"))
        {
            ChairSocket--;
            SpawnFX(item,true);
            CheckForCompletion(ChairSocket);
        }
        else if (item.name.Equals("Napkin"))
        {
            napkinSocket--;
            resolver.FoldItem();
            FoldMeshRenderer(item, true);
            CheckForCompletion(napkinSocket);
        }
        else if (item.name.Equals("TableCloth"))
        {
            myButton.interactable = false;
            resolver.FoldItem();
            myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "On Going...";
            item.GetComponent<MeshRenderer>().enabled = true;
        }
        else Debug.Log("tag is not found");
    }
    private void CheckForCompletion(int sockets)
    {
        if (sockets == 4)
        {
            myButton.interactable = true;
            myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Completed!";
        }
        else
        {
            myButton.interactable = false;
            myButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "On Going...";
        }
    }
    private void SpawnFX(GameObject item,bool _state)
    {
        if (!isPlaced)
        {
            item.transform.GetChild(1).gameObject.SetActive(_state);
        }
        
    }
    private void FoldMeshRenderer(GameObject item,bool _state)
    {
        if (!isNapkin) 
        {            
            item.transform.GetChild(1).GetComponent<FreezeItem>().isPlaced = !_state;
            item.GetComponent<MeshRenderer>().enabled = _state;
        }
    }

    private void AddToSlots(List<GameObject> myList,GameObject interactor)
    {
        if (myList.Contains(interactor))
        {
            myList.Remove(interactor);
        }
        myList.Add(interactor);
    }

    public void VisualCloth(bool _state)
    {
        if (!isCloth)
        {
            Tablecloth.transform.GetChild(1).gameObject.SetActive(_state);
        }
    }

    public void VisualNapkinIn(GameObject item)
    {
        if (!isNapkin)
        {
           item.GetComponent<FreezeItem>().VisualSetup(true);
        }
    }
    public void VisualNapkinOut(GameObject item)
    {
        if (!isNapkin)
        {
           item.GetComponent<FreezeItem>().VisualSetup(false);
        }
    }


}
