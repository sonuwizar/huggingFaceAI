using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SEC_1
{
    public class GameManuUIManager_CharacterHandler : MonoBehaviour
    {
        public static GameManuUIManager_CharacterHandler instance;

        public LockBtnData[] levelBtnData;
        public GameObject characterObject;
        internal int prevUnlockLevel;
        internal int unlockLevel;

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            int unlockedLevel = ReadWriteDataToJson.instance.GetPreviousUnlockedLevel();
            //int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");
            Debug.Log("Unlocked level" + unlockedLevel);
            for (int i = 0; i < levelBtnData.Length; i++)
            {
                levelBtnData[i].levelBtn.interactable = !(i > unlockedLevel);
                //levelBtnData[i].levelLockObject.SetActive(i > unlockedLevel);
            }

            characterObject.transform.position = levelBtnData[unlockedLevel].levelBtn.transform.position;
            //Debug.Log("Level btn data------1" + characterObject.transform.position);
            //Debug.Log("Level btn data-------2" + levelBtnData[unlockedLevel].levelBtn.transform.position);

            MoveCharacter();
        }
        #region Character movement

        public void MoveCharacter()
        {
            StartCoroutine(MoveCharacterRoutine());
        }

        IEnumerator MoveCharacterRoutine()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            int prevUnlockLevel = ReadWriteDataToJson.instance.GetPreviousUnlockedLevel();
            int unlockLevel = ReadWriteDataToJson.instance.GetUnlockedLevel();

            //int prevUnlockLevel = PlayerPrefs.GetInt("PrevUnlockLevel");
            //int unlockLevel = PlayerPrefs.GetInt("UnlockLevel");

            Debug.Log("prevv"+prevUnlockLevel);
            Debug.Log("unlock"+unlockLevel);

            if (prevUnlockLevel < unlockLevel)
            {
                float moveTime = 0;
                Vector3 objectIniPosition = levelBtnData[prevUnlockLevel].levelBtn.transform.position;
                Vector3 objectTargetPosition = levelBtnData[unlockLevel].levelBtn.transform.position;

                while (moveTime < 1)
                {
                    moveTime += 0.01f;

                    characterObject.transform.position = Vector3.Lerp(objectIniPosition, objectTargetPosition, moveTime);

                    yield return new WaitForSeconds(0.01f);
                }

                levelBtnData[unlockLevel].levelBtn.interactable = true;
                //levelBtnData[unlockLevel].levelLockObject.SetActive(false);
                ReadWriteDataToJson.instance.SetPreviousUnlockedLevel();
            }
        }

        #endregion

    }

    [System.Serializable]
    public class LockBtnData
    {
        public Button levelBtn;
        //public GameObject levelLockObject;
    }
}
