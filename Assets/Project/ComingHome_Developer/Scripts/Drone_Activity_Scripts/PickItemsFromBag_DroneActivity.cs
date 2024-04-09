using System.Collections.Generic;
using UnityEngine;

public class PickItemsFromBag_DroneActivity : MonoBehaviour
{
    public static PickItemsFromBag_DroneActivity instance;

    public List<GameObject> pickObjects , objectTargetList;

    List<GameObject> tempPickObjects;
    GameObject currPickObject;
    Vector3 iniPos ;
    Transform iniParent;
    bool isModelPicked;
    internal bool isDronePicked;

    void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        tempPickObjects = new List<GameObject>();
        referencelist();
    }

    public void PickItemsFromBag(string name)
    {
        if(!isDronePicked)
        {
            for (int i = 0; i < pickObjects.Count; i++)
            {
                if (pickObjects[i].name.Equals(name))
                {
                    pickObjects[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            AttachObjects_DroneActivity.instance.orginaldrone.SetActive(true);
            AttachObjects_DroneActivity.instance.orginaldrone.transform.position = new Vector3(15.5f, -1.65f, -19.3f);
            AttachObjects_DroneActivity.instance.orginaldrone.transform.localScale = new Vector3(.5f, .5f, .5f);
            DroneAnimationHandler_DroneActivity.instance.flyBtn.gameObject.SetActive(true);
        }
    }


    public void referencelist()
    {
        for (int i = 0; i < pickObjects.Count; i++)
        {
            tempPickObjects.Add(pickObjects[i]);
        }
    }

    public GameObject OnModelDown(Transform hitTransform)
    {
        for (int i = 0; i < tempPickObjects.Count; i++)
        {
            if(hitTransform.name.Equals(tempPickObjects[i].name))
            {
                Debug.Log("tempPickObjects");
                currPickObject = tempPickObjects[i];
                iniPos = currPickObject.transform.position;
                isModelPicked = true;
                AttachObjects_DroneActivity.instance.highlighter.SetActive(true);
                currPickObject.GetComponent<BoxCollider>().enabled = false;
                return currPickObject;
            }
        }
        return null;
    }

    public bool OnModelUp(Transform hitTransform)
    {
        if (hitTransform.name.Equals(AttachObjects_DroneActivity.instance.highlighter.name))
        {
            Debug.Log("On Model Up"+ hitTransform.name);
            return AttachObjects_DroneActivity.instance.AttachObjects(currPickObject.transform);
        }
        else
        {
            return false;
        }
    }

    public void ResetModel()
    {
        currPickObject.transform.position = iniPos;
        AttachObjects_DroneActivity.instance.highlighter.SetActive(false);
        currPickObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void MoveObjectToTarget(int index)
    {
        currPickObject.transform.SetParent(objectTargetList[index].transform);
        currPickObject.transform.localPosition = Vector3.zero;
        currPickObject.transform.localEulerAngles = Vector3.zero;
        currPickObject.transform.localScale = Vector3.one;
    }
}
