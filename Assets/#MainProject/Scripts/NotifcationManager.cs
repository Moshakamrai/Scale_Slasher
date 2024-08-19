using UnityEngine;
using System.Collections.Generic;

public class NotificationsManager : MonoBehaviour
{
    // Singleton instance
    private static NotificationsManager instance;

    // Dictionary to store listeners for each event type
    private Dictionary<string, List<System.Action<object[]>>> listeners = new Dictionary<string, List<System.Action<object[]>>>();

    public static NotificationsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NotificationsManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("NotificationsManager");
                    instance = obj.AddComponent<NotificationsManager>();
                }
            }
            return instance;
        }
    }

    // Method to add a listener for a specific notification
    public void AddListener(string notificationName, System.Action<object[]> listener)
    {
        if (!listeners.ContainsKey(notificationName))
        {
            listeners.Add(notificationName, new List<System.Action<object[]>>());
        }
        listeners[notificationName].Add(listener);
    }

    // Method to remove a listener for a specific notification
    public void RemoveListener(string notificationName, System.Action<object[]> listener)
    {
        if (listeners.ContainsKey(notificationName))
        {
            listeners[notificationName].Remove(listener);
            if (listeners[notificationName].Count == 0)
            {
                listeners.Remove(notificationName);
            }
        }
    }

    // Method to post a notification to all relevant listeners
    public void PostNotification(string notificationName, params object[] parameters)
    {
        if (listeners.ContainsKey(notificationName))
        {
            foreach (var listener in listeners[notificationName])
            {
                listener.Invoke(parameters);
            }
        }
    }

    // Method to clear all listeners
    public void ClearListeners()
    {
        listeners.Clear();
    }

    // Method to remove null or destroyed listeners
    public void RemoveRedundancies()
    {
        foreach (var key in listeners.Keys)
        {
            listeners[key].RemoveAll(listener => listener == null);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        RemoveRedundancies();
    }
}
