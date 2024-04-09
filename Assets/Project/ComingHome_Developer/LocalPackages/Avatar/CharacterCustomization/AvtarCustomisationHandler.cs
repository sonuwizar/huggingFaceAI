using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.Ver2
{
    public class AvtarCustomisationHandler : MonoBehaviour
    {
        public static AvtarCustomisationHandler instance;

        [SerializeField] Text modelNameText;
        [SerializeField] GameObject[] selectionUIParents;

        [Header("Selection UI Toggles")]
        [SerializeField] Toggle[] customisationSelectionToggles;

        [Header("Avtar Selection Buttons")]
        [SerializeField] Toggle[] avtarButtons;

        [Header("Avtar Model Data")]
        [SerializeField] AvtarCustomisationData[] avtarData;

        public delegate void OnCustomisationSubmit();
        public static OnCustomisationSubmit onCustomisationSubmit;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < customisationSelectionToggles.Length; i++)
            {
                int index = i;
                customisationSelectionToggles[i].onValueChanged.AddListener(delegate { ClickOnSelectionToggle(index); });
            }

            for (int i = 0; i < avtarButtons.Length; i++)
            {
                int index = i;
                avtarButtons[i].onValueChanged.AddListener(delegate { OnSelectAvtar(index); });
            }

            StartCoroutine(SelectToggles());
        }

        IEnumerator SelectToggles()
        {
            yield return new WaitForEndOfFrame();
            customisationSelectionToggles[0].isOn = true;
            avtarButtons[ReadWriteDataToJson_V2.instance.GetAvtar()].isOn = true;
            OnSelectAvtar(ReadWriteDataToJson_V2.instance.GetAvtar());
            ClickOnSelectionToggle(0);
        }

        void ClickOnSelectionToggle(int index)
        {
            for (int i = 0; i < selectionUIParents.Length; i++)
            {
                selectionUIParents[i].SetActive(i == index);
            }
        }

        void OnSelectAvtar(int index)
        {
            for (int i = 0; i < avtarData.Length; i++)
            {
                avtarData[i].model.SetActive(i == index);
                avtarData[i].customisationUIParent.SetActive(i == index);
            }
            modelNameText.text = avtarData[index].name;
            Debug.Log("Avtar- "+index);
            ReadWriteDataToJson_V2.instance.SetAvtar(index);
        }

        public void ClickSubmit()
        {
            ReadWriteDataToJson_V2.instance.StoreCustomisedDataToDb();

            if (onCustomisationSubmit != null)
            {
                onCustomisationSubmit();
            }
        }
    }

    [System.Serializable]
    public class AvtarCustomisationData
    {
        public string name;
        public GameObject model;
        public GameObject customisationUIParent;
    }
}
