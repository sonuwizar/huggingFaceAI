using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager_DroneActivity : MonoBehaviour
    {
        public static BagManager_DroneActivity instance;

        public Transform compoListParent;
        public GameObject bagComponentPrefab;

        [Header("UI Panels")]
        public GameObject bagPanelBlocker;
        public GameObject bagPanel, bagBtn;

        public GameObject collectingEffect;
        public GameObject vonMapTarget;

        public ComponentsData fan_1;
        public ComponentsData fan_2, fan_3, battery, stand, body,fan_4 , drone;

        internal List<Bag_ComponentsHandler_DroneActivity> bagComponents;
        internal List<GameObject> Objects = new List<GameObject>();
        Coroutine compoCollectingRoutine;
        internal int collectedComponentCount;
        internal GameObject currComp;
        internal bool isDroneColected;
        internal bool isItemCollected;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
        if(PlayerPrefs.HasKey("isItemCollected"))
        {
            isItemCollected = PlayerPrefs.GetString("isItemCollected").Equals("True");
        }
        bagComponents = new List<Bag_ComponentsHandler_DroneActivity>();

        InitialiseBagPanel(fan_1);
        InitialiseBagPanel(fan_2);
        InitialiseBagPanel(fan_3);
        InitialiseBagPanel(fan_4);
        InitialiseBagPanel(battery);
        InitialiseBagPanel(stand);
        InitialiseBagPanel(body);

    }

        public void SetBagPanel(bool status)
        {
        if (!status && bagBtn.GetComponent<Toggle>() != null)
            {
                bagBtn.GetComponent<Toggle>().isOn = false;
            }
                bagPanel.SetActive(status);
         }

    public void InitialiseBagPanel(ComponentsData compoData)
    {
        Debug.Log("Click on the btn");
        GameObject obj = Instantiate(bagComponentPrefab, compoListParent);
        Objects.Add(obj);
        Debug.Log("Gameobject obj" + obj.name);
        Bag_ComponentsHandler_DroneActivity currComponent = obj.GetComponent<Bag_ComponentsHandler_DroneActivity>();
        Debug.Log(" obj" + currComponent.name);
        currComponent.compoNameText.text = compoData.mainModel.name;
        currComponent.compoNameText.name = compoData.mainModel.name;
        currComponent.afterCollectingImage.SetActive(false);
        currComponent.compoSelectBtn.interactable = false;
        currComponent.compoSelectBtn.GetComponent<Image>().sprite = compoData.compoInitialIcon;
        currComponent.afterCollectingImage.GetComponent<Image>().sprite = compoData.afterCollectingCompoIcon;
        bagComponents.Add(currComponent);
    }
    public void AddComponentsOnBag(string name)
    {
        for (int i = 0; i < bagComponents.Count; i++)
        {
            Debug.Log("bag btn text- " + bagComponents[i].compoNameText.name + " , name- " + name);
            if (bagComponents[i].compoNameText.name.Equals(name))
            {
                bagComponents[i].afterCollectingImage.SetActive(true);
                bagComponents[i].compoSelectBtn.interactable = true;
                bagComponents[i].compoSelectBtn.onClick.AddListener(() => SelectComponentFromBag(name));
            }
        }
    }

    public void AddComponentOnBAgWithDB(string name)
    {
        AddComponentsOnBag(name);
        ReadWriteDataToJson.instance.SetComponentOnDB(name, true);
    }
    public void SelectComponentFromBag(string name)
    {
        for (int i = 0; i < bagComponents.Count; i++)
        {
            if (bagComponents[i].compoNameText.name.Equals(name))
            {
                bagComponents[i].compoSelectBtn.interactable = false;
                bagComponents[i].isUsed = true;
                PickItemsFromBag_DroneActivity.instance.PickItemsFromBag(name);
            }
        }
    }

    public void TriggeredComponents(string name, bool status)
    {
        int index = -1;
        if (name == fan_1.parentObject.name)
        {
            index = 0;
        }
        else
        if (name == fan_2.parentObject.name)
        {
            index = 1;
        }
        else
        if (name == fan_3.parentObject.name)
        {
            index = 2;
        }
        else
        if (name == fan_4.parentObject.name)
        {
            index = 3;
        }
        else
        if (name == battery.parentObject.name)
        {
            index = 4;
        }
        else
        if (name == stand.parentObject.name)
        {
            index = 5;
        }
        else
        if (name == body.parentObject.name)
        {
            index = 6;
        }
        if(index != -1)     
        {
            UI_Handler_DroneActivity.instance.SetCompoCollectBtn(index, status);
        }
    }

    public void CollectComponent(int index)
    {
        switch (index)
        {
            case 0:
                fan_1.parentObject.transform.parent.gameObject.SetActive(false);
                SetCompoCollectingEffect(fan_1.mainModel);
                AddComponentOnBAgWithDB(fan_1.mainModel.name);
                break;
            case 1:
                fan_2.parentObject.transform.parent.gameObject.SetActive(false);
                SetCompoCollectingEffect(fan_2.mainModel);
                AddComponentOnBAgWithDB(fan_2.mainModel.name);
                break;
            case 2:
                fan_3.parentObject.transform.parent.gameObject.SetActive(false);
                SetCompoCollectingEffect(fan_3.mainModel);
                AddComponentOnBAgWithDB(fan_3.mainModel.name);
                break;
            case 3:
                fan_4.parentObject.transform.parent.gameObject.SetActive(false);
                SetCompoCollectingEffect(fan_4.mainModel);
                AddComponentOnBAgWithDB(fan_4.mainModel.name);
                break;
            case 4:
                battery.parentObject.transform.parent.gameObject.SetActive(false);
                SetCompoCollectingEffect(battery.mainModel);
                AddComponentOnBAgWithDB(battery.mainModel.name);
                break;
            case 5:
                stand.parentObject.transform.parent.gameObject.SetActive(false);
                SetCompoCollectingEffect(stand.mainModel);
                AddComponentOnBAgWithDB(stand.mainModel.name);
                break;
            case 6:
                body.parentObject.transform.parent.gameObject.SetActive(false);
                SetCompoCollectingEffect(body.mainModel);
                AddComponentOnBAgWithDB(body.mainModel.name);
                break;
        }


    }

    public void SetCompoCollectingEffect(GameObject targetObject)
    {
        if (compoCollectingRoutine != null)
        {
            StopCoroutine(compoCollectingRoutine);
            collectingEffect.SetActive(false);
        }
        compoCollectingRoutine = StartCoroutine(CompoCollecing_Coroutine(targetObject));
        collectedComponentCount++;
        PlayerPrefs.SetString("isItemCollected", "True");
        if(collectedComponentCount == 7)
        {
            MultiDescription_DroneActivity.instance.Set7Desc_Von();
            vonMapTarget.SetActive(true);
            VonLab_DroneActivity.instance.vonTrigger.SetActive(true);
        }
    }

    IEnumerator CompoCollecing_Coroutine(GameObject targetObject)
    {
        collectingEffect.transform.position = targetObject.transform.position;
        collectingEffect.transform.eulerAngles = targetObject.transform.eulerAngles;
        collectingEffect.SetActive(true);
        yield return new WaitForSeconds(1);

        collectingEffect.SetActive(false);
    }

    public void UpdateDBAndCleanBag()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            Objects[i].SetActive(false);

        }
    }

    //public void Initialise(ComponentsData COMPO)
    //{
    //    Bag_ComponentsHandler_DroneActivity currComponent = GetComponent<Bag_ComponentsHandler_DroneActivity>();
    //    currComponent.afterCollectingImage.SetActive(true);
    //}


    [System.Serializable] 
    public class ComponentsData
    {
        public string modelName;
        public Sprite compoInitialIcon;
        public Sprite afterCollectingCompoIcon;
        public GameObject mainModel;
        public GameObject parentObject;
    }
}






