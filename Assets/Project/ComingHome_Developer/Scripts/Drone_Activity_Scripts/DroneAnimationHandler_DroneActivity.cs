using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneAnimationHandler_DroneActivity : MonoBehaviour
{
    public static DroneAnimationHandler_DroneActivity instance;

    public GameObject droneTrigger;
    public GameObject auraEffect;

    public Button flyBtn;
    public Button pickBoxBtn;
    public Button droneDeliveryBtn , landingBtn;
    public GameObject deliveryBox;
    public GameObject boxParent;
    public GameObject mysteryBox;
    public GameObject mysteryBox_Parent;

    public List<Transform> targetPos;

    internal int targetindex;

    [SerializeField]
    float speed = 2;
    private void Awake()
    {
        instance = this;
    }

  
    public void DroneAnimationTrigger(Collider other)
    {
        if (other.gameObject.transform.name.Equals(droneTrigger.name))
        {
            ThirdPersonController_V2.instance.isControllingEnabled = false;
            ThirdPersonController_V2.instance.GetComponent<Animator>().SetFloat("Blend", 0);
            TableTrigger_DroneActivity.instance.bagPanelBlocker.SetActive(false);
            ThirdPersonController_V2.instance.transform.localPosition = Vector3.zero;
            ThirdPersonController_V2.instance.transform.localEulerAngles = Vector3.zero;
            auraEffect.SetActive(false);
            VonLab_DroneActivity.instance.isCollectedDroneAfterDesc = false;
        }
    }

    IEnumerator MoveCoroutine(Vector3 TargetPos)
    {
        Transform drone = AttachObjects_DroneActivity.instance.orginaldrone.transform;
        Vector3 iniPos = drone.position;

        float timeValue = 0;

        while(timeValue < 1)
        {
            timeValue += speed * Time.deltaTime;
            drone.position = Vector3.Lerp(iniPos, TargetPos, timeValue);
            yield return new WaitForSeconds(.01f);
        }
        //yield return null;
        AfterMovingTarget();
    }


    public void AfterMovingTarget()
    {
        switch (targetindex)
        {
            case 0:
                Debug.Log("Target Index" + targetindex + "Position"+targetPos[targetindex].transform.position);
                targetindex++;
                StartCoroutine(MoveCoroutine(targetPos[targetindex].transform.position));
                break;
            case 1:
                targetindex++;
                StartCoroutine(MoveCoroutine(targetPos[targetindex].transform.position));
                break;
            case 2:// pick btn enable
                pickBoxBtn.gameObject.SetActive(true);
                flyBtn.gameObject.SetActive(false);
                deliveryBox.SetActive(true);
                MultiDescription_DroneActivity.instance.Set10Desc_Von();
                break;
            case 3: //After click pick btn
                StartCoroutine(DronePickup());
                break;
            case 4:// After click deliver
                //onenable locker // disable box
                MultiDescription_DroneActivity.instance.Set11Desc_Von();
                targetindex++;
                StartCoroutine(MoveCoroutine(targetPos[targetindex].transform.position));
                AttachObjects_DroneActivity.instance.orginaldrone.GetComponent<Animator>().SetTrigger("Idle");
                mysteryBox.SetActive(true);
                deliveryBox.SetActive(false);
                break;
            //case 5: // after click landing
            //    landingBtn.gameObject.SetActive(true);
            //    StartCoroutine(DroneLanding());
                //break;
            default:
                break;
        }
        
    }

    #region Buttons Activity
    public void ClickFlyAnimationBtn()
    {
        Debug.Log("FlyAnimtion");
        flyBtn.gameObject.SetActive(false);
        AttachObjects_DroneActivity.instance.orginaldrone.GetComponent<Animator>().enabled = true;
        AttachObjects_DroneActivity.instance.orginaldrone.GetComponent<Animator>().SetTrigger("Fly");
        StartCoroutine(MoveCoroutine(targetPos[targetindex].transform.position));
    }

    public void ClickPickupBtn()
    {
        pickBoxBtn.gameObject.SetActive(false);
        targetindex++;
        StartCoroutine(MoveCoroutine(targetPos[targetindex].transform.position));
    }

    public void ClickDeliveryBtn()
    {
        droneDeliveryBtn.gameObject.SetActive(false);
        targetindex++;
        StartCoroutine(MoveCoroutine(targetPos[targetindex].transform.position));
    }

    public void ClickLandingBtn()
    {
        landingBtn.gameObject.SetActive(false);
        targetindex++;
        StartCoroutine(MoveCoroutine(targetPos[targetindex].transform.position));
    }

    IEnumerator DronePickup()
    {
        AttachObjects_DroneActivity.instance.orginaldrone.GetComponent<Animator>().SetTrigger("PickObject");
        yield return new WaitForSeconds(1.3f);
        deliveryBox.transform.SetParent(boxParent.transform);
        deliveryBox.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(3f);
        droneDeliveryBtn.gameObject.SetActive(true);
    }

    //IEnumerator DroneLanding()
    //{
    //    yield return new WaitForSeconds(11f);
    //    mysteryBox.transform.SetParent(mysteryBox_Parent.transform);
    //}
    #endregion
}
