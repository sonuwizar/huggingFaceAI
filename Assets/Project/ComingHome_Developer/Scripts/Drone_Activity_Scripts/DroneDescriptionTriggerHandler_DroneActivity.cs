using UnityEngine;

public class DroneDescriptionTriggerHandler_DroneActivity : MonoBehaviour
{
    public static DroneDescriptionTriggerHandler_DroneActivity instance;

    public GameObject descTrigger;
    public GameObject descTrigger_1;
    public GameObject descTrigger_22;

    public GameObject descTrigger_2;

    private void Awake()
    {
        instance = this;
    }

    public void OnDescriptionTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.name.Equals(descTrigger.name) || (other.gameObject.transform.name.Equals(descTrigger_1.name)) || (other.gameObject.transform.name.Equals(descTrigger_22.name)) || other.gameObject.transform.name.Equals(descTrigger_2.name))
        {
            MultiDescription_DroneActivity.instance.Set4Desc_Von();
            other.gameObject.SetActive(false);
        }
    }
}
