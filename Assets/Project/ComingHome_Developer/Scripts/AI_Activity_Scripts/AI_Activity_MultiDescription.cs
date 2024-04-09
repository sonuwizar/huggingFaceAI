using System.Collections;
using UnityEngine;
using DescriptionWriter;

public class AI_Activity_MultiDescription : MonoBehaviour
{
    public static AI_Activity_MultiDescription instance;

    public InstructionsData[] vonHI_InstData, von2_InstData, von3_FollowData, von4_FollowData , von5_Intructions , von6_Intructions , von7_Intructions, von8_Intructions , von9_Intructions;
    public GameObject descriptionPanel;
    internal bool isNextBtnOff;
    internal int descIndex;
    bool isReturn;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ThirdPersonController_V2.instance.isControllingEnabled = false;

        if (CHMVW_GameplaySceneHandler.instance.backFromLab)
        {
            Set4Desc_Von();
            SEC_1.NPC_MovementController_Introduction.instance._agent.transform.localPosition = new Vector3(4.3f, 0.02f, 8f);
            SEC_1.NPC_MovementController_Introduction.instance._agent.transform.eulerAngles = new Vector3(0, -142, 0);
            SEC_1.NPC_MovementController_Introduction.instance.targetObject.GetComponent<BoxCollider>().enabled = false;
            ThirdPersonController_V2.instance.gameObject.transform.localPosition = new Vector3(7.31f, 0, 9.4f);
            ThirdPersonController_V2.instance.gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
           
        }
        else
        {
            ThirdPersonController_V2.instance.transform.localPosition = Vector3.zero;
            ThirdPersonController_V2.instance.transform.localEulerAngles = Vector3.zero;

            SetDesc_VonHi();
            SEC_1.NPC_MovementController_Introduction.instance._agent.transform.localPosition = new Vector3(2, 0, 2);
            SEC_1.NPC_MovementController_Introduction.instance._agent.transform.eulerAngles = new Vector3(0, -142, 0);
            SEC_1.NPC_MovementController_Introduction.instance.targetObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnEnable()
    {
        TextWriter.onClickClose += AfterClickClosed;
    }

    private void OnDisable()
    {
        TextWriter.onClickClose -= AfterClickClosed;
    }

    #region Start Description
    public void SetDesc_VonHi()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(vonHI_InstData);
    }

    public void Set2Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von2_InstData);
        //AI_Activity_UIHandler.instance.nextDescriptionBtn.gameObject.SetActive(false);
        //isNextBtnOff = true;
    }

    public void Set3Desc_Von()
    {
        ThirdPersonController_V2.instance.isControllingEnabled = true;
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von3_FollowData);
    }

    #endregion

    #region Return Description
    public void Set4Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von4_FollowData);
    }

    public void Set5Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von5_Intructions);
    }

    public void Set6Desc_Von()
    {
        ThirdPersonController_V2.instance.isControllingEnabled = true;
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von6_Intructions);
    }
    #endregion

    #region CommonDescription
    public void Set7esc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von7_Intructions);
    }
    public void Set8Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von8_Intructions);
    }
    public void Set9Desc_Von()
    {
        TextWriter.instance.gameObject.SetActive(true);
        TextWriter.instance.SetMultiLineDesc_WithIcon(von9_Intructions);
    }
    #endregion

    void AfterClickClosed()
    {
        StartCoroutine(PanelOnOff());
    }

    IEnumerator PanelOnOff()
    {
        yield return new WaitForEndOfFrame();
        if(CHMVW_GameplaySceneHandler.instance.backFromLab)
        {
            switch (descIndex)
            {
                case 0:
                    Debug.Log("Desc - 4" + descIndex);
                    Set5Desc_Von();
                    break;
                case 1:
                    Debug.Log("Desc - 5" + descIndex);
                    Set6Desc_Von();
                    SEC_1.NPC_MovementController_Introduction.instance.target4Object.SetActive(true);
                    SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target4Object;
                    //SEC_1.NPC_MovementController_Introduction.instance.SetMovement(true);
                    SEC_1.RobotSphere_Controller.instance.ManageRobot_OtherStages();
                    StartCoroutine(Movemnet_2());
                    break;
                case 4:
                    Debug.Log("Desc - 6" + descIndex);
                    Set9Desc_Von();
                    break;
                //case 4:
                //    Debug.Log("Desc - 7 " + descIndex);
                    //Set9Desc_Von();
                    //break;
                default:
                    break;
            }
            descIndex++;
        }
        else
        {
            switch (descIndex)
            {
                case 0:
                    Debug.Log("Desc -- 1" + descIndex);
                    AI_Activity_MultiDescription.instance.Set2Desc_Von();
                    break;
                case 1:
                    Debug.Log("Desc -- 2" + descIndex);
                    AI_Activity_MultiDescription.instance.Set3Desc_Von();
                    SEC_1.NPC_MovementController_Introduction.instance.target3Object.SetActive(true);
                    SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target3Object;
                    //SEC_1.NPC_MovementController_Introduction.instance.SetMovement(true);
                    SEC_1.RobotSphere_Controller.instance.ManageRobot_OtherStages();
                    StartCoroutine(Movement());
                    break;
                case 4:
                    Debug.Log("Desc -- 3" + descIndex);
                    Set9Desc_Von();
                    break;
                //case 2:
                //    AI_Activity_MultiDescription.instance.Set4Desc_Von();
                //    break;
                //case 3:
                //    AI_Activity_MultiDescription.instance.Set5Desc_Von();
                //    break;
                //case 5:
                //    AI_Activity_MultiDescription.instance.Set7esc_Von();
                //    break;
                //case 7:
                //    AI_Activity_MultiDescription.instance.Set9Desc_Von();
                //    break;
                default:
                    break;
            }
            descIndex++;
        }
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(4f);
        SEC_1.NPC_MovementController_Introduction.instance.npcMovement = true;
        SEC_1.NPC_MovementController_Introduction.instance.robotControllerNPC.SetWalkAnim(true);
    }

    IEnumerator Movemnet_2()
    {
        yield return new WaitForSeconds(4f);
        SEC_1.NPC_MovementController_Introduction.instance.npcMovement = true;
        SEC_1.NPC_MovementController_Introduction.instance.robotControllerNPC.SetWalkAnim(true);
    }
}
