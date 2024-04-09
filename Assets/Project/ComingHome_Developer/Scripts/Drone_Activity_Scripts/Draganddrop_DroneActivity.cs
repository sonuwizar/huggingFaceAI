using UnityEngine;

public class Draganddrop_DroneActivity : MonoBehaviour
    {
        // Start is called before the first frame update
        public static Draganddrop_DroneActivity instance;

        public float dragHeight;
        public Camera playerCamera;

        internal Collider currCollider;

        private Vector3 mOffset;
        private float mZCoordinate;
        GameObject currObject, currHitObj;
        Vector3 iniPos, iniAngle;
        bool isModelPicked;

        private void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
#if UNITY_EDITOR
                    Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
#else
                    Ray ray = playerCamera.ScreenPointToRay(Input.GetTouch(0).position);
#endif
                    if (Physics.Raycast(ray, out hit)) //on model down
                    {
                        isModelPicked = true;
                        currObject = PickItemsFromBag_DroneActivity.instance.OnModelDown(hit.transform);

                        if (currObject != null && isModelPicked)
                        {
                            mZCoordinate = playerCamera.WorldToScreenPoint(currObject.transform.position).z;
                            mOffset = currObject.transform.position - GetMouseAsWorldPoint();
                        }
                    }
                }
                else // when dragging
                {
                    OnModelDrag();
                    //SaveTheQueen.instance.ResetModel();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnModelUp();
            }

             Vector3 GetMouseAsWorldPoint()
            {
#if UNITY_EDITOR
                Vector3 mousePoint = Input.mousePosition;
                mousePoint.z = mZCoordinate;
                return playerCamera.ScreenToWorldPoint(mousePoint);
#else
        Vector3 mousePoint = Input.GetTouch(0).position;
        mousePoint.z = mZCoordinate;
        return playerCamera.ScreenToWorldPoint(mousePoint);
#endif
            }

            void OnModelDrag()
            {
                Debug.Log("Start Dragging");
          
                if (currObject != null && isModelPicked)
                {
                    Debug.Log("First drag");
                    currObject.transform.position = GetMouseAsWorldPoint() + mOffset;

                    Vector3 pos = currObject.transform.position;
                    if (pos.y < dragHeight)
                    {
                        currObject.transform.position = new Vector3(pos.x, dragHeight, pos.z);
                    }
                mZCoordinate = playerCamera.WorldToScreenPoint(currObject.transform.position).z;
                //mZCoordinate = Mathf.Abs(playerCamera.transform.position.z - currObject.transform.position.z);
                Debug.Log("End Dragging");
                }
            }

            void OnModelUp()
            {

                if (!isModelPicked || currObject == null)
                {
                    return;
                }
                isModelPicked = false;
                RaycastHit hit;
#if UNITY_EDITOR
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
#else
            Ray ray = playerCamera.ScreenPointToRay(Input.GetTouch(0).position);
#endif
                if(Physics.Raycast(ray, out hit,50))
                {
                if (hit.transform != null && !PickItemsFromBag_DroneActivity.instance.OnModelUp(hit.transform))
                {
                    Debug.Log("Hit Transform Up"+hit.transform.name);
                    PickItemsFromBag_DroneActivity.instance.ResetModel();
                    currObject = null;
                }
            }
                else
                {
                PickItemsFromBag_DroneActivity.instance.ResetModel();
                currObject = null;
                }
            }
        }
    }
