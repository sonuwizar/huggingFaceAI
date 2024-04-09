using System.Collections;
using UnityEngine;

namespace SEC_1
{
    public class RobotSphere_Controller : MonoBehaviour
    {
        public static RobotSphere_Controller instance;

        public AnimationClip openAnimClip;
        Animator anim;
        //NPC_MovementController npcMovementController;

        // Start is called before the first frame update

        
        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            anim = GetComponentInChildren<Animator>();
            //npcMovementController = GetComponent<NPC_MovementController>();

            //if (GameSceneManager.instance.currentStage == GameSceneManager.CurrentStage.InitialStage || GameSceneManager.instance.currentStage == GameSceneManager.CurrentStage.GameStage)
            //{
            //    yield return new WaitForSeconds(1.5f);
            //    SetOpenAnim(true);
            //    yield return new WaitForSeconds(openAnimClip.length);
            //    npcMovementController.SetMovement(true);
            //}
            //else
            //{
            //    npcMovementController.SetNpcAtTarget();
            //    yield return new WaitForSeconds(1.5f);
            //    SetOpenAnim(true);
            //}
        }
        private void Awake()
        {
            instance = this;
        }

        public void ManageRobot_InitialAndGameStage()
        {
            StartCoroutine(RobotCoroutine_InitialandGameStage());
        }

        IEnumerator RobotCoroutine_InitialandGameStage()
        {
            yield return new WaitForSeconds(1.5f);
            SetOpenAnim(true);
            yield return new WaitForSeconds(openAnimClip.length);
            //npcMovementController.SetMovement(true);
        }

        public void ManageRobot_OtherStages()
        {
            //npcMovementController.SetNpcAtTarget();
            SetOpenAnim(true);
        }

        public void SetWalkAnim(bool status)
        {
            anim.SetBool("Walk_Anim", status);
        }

        public void SetRollAnim(bool status)
        {
            anim.SetBool("Roll_Anim", status);
        }

        public void SetOpenAnim(bool status)
        {
            anim.SetBool("Open_Anim", status);
        }
    }
}
