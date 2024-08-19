using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerClass : MonoBehaviour
{
    void Start()
    {
        // Register this object to listen for the "OnPlayerHealthChange" event
        NotificationsManager.Instance.AddListener("OnPlayerHealthChange", OnPlayerHealthChange);
    }

    void OnDestroy()
    {
        // Unregister this listener to prevent memory leaks
        NotificationsManager.Instance.RemoveListener("OnPlayerHealthChange", OnPlayerHealthChange);
    }

    // Event handler that will be called when the notification is posted
    void OnPlayerHealthChange(object[] parameters)
    {
        // Handle the event
        if (parameters.Length > 0 && parameters[0] is int health)
        {
            Debug.Log("Player health has changed to: " + health);
        }
    }
}
