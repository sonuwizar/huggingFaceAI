using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DescriptionWriter;

public class MultiDesc_Intro : MonoBehaviour
{
    public static MultiDesc_Intro instance;

    public InstructionsData[] vonHI_InstData, von2_Instruction, von3_Instruction , von4_Instruction , von5_Instruction , von6_Instruction , von7_Instruction , von8_Instruction, von9_Instruction;
    public GameObject descriptionPanel;
    public Button nextDescriptionBtn;
    bool isNextOffBtn;
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
        if (CHMVW_GameplaySceneHandler.instance.backFromLab)
        {
            Debug.Log("Back From Lab");
            SEC_1.NPC_MovementController_Introduction.instance._agent.transform.localPosition = new Vector3(4.6f, 0f, 7.8f);
            SEC_1.NPC_MovementController_Introduction.instance._agent.transform.eulerAngles = new Vector3(0, -142, 0);
            StartCoroutine(SetPlayerPosition());
            //SEC_1.RobotSphere_Controller.instance.SetOpenAnim(true);
        }
        else
        {
            Debug.Log("Start MVW");
            ThirdPersonController_V2.instance.isControllingEnabled = false;
            ThirdPersonController_V2.instance.transform.localPosition = Vector3.zero;
            ThirdPersonController_V2.instance.transform.localEulerAngles = Vector3.zero;
        }
    }

    IEnumerator SetPlayerPosition()
    {
        ThirdPersonController_V2.instance.isControllingEnabled = false;
        ThirdPersonController_V2.instance.transform.localPosition = new Vector3(7.7f, 0f, 9.4f);
        ThirdPersonController_V2.instance.transform.localEulerAngles = new Vector3(0, -90, 0);
        yield return new WaitForEndOfFrame();
        ThirdPersonController_V2.instance.isControllingEnabled = true;
    }

    public void SetDesc_VonHi()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(vonHI_InstData);
    }

    public void Set2Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von2_Instruction);
    }

    public void Set3Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von3_Instruction);
    }

    public void Set4Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von4_Instruction);
    }

    public void Set5Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von5_Instruction);
    }

    public void Set6Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von6_Instruction);
    }

    public void Set7Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von7_Instruction);
        StartCoroutine(VonDirectionToGate_Introduction());
    }

    public void Set8Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von8_Instruction);
    }

    public void Set9Desc_Von()
    {
        TextWriter.instance.SetMultiLineDesc_WithIcon(von9_Instruction);
    }

    public void AfterClickClosed()
    {
        StartCoroutine(PanelOnOff());
    }
         
        IEnumerator PanelOnOff()
        {
        Debug.Log("PanelOnoof" + descIndex);
            yield return new WaitForEndOfFrame();
            if (CHMVW_GameplaySceneHandler.instance.backFromLab)
            {
                Debug.Log("isReturn");
                switch (descIndex)
                {
                    case 0:
                        Debug.Log("Case 0");
                        Set9Desc_Von();
                        break;
                    case 1:
                    ReadWriteDataToJson.instance.SetUnlockedLevelToDB(1);
                    Addressable.SceneLoader_Addressable.instance.LoadScene(0);
                    break;
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
                        Set2Desc_Von();
                        break;
                    case 1:
                        Set3Desc_Von();
                        break;
                    case 2:
                        Set4Desc_Von();
                        break;
                    case 3:
                        Set5Desc_Von();
                        break;
                    case 4:
                        Set6Desc_Von();
                        break;
                    case 5:
                        Set7Desc_Von();
                        break;
                    default:
                        break;
                }
                descIndex++;
            }
        }
    IEnumerator VonDirectionToGate_Introduction()
    {
        yield return new WaitForSeconds(4f);
        SEC_1.NPC_MovementController_Introduction.instance.target2Object.SetActive(true);
        SEC_1.NPC_MovementController_Introduction.instance.target = SEC_1.NPC_MovementController_Introduction.instance.target2Object;
        SEC_1.NPC_MovementController_Introduction.instance.SetMovement(true);
        ThirdPersonController_V2.instance.isControllingEnabled = true;
        ThirdPersonController_V2.instance.isRotation = false;
    }
    }
       



