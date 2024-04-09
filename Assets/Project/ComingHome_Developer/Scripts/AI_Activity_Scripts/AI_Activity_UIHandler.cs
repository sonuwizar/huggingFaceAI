using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Addressable;
public class AI_Activity_UIHandler : MonoBehaviour
{
    public static AI_Activity_UIHandler instance;

    public GameObject jammoPlayer;
    public GameObject helloDescriptionPanel;
    public Button playBtn;
    public Button playerMove2Times , playerMove3Times;
    public Button helloBtn;
    public Button rightRotateBtn;
    public Button leftRotateBtn;
    public Button inputNameBtn;
    public GameObject inputFieldPanel;
    public GameObject inputDescriptionPanel;
    internal int stepCount = 0, stepsToMove;
    public Text countText;
    public Text updatedNameText;
    public Text playerText;
    public GameObject wellDonePanel;
    public GameObject celebrationEffect;
    public Text stepTakenTxt;
    public Text XpEarnedTxt;
    public AnimationClip animClip;
    public Button nextDescriptionBtn;

    private void Awake()
    {
        instance = this;
    }

    
    private void Start()
    {
        //AI_Activity_MultiDescription.instance.SetDesc_VonHi();
        updatedNameText.gameObject.SetActive(false);
    }

    public void PlayBtn()
    {
        playBtn.gameObject.SetActive(false);
        jammoPlayer.GetComponent<ThirdPersonController_V2>().isControllingEnabled = false;
        AI_Activity_UIHandler.instance.jammoPlayer.GetComponent<Animator>().SetFloat("Blend", 0);
        jammoPlayer.transform.position = AI_Activity_MovePlayer.instance.aiBoardPosMatrix[0, 0].position;
        jammoPlayer.transform.localEulerAngles = Vector3.zero;
        playerMove2Times.gameObject.SetActive(true);
        playerMove3Times.gameObject.SetActive(true);
        helloBtn.gameObject.SetActive(true);
        rightRotateBtn.gameObject.SetActive(true);
        leftRotateBtn.gameObject.SetActive(true);
        inputNameBtn.gameObject.SetActive(true);
        AI_Activity_MovePlayer.instance.InitDirection();
        AI_Activity_MultiDescription.instance.Set7esc_Von();
        updatedNameText.gameObject.SetActive(true);
    }
    public void HelloBtn()
    {
        playBtn.gameObject.SetActive(false);
        helloDescriptionPanel.SetActive(true);
        StartCoroutine(ImageAnimation());
        BtnInteractable(false);
    }

    IEnumerator ImageAnimation()
    {
        yield return new WaitForSeconds(animClip.length);
        helloDescriptionPanel.SetActive(false);
        BtnInteractable(true);
    }
    public void NameBtn()
    {
        inputFieldPanel.SetActive(true);
        inputDescriptionPanel.SetActive(true);
        BtnInteractable(false);
    }

    public void SubmitBtn()
    {
        inputDescriptionPanel.SetActive(false);
        inputFieldPanel.SetActive(false);
        string playerName = playerText.text;
        updatedNameText.text = playerName;
        BtnInteractable(true);
    }

    public void BtnInteractable(bool status)
    {
        playerMove2Times.GetComponent<Button>().interactable = status;
        rightRotateBtn.GetComponent<Button>().interactable = status;
        leftRotateBtn.GetComponent<Button>().interactable = status;
        helloBtn.GetComponent<Button>().interactable = status;
        inputNameBtn.GetComponent<Button>().interactable = status;
        playerMove3Times.GetComponent<Button>().interactable = status;
    }

    public void BtnOnOff(bool status)
    {
        playerMove2Times.gameObject.SetActive(status);
        rightRotateBtn.gameObject.SetActive(status);
        leftRotateBtn.gameObject.SetActive(status);
        helloBtn.gameObject.SetActive(status);
        inputNameBtn.gameObject.SetActive(status);
        playerMove3Times.gameObject.SetActive(status);
    }

    public void XpEarned()
    {
        if(stepCount <= 15)
        {
            Debug.Log("step to move_15");
            UIVersion_2.CoinManager_V2.instance.AddCoin(50);
            int textStore = 50;
            XpEarnedTxt.text = textStore.ToString(); 
        }

        else if (stepCount < 15 || stepCount <= 20)
        {
            Debug.Log("step to move_20");
            UIVersion_2.CoinManager_V2.instance.AddCoin(35);
            int textStore = 35;
            XpEarnedTxt.text = textStore.ToString();
        }
        else
        if(stepCount < 20 || stepCount <= 25)
        {
            UIVersion_2.CoinManager_V2.instance.AddCoin(30);
            int textStore = 30;
            XpEarnedTxt.text = textStore.ToString();
        }
        else
        if (stepCount < 25 || stepCount <= 30)
        {
            UIVersion_2.CoinManager_V2.instance.AddCoin(25);
            int textStore = 25;
            XpEarnedTxt.text = textStore.ToString();
        }

        else
        if (stepCount < 30|| stepCount <= 35)
        {
            UIVersion_2.CoinManager_V2.instance.AddCoin(20);
            int textStore = 20;
            XpEarnedTxt.text = textStore.ToString();
        }
        else
        if (stepCount < 35 || stepCount <= 40)
        {
            UIVersion_2.CoinManager_V2.instance.AddCoin(15);
            int textStore = 15;
            XpEarnedTxt.text = textStore.ToString();
        }
        else
        if (stepCount < 40 || stepCount <= 45)
        {
            UIVersion_2.CoinManager_V2.instance.AddCoin(10);
            int textStore = 10;
            XpEarnedTxt.text = textStore.ToString();
        }
        else 
        if(stepCount > 45)
         {
            Debug.Log("45");
            UIVersion_2.CoinManager_V2.instance.AddCoin(5);
            int textStore = 5;
            XpEarnedTxt.text = textStore.ToString();
        }
    }

    public void ReturnBtn()
    {
        ReadWriteDataToJson.instance.SetUnlockedLevelToDB(2);
        SceneLoader_Addressable.instance.LoadScene(0);
    }

    public void ReplayBtn()
    {
        SceneLoader_Addressable.instance.LoadCurrentScene();
    }

}
