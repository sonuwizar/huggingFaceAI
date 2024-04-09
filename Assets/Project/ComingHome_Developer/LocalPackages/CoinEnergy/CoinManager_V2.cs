using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UIVersion_2
{
    public class CoinManager_V2 : MonoBehaviour
    {
        public static CoinManager_V2 instance;

        public GameObject coinPrefab;
        public Transform coinParent;
        public AnimationClip coinAnimClip;
        public Text coinText;
        //public Image bgImage;

        public delegate void UpdateCoin(string text);
        public static event UpdateCoin updateCoin;

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
            UpdateCoinTxt();
        }

        public void AddCoin(int value)
        {
            if (value > 0)
            {
                GameObject obj = Instantiate(coinPrefab, coinParent);
                StartCoroutine(CollectCoin(obj, value));
            }
        }

        IEnumerator CollectCoin(GameObject obj, int value)
        {
            yield return new WaitForSeconds(coinAnimClip.length);
            Destroy(obj);
            ReadWriteDataToJson.instance.AddCoin(value);
            coinText.text = ReadWriteDataToJson.instance.GetTotalCoin().ToString();
            //if (MultiCircuitUIController.instance != null)
            //{
            //    MultiCircuitUIController.instance.UpdateCoinTxt();
            //}

            if (updateCoin != null)
            {
                string coinText = ReadWriteDataToJson.instance.GetCollectedCoin().ToString();
                updateCoin(coinText);
            }
        }

        internal void UpdateCoinTxt()
        {
            coinText.text = ReadWriteDataToJson.instance.GetTotalCoin().ToString();
        }
    }
}
