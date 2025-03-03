using DamageNumbersPro;
using UnityEngine;
using KwlEventBus;

public class KwlWarningTextManager : MonoBehaviour
{
    [SerializeField] private RectTransform spawnOffset;
    [SerializeField] private DamageNumber damageNumberPrefab;

    private void OnEnable()
    {
        KwlBus<SendWarningEvent>.AddListener(DamageNumberRequest);
    }
    private void OnDisable()
    {
        KwlBus<SendWarningEvent>.RemoveListener(DamageNumberRequest);
    }
    private void DamageNumberRequest(SendWarningEvent e)
    {
        var damageNumberInstance = damageNumberPrefab.Spawn(transform.position, e.message, e.color);
        damageNumberInstance.lifetime = e.lifetime;

        damageNumberInstance.SetAnchoredPosition(spawnOffset, Vector2.zero);
        Vector2 sourceOffsetMin = spawnOffset.offsetMin;
        Vector2 sourceOffsetMax = spawnOffset.offsetMax;
        damageNumberInstance.transform.GetComponent<RectTransform>().offsetMin = sourceOffsetMin;
        damageNumberInstance.transform.GetComponent<RectTransform>().offsetMax = sourceOffsetMax;
    }
}
