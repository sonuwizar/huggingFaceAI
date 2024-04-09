using UnityEngine;
using Addressable;

public class CHMVW_GameManager : MonoBehaviour
{
    public static CHMVW_GameManager instance;

    public int activitySceneIndex , currStageIndex;
    internal bool isReturn = false;
    internal int currIndex;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("backFromLab"))//to load env automatic when return from Lab
        {
            isReturn = PlayerPrefs.GetString("backFromLab").Equals("True");
            if(isReturn && PlayerPrefs.HasKey("CHMVW_Activity"))
            {
                currIndex = PlayerPrefs.GetInt("CHMVW_Activity");
                LoadLevel(currIndex);
            }
        }
    }

    public void GameLevel(int index)
    {
        PlayerPrefs.SetString("backFromLab", "False");
        PlayerPrefs.SetString("backFromQuest", "False");
        LoadLevel(index);
    }

    void LoadLevel(int index)
    {
        PlayerPrefs.SetInt("CHMVW_Activity", index);

        if (index == 0)
        {
            PlayerPrefs.SetString("learnverseActivityType", "Intro");
        }
        else
        {
            PlayerPrefs.SetString("learnverseActivityType", "WorkingActivity");
        }

        SceneLoader_Addressable.instance.LoadScene(activitySceneIndex);
    }

}
