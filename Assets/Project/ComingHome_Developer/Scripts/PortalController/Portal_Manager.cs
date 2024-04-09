using UnityEngine;

namespace SEC_1
{
    public class Portal_Manager : MonoBehaviour
    {
        public static Portal_Manager instance;

        public portalData[] portals;

        private void Awake()
        {
            instance = this;
        }

        public void SetPortal(int index, bool status)
        {
            portals[index].parentPortal.SetActive(status);
            //if (NPC_MovementController.instance != null && (GameSceneManager.instance.currentStage != GameSceneManager.CurrentStage.InitialStage))
            //{
            //    NPC_MovementController.instance.SetTargetObject(portals[index].door_1);
            //}
        }
    }

    [System.Serializable]
    public class portalData
    {
        public GameObject parentPortal;
        public GameObject door_1, door_2;
        public GameObject entryPos_Door_1, entryPos_Door_2;
    }
}
