using UnityEngine;

namespace Customisation.Ver2
{
    public class ColorCustomizationManager : CustomisationModelDataCollector
    {
        [Header("Parts button colors")]
        public Color partsButton_iniColor;
        public Color partsButton_SelectedColor;

        int selectedModelIndex;


        private void OnEnable()
        {
            ColorPicker.setColor += SetModelColor;
        }

        private void OnDisable()
        {
            ColorPicker.setColor -= SetModelColor;
        }

        public void SetModelColor(Color color)
        {
            if (currCustomizationModel != null)
            {
                for (int j = 0; j < currCustomizationModel.rendererParts.Length; j++)
                {
                    currCustomizationModel.rendererParts[j].material.SetColor(currCustomizationModel.shaderColorVariableName, color);
                }
                ReadWriteDataToJson_V2.instance.StoreColor(color, selectedModelIndex);
            }
        }

        public void SetCustomizationModel(string partName)
        {
            for (int i = 0; i < customizationModelData.Length; i++)
            {
                if (customizationModelData[i].modelPartName.ToLower().Equals(partName.ToLower()))
                {
                    selectedModelIndex = i;
                    currCustomizationModel = customizationModelData[i];
                    break;
                }
            }
        }

    }
}

