using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.Ver2
{
    public class CustomisationModelDataCollector : MonoBehaviour
    {
        [Header("Customization model data")]
        public CustomizationModelData[] customizationModelData;

        internal CustomizationModelData currCustomizationModel;

        // Start is called before the first frame update
        public virtual void Start()
        {
            StartCoroutine(InitialiseCustomisation());
        }

        IEnumerator InitialiseCustomisation()
        {
            yield return new WaitForEndOfFrame();
            ReadWriteDataToJson_V2.instance.InitialiseColorList(customizationModelData.Length);
            InitializeMaterial();
        }

        public virtual void InitializeMaterial()
        {
            for (int i = 0; i < customizationModelData.Length; i++)
            {
                Color color = ReadWriteDataToJson_V2.instance.GetColor(i);

                for (int j = 0; j < customizationModelData[i].rendererParts.Length; j++)
                {
                    customizationModelData[i].rendererParts[j].material = customizationModelData[i].requiredMaterial;
                    customizationModelData[i].rendererParts[j].material.SetColor(customizationModelData[i].shaderColorVariableName, color);
                }
            }
            currCustomizationModel = customizationModelData[0];
        }
    }

    [System.Serializable]
    public class CustomizationModelData
    {
        public string modelPartName;
        public Material requiredMaterial;
        public string shaderColorVariableName;
        public Image partsButtonImage;

        //[Header("Emission Type")]
        //public bool isEmissionRequired;
        //public string shaderEmissionColorName;

        [Header("RendererParts")]
        public Renderer[] rendererParts;
    }
}