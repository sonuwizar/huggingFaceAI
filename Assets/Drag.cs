using UnityEngine;



public class DragAndDrop : MonoBehaviour
{

    // The plane the object is currently being dragged on
    Plane dragPlane;

    // The difference between where the mouse is on the drag plane and 
    // where the origin of the object is on the drag plane
    Vector3 offset;

    Camera myMainCamera;
    public Transform targetObject;


    void Start()
    {
        myMainCamera = Camera.main;
    }

    void OnMouseDown()
    {

        dragPlane = new Plane(myMainCamera.transform.forward, targetObject.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = targetObject.position - camRay.GetPoint(planeDist);
    }

    void OnMouseDrag()
    {

        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        targetObject.position = camRay.GetPoint(planeDist) + offset;
    }
}