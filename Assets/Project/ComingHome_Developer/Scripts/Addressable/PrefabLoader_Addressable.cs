using UnityEngine;

namespace Addressable
{
    public class PrefabLoader_Addressable : MonoBehaviour
    {
        public static PrefabLoader_Addressable instance;

        private void Awake()
        {
            instance = this;
        }

        public GameObject InstantiatePrefab(string prefabName)
        {
            return Instantiate_Prefab(prefabName, null);
        }

        public GameObject InstantiatePrefab(string prefabName, Transform prefabParent)
        {
            return Instantiate_Prefab(prefabName, prefabParent);
        }

        GameObject Instantiate_Prefab(string prefabName, Transform prefabParent)
        {
            PrefabData[] pd = SceneManager_Addressable.instance.prefabData;

            for (int i = 0; i < pd.Length; i++)
            {
                if (pd[i].prefabName.ToLower().Equals(prefabName.ToLower()))
                {
                    return SceneManager_Addressable.instance.LoadSelectedPrefab(pd[i], prefabParent);
                }
            }
            return null;
        }

    }

}
