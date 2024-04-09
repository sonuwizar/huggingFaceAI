using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UIVersion_2
{
    public class WizBuxManager : MonoBehaviour
    {
        public static WizBuxManager instance;

        public Text wizbuxText;

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

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            UpdateWizaBux();
        }

        internal void UpdateWizaBux()
        {
            int wizBuxCount = ReadWriteDataToJson.instance.GetTotalWizbux();
            wizbuxText.text = wizBuxCount.ToString();
        }
    }
}
