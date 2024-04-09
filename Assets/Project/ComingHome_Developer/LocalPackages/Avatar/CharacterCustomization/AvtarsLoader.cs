using UnityEngine;

namespace Customisation.Ver2
{
    public class AvtarsLoader : MonoBehaviour
    {
        public static AvtarsLoader instance;

        [SerializeField] GameObject[] avtars;
        internal int avtarIndex;

        private void Awake()
        {
            instance = this;
            avtarIndex = ReadWriteDataToJson_V2.instance.GetAvtar();
        }

        private void Start()
        {
            for (int i = 0; i < avtars.Length; i++)
            {
                avtars[i].SetActive(i == avtarIndex);
            }
        }

        public GameObject SelectedAvtar()
        {
            return avtars[avtarIndex];
        }
    } 
}
