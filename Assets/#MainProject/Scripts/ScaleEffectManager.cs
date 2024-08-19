using UnityEngine;
using System.Collections;

public class ScaleEffectManager : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    private float transitionDuration = 1.5f; // Duration for the scaling effect

    void Start()
    {
        // Register for scale change events
        NotificationsManager.Instance.AddListener("IncreaseScale", OnIncreaseScale);
        NotificationsManager.Instance.AddListener("DecreaseScale", OnDecreaseScale);
    }

    void OnDestroy()
    {
        // Unregister from events to avoid memory leaks
        NotificationsManager.Instance.RemoveListener("IncreaseScale", OnIncreaseScale);
        NotificationsManager.Instance.RemoveListener("DecreaseScale", OnDecreaseScale);
    }

    private void OnIncreaseScale(object[] parameters)
    {
        if (parameters.Length > 0 && parameters[0] is GameObject targetObject)
        {
            Debug.Log("Increasing scale of: " + targetObject.name);
            ApplyScaleEffect(targetObject, true);
        }
    }

    private void OnDecreaseScale(object[] parameters)
    {
        if (parameters.Length > 0 && parameters[0] is GameObject targetObject)
        {
            Debug.Log("Decreasing scale of: " + targetObject.name);
            ApplyScaleEffect(targetObject, false);
        }
    }

    public void ApplyScaleEffect(GameObject targetObject, bool increaseSize)
    {
        // Store the original scale of the object
        originalScale = targetObject.transform.localScale;

        // Calculate the new scale
        targetScale = increaseSize ? originalScale * 1.5f : originalScale * 0.5f;

        // Start coroutine to smoothly scale the object
        StartCoroutine(SmoothScale(targetObject, targetScale, transitionDuration));
    }

    private IEnumerator SmoothScale(GameObject targetObject, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = targetObject.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            targetObject.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.localScale = targetScale;
    }
}
