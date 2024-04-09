using System.Collections.Generic;
using UnityEngine;

namespace Addressable
{
    public class DontDestroy_Handler : MonoBehaviour
    {
        public static DontDestroy_Handler instance;
        public int appSceneIndex = 0;

        List<GameObject> dontDestroyObject = new List<GameObject>();

        private void Awake()
        {
            instance = this;
        }

        internal void Add_To_DontDestroy(GameObject obj)
        {
            DontDestroyOnLoad(obj);
            dontDestroyObject.Add(obj);
        }

        internal void Remove_From_DontDestroy(GameObject obj)
        {
            if (dontDestroyObject.Contains(obj))
            {
                dontDestroyObject.Remove(obj);
                Destroy(obj);
            }
        }

        internal void Destroy_AllDontDestroy()
        {
            for (int i = 0; i < dontDestroyObject.Count; i++)
            {
                Destroy(dontDestroyObject[i]);
            }

            dontDestroyObject.Clear();
        }
    }
}
