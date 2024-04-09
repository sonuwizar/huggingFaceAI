using UnityEngine;

public class TableTrigger_DroneActivity : MonoBehaviour
{
    public static TableTrigger_DroneActivity instance;

    public GameObject tableTrigger;
    public Camera tableCamera;
    public GameObject bagPanelBlocker;
    public GameObject warningMsgPanel;
    public GameObject doorTrigger_Portal;

    void Start()
    {
        instance = this;
        doorTrigger_Portal.SetActive(false);
    }

    public void TableTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.name.Equals(tableTrigger.name))
        {
            if (BagManager_DroneActivity.instance.collectedComponentCount == 7)
            {
                tableCamera.gameObject.SetActive(true);
                tableTrigger.GetComponent<BoxCollider>().enabled = false;
                ThirdPersonController_V2.instance.gameObject.SetActive(false);
                bagPanelBlocker.SetActive(false);
                MultiDescription_DroneActivity.instance.Set8Desc_Von();
                Debug.Log("Collected Components");
            }
            else
            {
                warningMsgPanel.SetActive(true);
                Debug.Log("Not Collected Components");
            }
        }
    }

    public void TableTriggerExit(Collider other)
    {
        if (other.gameObject.transform.name.Equals(tableTrigger.name))
        {
            warningMsgPanel.SetActive(false);
        }
    }
}
