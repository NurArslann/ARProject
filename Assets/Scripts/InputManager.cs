using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;
public class InputManager : ARBaseGestureInteractable
{
   //[SerializeField] private GameObject arObj;
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    Touch touch;
    private Pose pose;

    // Update is called once per frame
    /*  void Update()
     {
        touch = Input.GetTouch(0);

         if (Input.touchCount <= 0 || touch.phase != TouchPhase.Began)
             return;


         if (Input.GetMouseButtonDown(0))
         {
             Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
             if (_raycastManager.Raycast(ray, _hits))
             {
                 Pose pose = _hits[0].pose;
                 Instantiate(DataHander.Instance.GetFurnitre(), pose.position, pose.rotation);
                 //ekranda tıkladığım yerde mobilyamın oluşması için 
             }
         } 
}*/
    private void FixedUpdate()
    {
        CrosshairCalculation();
        //Belli bir süre içerisinde düzenli bir şekilde çalışan fonksiyon
    }
    bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;

    }
    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        Ray ray = arCam.ScreenPointToRay(origin);
        if (GestureTransformationUtility.Raycast(origin, _hits, TrackableType.PlaneWithinPolygon)) 
        { 
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles= new Vector3(90, 0, 0);
        }
    }
    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null)
            return true;
        return false;
    }
    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)
            return;
        if(gesture.targetObject !=null || IsPointerOverUI(gesture))
        {
            return;

        }
        if(GestureTransformationUtility.Raycast(gesture.startPosition, _hits, TrackableType.PlaneWithinPolygon))
        {
            GameObject placedOnj = Instantiate(DataHander.Instance.GetFurnitre(), pose.position, pose.rotation);
            var anchorObject = new GameObject(name: "PlacementAnchor");
            anchorObject.transform.position = pose.position;
            anchorObject.transform.rotation = pose.rotation;
            placedOnj.transform.parent = anchorObject.transform;
        }
    }
}
