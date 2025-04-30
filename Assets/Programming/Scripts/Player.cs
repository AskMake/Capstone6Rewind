using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    PlayerInput playerInput;
    InventoryManager inventoryManager;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [SerializeField]
    float distance;
    [HideInInspector]
    public bool canMove = true;
    [SerializeField]
    CinemachineInputAxisController axisController;

    InputAction moveAction,jumpAction,interactAction,inventoryAction,uiInventoryAction;
    [SerializeField]
    LayerMask layerMask;
    public bool inDialogue;
    [SerializeField]
    EventSystem eventSystem;

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        uiInventoryAction = InputSystem.actions.FindAction("InventoryCancel");
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveAction.ReadValue<Vector2>().y : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveAction.ReadValue<Vector2>().x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (jumpAction.IsPressed() && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if((inventoryAction.IsPressed() || uiInventoryAction.IsPressed()) && !inDialogue)
        {
            Inventory();
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            ObjectCheck();
        }
        if( eventSystem.currentSelectedGameObject.TryGetComponent<InventoryInfo>(out var inventoryInfo))
        {
            Debug.Log(inventoryInfo.name);
        }
        

    }
    void ObjectCheck()
    {
        
    // in general I'd use vars .. no need to have class fields for those
    var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
   
    if (Physics.Raycast(ray, out var hit, distance))
    {   
        if(hit.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            // hitting an IInteractable -> store
            SetInteractable(interactable);
        }
        else
        {
            // hitting something that is not IInteractable -> reset
            SetInteractable(null);
        }
    }
    else
    {
        // hitting nothing at all -> reset
        SetInteractable(null);
    }

    // if currently focusing an IInteractable and click -> interact
    if(currentInteractable != null && interactAction.IsPressed())
    {
        currentInteractable.Interact();
    }
    }
    private IInteractable currentInteractable;

    private void SetInteractable(IInteractable interactable)
    {
        // if is same instance (or both null) -> ignore
        if(currentInteractable == interactable) return;

        // otherwise if current focused exists -> reset
        if(currentInteractable != null) currentInteractable.IsLooking = false;

        // store new focused
        currentInteractable = interactable;

        // if not null -> set looking
        if(currentInteractable != null) currentInteractable.IsLooking = true;
    }
    public void ChangeButtonMap()
    {
        if(playerInput.currentActionMap.name == playerInput.defaultActionMap)
        {
            playerInput.SwitchCurrentActionMap("UI");
            canMove = false;
            axisController.Controllers[1].Enabled = false;
        }
        else {
            playerInput.SwitchCurrentActionMap("Player");
            canMove = true;
            axisController.Controllers[1].Enabled = true;
        }
    }
    void Inventory()
    {
        inventoryManager.Inventory();
        ChangeButtonMap();
    }
}