using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace SEC_1
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPC_MovementController_Introduction : MonoBehaviour
    {
        public static NPC_MovementController_Introduction instance;

        public GameObject targetObject, target2Object, target3Object, target4Object, target5Object , target6Object, target7Object, target8Object, target9Object,vonHelpCollider;
        public bool npcMovement;
        public bool isIntroduction;

        internal NavMeshAgent _agent;
        internal RobotSphere_Controller robotControllerNPC;
        internal GameObject target;
        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            robotControllerNPC = GetComponent<RobotSphere_Controller>();
        }

        public void InitIntro()
        {
            target = targetObject;
            RobotSphere_Controller.instance.ManageRobot_InitialAndGameStage();
            StartCoroutine(setMovement());
        }

        IEnumerator setMovement()
        {
            yield return new WaitForSeconds(6f);
            npcMovement = true;
            robotControllerNPC.SetWalkAnim(true);
        }

        public void SetMovement(bool status)
        {
            npcMovement = status;
            robotControllerNPC.SetWalkAnim(status);
            if (npcMovement)
            {
                AudioHandler.instance.PlayAudio_NPC();
            }
        }

        public void SetTargetObject(GameObject obj)
        {
            MoveDirectToTarget();
            target = obj;
            _agent.stoppingDistance = 0;
        }

        public void MoveDirectToTarget()
        {
            transform.position = target.transform.position;
            robotControllerNPC.SetWalkAnim(true);
        }

        public void SetNpcAtTarget()
        {
            //transform.position = targetObject.transform.position;
            _agent.Warp(target.transform.position);
            robotControllerNPC.SetWalkAnim(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_agent != null && npcMovement)
            {
                _agent.SetDestination(target.transform.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Equals(targetObject.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
                //GameSceneManager.instance.SetPlayerMovement(true);
                vonHelpCollider.SetActive(true);
                MultiDesc_Intro.instance.descriptionPanel.SetActive(true);
                if (target.name.Equals(targetObject.name))
                {
                    Debug.Log("robot hit the collider-----" + targetObject.name + "hit_2 collider--- " + target.name + " Hit_3 Collider---" + target2Object.name);
                    MultiDesc_Intro.instance.SetDesc_VonHi();
                    //StartCoroutine(Description());
                }
            }
            else if (other.gameObject.name.Equals(target2Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
            }
            else if (other.gameObject.name.Equals(target3Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
            }

            else if(other.gameObject.name.Equals(target4Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
                Debug.Log("Target Object------------------");
            }

            else if(other.gameObject.name.Equals(target5Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
            }

            else if (other.gameObject.name.Equals(target6Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
            }

            else if (other.gameObject.name.Equals(target7Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
            }

            else if (other.gameObject.name.Equals(target8Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
                AttachObjects_DroneActivity.instance.VonMovingTargetPosition();
            }

            else if (other.gameObject.name.Equals(target9Object.name))
            {
                target.SetActive(false);
                npcMovement = false;
                robotControllerNPC.SetWalkAnim(false);
                AudioHandler.instance.StopAudio_NPC();
                //SEC_1.NPC_MovementController_Introduction.instance._agent.transform.localPosition = new Vector3(7.06f, 0.02f, 3.81f);
                //SEC_1.NPC_MovementController_Introduction.instance._agent.transform.eulerAngles = new Vector3(0, -142, 0);
            }
        }

    }
}
