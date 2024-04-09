using UnityEngine;
using UnityEngine.UI;

public class UI_Handler_DroneActivity : MonoBehaviour
{
    public static UI_Handler_DroneActivity instance;

    public GameObject collectBtn;
    public GameObject nextDescriptionButn;

    private void Awake()
    {
        instance = this; 
    }

   
    private void Start()
    {
        
    }

    public void SetCompoCollectBtn(int index, bool status)
    {
        if(status)
        {
            collectBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            collectBtn.GetComponent<Button>().onClick.AddListener(() => CollectComponent(index));
        }
        collectBtn.SetActive(status);
    }

    void CollectComponent(int index)
    {
        collectBtn.SetActive(false);
        BagManager_DroneActivity.instance.CollectComponent(index);
    }

}
