using System.Collections;
using UnityEngine;
using DescriptionWriter;

public class MultiDescription_DroneActivity : MonoBehaviour
{
    public static MultiDescription_DroneActivity instance;

    public InstructionsData[] vonHI_InstData, von2_InstData, von3_InstData, von4_InstData, von5_InstData, von6_InstData , von7_InstData, von8_InstData , von9_InstData , von10_InstData , von11_InstData , von12_InstData, von13_InstData;
    public GameObject descriptionPanel;
    internal bool isNextBtnOff;
    internal int descIndex;
    
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        TextWriter.onClickClose += AfterClickClosed;
    }

    private void OnDisable()
    {
        TextWriter.onClickClose -= AfterClickClosed;
    }

    private void Start()
    {
        descriptionPanel.gameObject.SetActive(false);
        if (!BagManager_DroneActivity.instance.isItemCollected)
        {
            if (CHMVW_GameplaySceneHandler.instance.backFromLab)
            {
                Debug.Log("BackfromPosition");
                DroneDescriptionTriggerHandler_DroneActivity.instance.descTrigger_2.SetActive(true);
                DroneDescriptionTriggerHandler_DroneActivity.instance.descTrigger.SetActive(false);
                StartCoroutine(SetPlayerPosition());
            }
            else
            {
                DroneDescriptionTriggerHandler_DroneActivity.instance.descTrigger.SetActive(true);
                DroneDescriptionTriggerHandler_DroneActivity.instance.descTrigger_2.SetActive(false);

                ThirdPersonController_V2.instance.isControllingEnabled = false;
                ThirdPersonController_V2.instance.transform.localPosition = Vector3.zero;
                ThirdPersonController_V2.instance.transform.localEulerAngles = Vector3.zero;

                SetDesc_VonHi();
                SEC_1.NPC_MovementController_Introduction.instance._agent.transform.localPosition = new Vector3(2f, 0f, 2f);
                SEC_1.NPC_MovementController_Introduction.instance._agent.transform.eulerAngles = new Vector3(0, -142, 0);
                SEC_1.NPC_MovementController_Introduction.instance.targetObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            DroneDescriptionTriggerHandler_DroneActivity.instance.descTrigger.SetActive(false);
            DroneDescriptionTriggerHandler_DroneActivity.instance.descTrigger_2.SetActive(false);
        }
    }

    IEnumerator SetPlayerPosition()
    {
        ThirdPersonController_V2.instance.isControllingEnabled = false;
        yield return new WaitForEndOfFrame();
        ThirdPersonController_V2.instance.transform.localPosition = new Vector3(7.7f, 0f, 9.4f);
        ThirdPersonController_V2.instance.transform.localEulerAngles = new Vector3(0, -90, 0);
        Debug.Log("JammoPosition" + ThirdPersonController_V2.instance.transform.localPosition);
        yield return new WaitForEndOfFrame();
        ThirdPersonController_V2.instance.isControllingEnabled = true;
    }

    public void SetDesc_VonHi()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(vonHI_InstData);
    }

    public void Set2Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von2_InstData);
    }

    public void Set3Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von3_InstData);
    }

    public void Set4Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von4_InstData);
    }

    public void Set5Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von5_InstData);
    }

    public void Set6Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von6_InstData);
    }

    public void Set7Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von7_InstData);
    }

    public void Set8Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von8_InstData);
    }

    public void Set9Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von9_InstData);
    }

    public void Set10Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von10_InstData);
    }

    public void Set11Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von11_InstData);
    }

    public void Set12Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von12_InstData);
    }

    public void Set13Desc_Von(bool status)
    {
        TextWriter.instance.gameObject.SetActive(status);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von13_InstData);
    }


    void AfterClickClosed()
    {
        StartCoroutine(PanelOnOff());
    }

    IEnumerator PanelOnOff()
    {
        yield return new WaitForEndOfFrame();
        if (CHMVW_GameplaySceneHandler.instance.backFromLab)
        {
            switch (descIndex)
            {
                case 0:
                    Set5Desc_Von();
                    break;
                case 1:
                    Set6Desc_Von();
                    break;
                case 8:
                    Set12Desc_Von();
                    break;
                default:
                    break;
            }
            Debug.Log("descIndex" + descIndex);
            descIndex++;
        }
        else
        {
            switch (descIndex)
            {
                case 0:
                    Set2Desc_Von();
                    break;
                case 1:
                    ThirdPersonController_V2.instance.isControllingEnabled = true;
                    SEC_1.NPC_MovementController_Introduction.instance.target5Object.SetActive(true);
                    SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target5Object;
                    //SEC_1.NPC_MovementController_Introduction.instance.SetMovement(true);
                    SEC_1.RobotSphere_Controller.instance.ManageRobot_OtherStages();
                    StartCoroutine(Movement());
                    Set3Desc_Von();
                    break;
                case 3:
                    Set5Desc_Von();
                    break;
                case 4:
                    Set6Desc_Von();
                    break;
                case 11:
                    Set12Desc_Von();
                    break;
                default:
                    break;
            }
            Debug.Log("descIndex" + descIndex);
            descIndex++;
        }
    }
    IEnumerator Movement()
    {
        yield return new WaitForSeconds(4f);
        SEC_1.NPC_MovementController_Introduction.instance.npcMovement = true;
        SEC_1.NPC_MovementController_Introduction.instance.robotControllerNPC.SetWalkAnim(true);
    }
}
