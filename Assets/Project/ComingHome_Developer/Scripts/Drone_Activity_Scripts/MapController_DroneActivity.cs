using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController_DroneActivity : MonoBehaviour
{
    public static MapController_DroneActivity instance;

    [SerializeField] Transform player;
    [SerializeField] Transform mapCamera;
    [SerializeField] float mapRadius, cameraHeight;

    [Header("Map Pointer Parents")]
    [SerializeField] Transform playerMapPointerParent;
    public Transform[] locationsMapPointerParent;

    [Header("Map Pointer Prefabs")]
    [SerializeField] GameObject playerPointerPrefab;
    public GameObject locationPointerPrefab;

    [Header("Map Pointer Transform Values")]
    [SerializeField] Vector3 mapPointerLocalPosition;
    [SerializeField] Vector3 mapPointerLocalEulerAngle, mapPointerLocalScale;

    Transform playerMapPointer;
    List<Transform> locationMapPointer;
    List<Vector3> locationInitialPosition;
    Coroutine updateLocationCoroutine;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mapCamera.localPosition = new Vector3(0, cameraHeight, 0);

        //Generate playe pointer
        playerMapPointer = Instantiate(playerPointerPrefab, playerMapPointerParent).transform;
        SetInitialTransformValues(playerMapPointer);

        //Generate locations pointer
        locationMapPointer = new List<Transform>();
        locationInitialPosition = new List<Vector3>();
        for (int i = 0; i < locationsMapPointerParent.Length; i++)
        {
            Transform trans = Instantiate(locationPointerPrefab, locationsMapPointerParent[i]).transform;
            SetInitialTransformValues(trans);
            locationMapPointer.Add(trans);
            locationInitialPosition.Add(trans.position);
        }

        updateLocationCoroutine = StartCoroutine(UpdateLocationsOnMap());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mapCamera.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraHeight, player.transform.position.z);
        mapCamera.localEulerAngles= new Vector3(mapCamera.localEulerAngles.x, player.transform.localEulerAngles.y, mapCamera.localEulerAngles.z);
    }

    IEnumerator UpdateLocationsOnMap()
    {
        float distance = 0;
        for (int i = 0; i < locationMapPointer.Count; i++)
        {
            distance = Vector3.Distance(locationInitialPosition[i], mapCamera.position);

            if (distance > mapRadius)
            {
                Vector3 temPos = (locationInitialPosition[i] - mapCamera.position) * (mapRadius / distance);
                locationMapPointer[i].position = temPos + mapCamera.position;
            }
        }
        yield return new WaitForEndOfFrame();
        updateLocationCoroutine = StartCoroutine(UpdateLocationsOnMap());
    }

    public void ResetLocationOnMap()
    {
        if(updateLocationCoroutine!= null)
        {
            StopCoroutine(updateLocationCoroutine);
        }

        for (int i = 0; i < locationMapPointer.Count; i++)
        {
            locationMapPointer[i].position = locationInitialPosition[i];
        }
    }

    public void StartUpdateLocation()
    {
        if (updateLocationCoroutine != null)
        {
            StopCoroutine(updateLocationCoroutine);
        }
        updateLocationCoroutine = StartCoroutine(UpdateLocationsOnMap());
    }

    void SetInitialTransformValues(Transform targetTransform)
    {
        targetTransform.localPosition = mapPointerLocalPosition;
        targetTransform.localEulerAngles = mapPointerLocalEulerAngle;
        targetTransform.localScale = mapPointerLocalScale;
    }
}
