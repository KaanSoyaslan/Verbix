using System.Collections;
using UnityEngine;

public class SizeChangerByRatio : MonoBehaviour
{
    private Coroutine resizeCoroutine;

    public static float waitTime = 1f;
    public static bool isActiveCheck = true;

    [SerializeField] private float sizeTablets;
    [SerializeField] private float sizePhones;
    [SerializeField] private float sizeExtendedPhones;

    void OnEnable()
    {
        resizeCoroutine = StartCoroutine(CheckAndResize());
    }

    void OnDisable()
    {
        if (resizeCoroutine != null)
        {
            StopCoroutine(resizeCoroutine);
            resizeCoroutine = null;
        }
    }

    private IEnumerator CheckAndResize()
    {
        AdjustObjectSize();
        if (!isActiveCheck)
        {
            yield break;
        }

        yield return new WaitForSeconds(waitTime);
        if (gameObject.activeSelf)
        {
            resizeCoroutine = StartCoroutine(CheckAndResize());
        }
    }

    private void AdjustObjectSize()
    {
        float screenRatio = (float)Screen.height / (float)Screen.width;

        if (screenRatio < 1.7f) //Tablets, Ipads (4:3)...
        {
            transform.localScale = Vector3.one * sizeTablets;
        }
        else if (screenRatio >= 1.7f && screenRatio < 1.8f) //16:9 Devices
        {
            transform.localScale = Vector3.one * sizePhones;
        }
        else //extended screen devices  (17:9)...
        {
            transform.localScale = Vector3.one * sizeExtendedPhones;
        }
    }
}