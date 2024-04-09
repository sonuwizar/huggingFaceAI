using System.Collections;
using UnityEngine;

public class VonLab_DroneActivity : MonoBehaviour
{
    public static VonLab_DroneActivity instance;

    public GameObject vonTrigger;
    public GameObject portalTrigger , portalTrigger_2;

    internal bool isCollectedDroneAfterDesc;

    private void Start()
    {
        isCollectedDroneAfterDesc = false;
    }

    private void Awake()
    {
        instance = this;
    }

    public void OnTriggerVanEnter(Collider other)
    {
        if(other.gameObject.transform.name.Equals(vonTrigger.name))
        {
            ThirdPersonController_V2.instance.isControllingEnabled = false;
            TableTrigger_DroneActivity.instance.doorTrigger_Portal.SetActive(true);
            SEC_1.NPC_MovementController_Introduction.instance.target6Object.SetActive(true);
            SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target6Object;
            SEC_1.RobotSphere_Controller.instance.ManageRobot_OtherStages();
            StartCoroutine(VonMovingToTarget_6());
            ThirdPersonController_V2.instance.GetComponent<Animator>().SetFloat("Blend", 0);
            ThirdPersonController_V2.instance.transform.localEulerAngles = new Vector3(0, -211, 0);
            vonTrigger.SetActive(false);
        }

        else if (other.gameObject.transform.name.Equals(portalTrigger.name))
        {
            SEC_1.NPC_MovementController_Introduction.instance.target7Object.SetActive(true);
            SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target7Object;
            SEC_1.RobotSphere_Controller.instance.ManageRobot_OtherStages();
            StartCoroutine(VonMovingToTarget_7());
        }

        else if(other.gameObject.transform.name.Equals(portalTrigger_2.name))
        {
           if (isCollectedDroneAfterDesc)
            {
                MultiDescription_DroneActivity.instance.Set13Desc_Von(true);
                portalTrigger.SetActive(false);
            }
            else
            {
                MultiDescription_DroneActivity.instance.Set13Desc_Von(false);
                portalTrigger.SetActive(true);
            }
         
        }
    }

    IEnumerator VonMovingToTarget_6()
    {
        yield return new WaitForSeconds(5f);
        SEC_1.NPC_MovementController_Introduction.instance.npcMovement = true;
        SEC_1.RobotSphere_Controller.instance.SetWalkAnim(true);
        yield return new WaitForSeconds(1f);
        ThirdPersonController_V2.instance.isControllingEnabled = true;
    }

    IEnumerator VonMovingToTarget_7()
    {
        yield return new WaitForSeconds(4f);
        SEC_1.NPC_MovementController_Introduction.instance.npcMovement = true;
        //SEC_1.NPC_MovementController_Introduction.instance.robotControllerNPC.SetWalkAnim(true);
        SEC_1.RobotSphere_Controller.instance.SetWalkAnim(true);
    }
}
