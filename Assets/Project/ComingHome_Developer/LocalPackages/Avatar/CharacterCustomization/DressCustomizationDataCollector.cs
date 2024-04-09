using System.Collections;
using UnityEngine;

namespace Customisation.Ver2
{
    public class DressCustomizationDataCollector : MonoBehaviour
    {
        public AvtarPartsModel[] avtarPartsModels;

        internal int tempIndex;

        // Start is called before the first frame update
        public void Start()
        {
            ReadWriteDataToJson_V2.instance.InitializePartsIndex(avtarPartsModels.Length);
            StartCoroutine(InitialiseParts());
        }

        IEnumerator InitialiseParts()
        {
            yield return new WaitForEndOfFrame();
            
            for (int i = 0; i < avtarPartsModels.Length; i++)
            {
                tempIndex = i;
                int partOptionIndex = ReadWriteDataToJson_V2.instance.GetPartOption(tempIndex);

                SetPartOptionModel(tempIndex, partOptionIndex);
            }
        }

        public void SetPartOptionModel(int partIndex, int partOptionIndex)
        {
            for (int i = 0; i < avtarPartsModels[partIndex].partsOptionModel.Length; i++)
            {
                if (avtarPartsModels[partIndex].partsOptionModel[i] != null)
                {
                    avtarPartsModels[partIndex].partsOptionModel[i].SetActive(i == partOptionIndex);
                }
            }
            //Debug.Log("model activated- " + avtarPartsModels[partIndex].name + " , " + partOptionIndex);

            SetSelectedPartIndex(partIndex, partOptionIndex);
        }

        public void SetSelectedPartIndex(int partIndex, int partOptionIndex)
        {
            ReadWriteDataToJson_V2.instance.StorePartsOption(partIndex, partOptionIndex);
        }
    }

    [System.Serializable]
    public class AvtarPartsModel
    {
        public string name;
        public GameObject[] partsOptionModel;
    }
}
