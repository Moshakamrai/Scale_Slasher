using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class GameController : MonoBehaviour
{
    public PlayerInput playerInput;

    InputAction shrink;
    InputAction enlarge;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        shrink = playerInput.actions.FindAction("MakeSmall");
        enlarge = playerInput.actions.FindAction("MakeBig");
    }

    void OnEnable()
    {
        // Register callbacks for the input actions
        shrink.performed += OnShrink;
        enlarge.performed += OnEnlarge;
    }

    void OnDisable()
    {
        // Unregister callbacks to avoid memory leaks
        shrink.performed -= OnShrink;
        enlarge.performed -= OnEnlarge;
    }

    // Method called when the "MakeSmall" action is triggered
    private void OnShrink(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Shrink: " + hitObject.name);
            NotificationsManager.Instance.PostNotification("DecreaseScale", hitObject);
        }
    }

    // Method called when the "MakeBig" action is triggered
    private void OnEnlarge(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (!hitObject.CompareTag("Unscale"))
            {
                Debug.Log("Enlarge: " + hitObject.name);
                NotificationsManager.Instance.PostNotification("IncreaseScale", hitObject);
            }
        }
    }
}
