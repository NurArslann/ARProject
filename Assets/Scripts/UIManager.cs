using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private GraphicRaycaster raycaster;
    public Transform selectionPoint;
    private PointerEventData pData;
    private EventSystem eventSystem;
    // Start is called before the first frame update

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }

    }


    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        pData = new PointerEventData(eventSystem);
        pData.position = selectionPoint.position;
    }
    //GraphicRaycaster canvasta yani scrolbarda bir objeyle maskaleþtiðini kontrol eder 
    
    public bool OnEntered(GameObject button)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pData, results);
        foreach(RaycastResult result in results)
        {
            if (result.gameObject == button)
            {
                return true;
            }
        }
        return false;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
