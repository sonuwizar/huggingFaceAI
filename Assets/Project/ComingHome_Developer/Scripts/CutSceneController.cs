using System.Collections;
using UnityEngine;

namespace SEC_1
{
    public class CutSceneController : MonoBehaviour
    {
        public static CutSceneController instance;

        public GameObject playerIniCameraParent, cutSceneCameraParent;

        [Header("CutScene Animatin Clip")]
        public AnimationClip iniClip;
        public AnimationClip cameraShakeAnimClip, cutSceneGameStage, vonIntroAnimClip;

        Animator cutSceneCameraAnimator;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
            cutSceneCameraAnimator = cutSceneCameraParent.GetComponentInChildren<Animator>();
        }

        public void InitializeCutScene(bool status)
        {
            playerIniCameraParent.SetActive(false);
            cutSceneCameraParent.SetActive(true);

            if (status)
            {
                cutSceneCameraAnimator.Play(iniClip.name);
            }
        }

        public void PlayAfterExplosion()
        {
            StartCoroutine(AfterExplosion_Coroutine());
        }

        IEnumerator AfterExplosion_Coroutine()
        {
            cutSceneCameraAnimator.Play(cameraShakeAnimClip.name);

            yield return new WaitForSeconds(cameraShakeAnimClip.length);

            cutSceneCameraAnimator.Play(cutSceneGameStage.name);
        }

        public void PlayVonIntro()
        {
            cutSceneCameraAnimator.Play(vonIntroAnimClip.name);
        }
    }
}
