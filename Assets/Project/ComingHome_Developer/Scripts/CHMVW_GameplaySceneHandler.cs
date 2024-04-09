using UnityEngine;
using Addressable;

public class CHMVW_GameplaySceneHandler : MonoBehaviour
{
    public static CHMVW_GameplaySceneHandler instance;

    public enum ActivitySelect {Introduction , AI_Activity , Drone_Activity , CarDetection_Activity ,ObstacleDetection_Activity , SmartLocker_Activity , DriverlessVehicle_Activity };
    public ActivitySelect activitySelect;

    public GameObject ai_ActivityPrefab , drone_ActivityPrefab , introductionPrefab;
    public GameObject canvasTxtjammo;
    public GameObject jammoPlayer;
    int currIndex;
    internal bool backFromLab=false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currIndex = PlayerPrefs.GetInt("CHMVW_Activity");
        if (PlayerPrefs.HasKey("backFromLab"))
        {
            backFromLab = PlayerPrefs.GetString("backFromLab").Equals("True");
            PlayerPrefs.SetString("backFromLab", "False");
        }
        activitySelect =(ActivitySelect)currIndex;

        switch (activitySelect)
        {
            case ActivitySelect.Introduction:
                Introduction(true);
                AI_Activity(false);
                Drone_Activity(false);
                if (backFromLab)
                {
                    MultiDesc_Intro.instance.Set8Desc_Von();
                }
                else
                {
                    MultiDesc_Intro.instance.descriptionPanel.SetActive(false);
                    SEC_1.NPC_MovementController_Introduction.instance.InitIntro();
                }
                PlayerPrefs.SetString("isItemCollected", "False");
                SEC_1.AudioHandler.instance.PlayCutSceneBg();
                break;
            case ActivitySelect.AI_Activity:
                AI_Activity(true);
                Introduction(false);
                PlayerPrefs.SetString("isItemCollected", "False");
                Drone_Activity(false);
                SEC_1.AudioHandler.instance.PlayCutSceneBg();
                break;
            case ActivitySelect.Drone_Activity:
                AI_Activity(false);
                Drone_Activity(true);
                Introduction(false);
                SEC_1.AudioHandler.instance.PlayCutSceneBg();
                break;
            case ActivitySelect.CarDetection_Activity:
                AI_Activity(false);
                Drone_Activity(false);
                Introduction(false);
                PlayerPrefs.SetString("isItemCollected", "False");
                break;
            case ActivitySelect.ObstacleDetection_Activity:
                AI_Activity(false);
                Drone_Activity(false);
                Introduction(false);
                PlayerPrefs.SetString("isItemCollected", "False");
                break;
            case ActivitySelect.SmartLocker_Activity:
                AI_Activity(false);
                Drone_Activity(false);
                Introduction(false);
                PlayerPrefs.SetString("isItemCollected", "False");
                break;
            case ActivitySelect.DriverlessVehicle_Activity:
                AI_Activity(false);
                Drone_Activity(false);
                Introduction(false);
                PlayerPrefs.SetString("isItemCollected", "False");
                break;
            default:
                break;
        }
    }

    public void AI_Activity(bool status)
    {
        ai_ActivityPrefab.SetActive(status);
        canvasTxtjammo.SetActive(status);
        jammoPlayer.GetComponent<AI_Activity_MovePlayer>().enabled = status;
    }

    public void CarDetection_Activity(bool status)
    {

    }

    public void Drone_Activity(bool status)
    {
        drone_ActivityPrefab.SetActive(status);
        jammoPlayer.GetComponent<Player_TriggerCollision_DroneActivity>().enabled = status;
    }

    public void Introduction(bool status)
    {
        introductionPrefab.SetActive(status);
    }

    public void SceneLoader()
    {
        SceneLoader_Addressable.instance.LoadScene(0);
    }

}
