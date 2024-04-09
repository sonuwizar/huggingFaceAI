using UnityEngine;
using UnityEngine.UI;
using Addressable;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager instance;

    public float totalQues;
    public Image slideImage;
    public GameObject gameOverPanel;

    internal float energyValue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            //Addressable.DontDestroy_Handler.instance.Add_To_DontDestroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SetSlider();
    }

    void SetSlider()
    {
        energyValue = Mathf.Round(totalQues / 2);
    }

    public void ReduceEnergy()
    {
        if (slideImage.fillAmount > 0)
        {
            slideImage.fillAmount -= 1 / energyValue;
        }

        if (slideImage.fillAmount == 0)
        {
            //gameOverPanel.SetActive(true);
        }
    }

    public void ClickRestartOnGameover(int sceneIndex)
    {
        gameOverPanel.SetActive(false);
        SetSlider();
        SceneLoader_Addressable.instance.LoadScene(sceneIndex);
    }
}

