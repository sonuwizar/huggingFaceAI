using UnityEngine;
using UnityEngine.AI;

namespace SEC_1
{
    public class PortalTrigger : MonoBehaviour
    {
        public Transform targetObject;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<NavMeshAgent>() != null)
            {
                other.transform.GetComponent<NavMeshAgent>().Stop();
            }
            other.gameObject.SetActive(false);
            other.transform.position = targetObject.position;
            other.gameObject.SetActive(true);
        }
    }
}
