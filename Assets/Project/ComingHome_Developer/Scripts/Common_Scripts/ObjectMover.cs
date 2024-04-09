using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public static ObjectMover instance;

    public delegate void AfterAttach();
    public static event AfterAttach afterAttach;
    Coroutine movingRoutine;

    [SerializeField] GameObject magicPS;

    private void Awake()
    {
        instance = this;
    }

    public void MoveModel(Transform currModel, Transform targetModel, float moveTime)
    {
        currModel.transform.SetParent(targetModel);
        StartCoroutine(MoveDelay(currModel, moveTime));
    }

    internal void VanishModel(Transform currModel)
    {
        GameObject instantiatedEffect = Instantiate(magicPS, magicPS.transform.parent);
        instantiatedEffect.transform.parent = currModel.parent;
        instantiatedEffect.transform.localPosition = currModel.localPosition;
        instantiatedEffect.SetActive(true);
        Destroy(instantiatedEffect, 1f);
        afterAttach();
    }

    public void MoveModel(Transform currModel, float moveTime)
    {
        StartCoroutine(MoveDelay(currModel, moveTime));
    }

    IEnumerator MoveDelay(Transform currModel, float moveTime)
    {
        float lerpTime = 0;

        Vector3 InitialPos = currModel.transform.localPosition;
        Vector3 initialRot = currModel.transform.localEulerAngles;
        Vector3 iniScale = currModel.transform.localScale;

        for (float i = 0; i <= moveTime; i += 2*Time.deltaTime)
        {
            lerpTime = Mathf.Lerp(0, 1, i / moveTime);

            currModel.transform.localPosition = Vector3.Lerp(InitialPos, Vector3.zero, lerpTime);
            currModel.transform.localEulerAngles = Vector3.Lerp(initialRot, Vector3.zero, lerpTime);
            currModel.transform.localScale = Vector3.Lerp(iniScale, Vector3.one, lerpTime);

            yield return new WaitForSeconds(0.005f);
        }

        currModel.transform.localPosition = Vector3.zero;
        currModel.transform.localEulerAngles = Vector3.zero;
        currModel.transform.localScale = Vector3.one;

        if (afterAttach != null)
        {
            afterAttach();
        }
    }

    public void MoveModelInCurve(Transform currModel, float moveTime, float curveHeight)
    {
        movingRoutine = StartCoroutine(MoveDelay_Curve(currModel, moveTime, curveHeight));
    }

    IEnumerator MoveDelay_Curve(Transform currModel, float moveTime, float curveHeight)
    {
        float lerpTime = 0;

        Vector3 InitialPos = currModel.transform.localPosition;
        Vector3 initialRot = currModel.transform.localEulerAngles;
        Vector3 iniScale = currModel.transform.localScale;
        Vector3 heightPos = InitialPos/2;
        heightPos = new Vector3(heightPos.x, curveHeight, heightPos.z);

        for (float i = 0; i <= moveTime; i += 2 * Time.deltaTime)
        {
            lerpTime = Mathf.Lerp(0, 1, i / moveTime);

            Vector3 m1 = Vector3.Lerp(InitialPos, heightPos, lerpTime);
            Vector3 m2 = Vector3.Lerp(heightPos, Vector3.zero, lerpTime);

            currModel.transform.localPosition = Vector3.Lerp(m1, m2, lerpTime);
            currModel.transform.localEulerAngles = Vector3.Lerp(initialRot, Vector3.zero, lerpTime);
            currModel.transform.localScale = Vector3.Lerp(iniScale, Vector3.one, lerpTime);

            yield return new WaitForSeconds(0.005f);
        }

        currModel.transform.localPosition = Vector3.zero;
        currModel.transform.localEulerAngles = Vector3.zero;
        currModel.transform.localScale = Vector3.one;

        if (afterAttach != null)
        {
            afterAttach();
        }
    }

    public void StopMovingRoutine()
    {
        if (movingRoutine != null)
        {
            StopCoroutine(movingRoutine);
        }
    }

}
