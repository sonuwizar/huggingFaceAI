using UnityEngine;
using UnityEngine.UI;

public class ParentCoinManager : MonoBehaviour
{

    [SerializeField] private Text WizBuxText;
    [SerializeField] private Text WizCoinText;

    public static ParentCoinManager INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;

        //SetWizBux(GameManager.Instance._userInfo.userDetails.WizbuxCoin);
        //SetGems(GameManager.Instance._userInfo.userDetails.ExperiencePoint);
        //WizBuxText.text = GameManager.Instance._userInfo.userDetails.WizbuxCoin.ToString();
        //WizCoinText.text = GameManager.Instance._userInfo.userDetails.ExperiencePoint.ToString();
    }

    public void SetWizBux(int coinToAdd)
    {
        //Debug.Log(GameManager.Instance._userInfo.userDetails.WizbuxCoin);
        //GameManager.Instance._userInfo.userDetails.WizbuxCoin += coinToAdd;
        //WizBuxText.text = GameManager.Instance._userInfo.userDetails.WizbuxCoin.ToString(); 
    }

    public void SetGems(int coinToAdd)
    {
        //GameManager.Instance._userInfo.userDetails.ExperiencePoint += coinToAdd;
        //WizCoinText.text = GameManager.Instance._userInfo.userDetails.ExperiencePoint.ToString();
    }
    public int GetWizBux()
    {
        //Debug.Log("Total coin counts" +GameManager.Instance._userInfo.userDetails.WizbuxCoin);
        //return GameManager.Instance._userInfo.userDetails.WizbuxCoin;
        return 1;
    }

    public void OnClickGetCoinBtn(Text txt)
    {
        //Debug.Log(GetWizBux());
        //txt.text= GetWizBux().ToString();
    }

    //public int GetGems()
    //{
    //    //r/*eturn GameManager.Instance._userInfo.userDetails.ExperiencePoint;*/
    //}



}
