using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttachObjects_DroneActivity : MonoBehaviour
{
    public static AttachObjects_DroneActivity instance;

    public GameObject highlighter;
    public Button collectBtn;
    public GameObject orginaldrone;
    public GameObject olddrone;

    internal int attachIndex;
    private void Awake()
    {
        instance = this;
    }
    
    public bool AttachObjects(Transform currObject)
    {
            if (currObject.name.Equals(PickItemsFromBag_DroneActivity.instance.pickObjects[attachIndex].name) || (attachIndex >=2 && currObject.name.ToLower().Contains("fan")))
            {
                Debug.Log("Attach objects"+attachIndex + "Objects" + currObject.name);
                highlighter.SetActive(false);
                PickItemsFromBag_DroneActivity.instance.MoveObjectToTarget(attachIndex);
                attachIndex++;
            
            if (attachIndex >= 7)
            {
                collectBtn.gameObject.SetActive(true);
                //VonLab_DroneActivity.instance.vonTrigger.SetActive(false);
            }
                return true;
        }
       
        return false;
    }

    public void CollectBtn(bool status)
    {
        orginaldrone.SetActive(!status);
        collectBtn.gameObject.SetActive(!status);
        TableTrigger_DroneActivity.instance.tableCamera.gameObject.SetActive(!status);
        TableTrigger_DroneActivity.instance.tableTrigger.GetComponent<BoxCollider>().enabled = !status;
        ThirdPersonController_V2.instance.gameObject.SetActive(status);
        TableTrigger_DroneActivity.instance.bagPanelBlocker.SetActive(status);
        BagManager_DroneActivity.instance.UpdateDBAndCleanBag();
        BagManager_DroneActivity.instance.InitialiseBagPanel(BagManager_DroneActivity.instance.drone);
        StartCoroutine(InitailiseBagComponents());
        DroneAnimationHandler_DroneActivity.instance.auraEffect.SetActive(true);
        PickItemsFromBag_DroneActivity.instance.isDronePicked = true;
        olddrone.SetActive(false);
        MultiDescription_DroneActivity.instance.Set9Desc_Von();
        SEC_1.NPC_MovementController_Introduction.instance.target8Object.SetActive(true);
        SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target8Object;
        SEC_1.NPC_MovementController_Introduction.instance.SetMovement(true);
        VonLab_DroneActivity.instance.isCollectedDroneAfterDesc = true;
    }

    IEnumerator InitailiseBagComponents()
    {
        yield return new WaitForEndOfFrame();
        BagManager_DroneActivity.instance.AddComponentOnBAgWithDB(BagManager_DroneActivity.instance.drone.mainModel.name);
    }

     public void VonMovingTargetPosition()
    {
        SEC_1.NPC_MovementController_Introduction.instance.target9Object.SetActive(true);
        SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target9Object;
        SEC_1.NPC_MovementController_Introduction.instance.SetMovement(true);
    }
}
