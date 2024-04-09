using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.Ver2
{
    public class DressCustomizationManager : DressCustomizationDataCollector
    {
        public AvtarParts[] avtarParts;

        int selectedPartIndex;

        // Start is called before the first frame update
        void Start()
        {
            base.Start();

            for (int i = 0; i < avtarParts.Length; i++)
            {
                int index = i;
                avtarParts[i].partToggle.onValueChanged.AddListener(delegate { SelectPartsUIPanel(index); });
            }

            StartCoroutine(InitialisePartsToggles());
        }

        IEnumerator InitialisePartsToggles()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < avtarParts.Length; i++)
            {
                tempIndex = i;
                int partOptionIndex = ReadWriteDataToJson_V2.instance.GetPartOption(tempIndex);
                //Debug.Log("Part- " + avtarParts[i].name + ", " + partOptionIndex);

                SetPartOptionToggle(partOptionIndex);
            }
        }

        void SelectPartsUIPanel(int index)
        {
            for (int i = 0; i < avtarParts.Length; i++)
            {
                avtarParts[i].partOptionUIParent.SetActive(i == index);
            }
            selectedPartIndex = index;
        }

        void SetPartOptionToggle(int index)
        {
            for (int j = 0; j < avtarParts[tempIndex].partsOptionToggles.Length; j++)
            {
                avtarParts[tempIndex].partsOptionToggles[j].isOn = (index == j);
            }
        }

        public void SelectPartOption(int index) => SetPartOptionModel(selectedPartIndex, index);
    }

    [System.Serializable]
    public class AvtarParts
    {   
        public string name;
        public Toggle partToggle;
        public GameObject partOptionUIParent;
        public Toggle[] partsOptionToggles;
    }
}
