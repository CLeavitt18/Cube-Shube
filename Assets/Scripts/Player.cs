using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput inputSystem;
    [SerializeField] private float speed;
    [SerializeField] private float lookAngle;
    [SerializeField] private float mouseSens;
    [SerializeField] private float smoothing;

    private float movementX;
    private float movementY;

    private Vector2 mouseMove;

    private Vector2 mouseLook;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        transform.Translate(movement * speed * Time.deltaTime);
        
        inputSystem.camera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right); 
        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x / 2.0f;
        movementY = movementVector.y;
    }

    public void OnLook(InputValue mouseValue)
    {
        Vector2 mouseVector = mouseValue.Get<Vector2>();

        mouseVector = Vector2.Scale(mouseVector, new Vector2(mouseSens, mouseSens));
        
        mouseMove.x = Mathf.Lerp(mouseMove.x, mouseVector.x, 1f / smoothing);
        mouseMove.y = Mathf.Lerp(mouseMove.y, mouseVector.y, 1f / smoothing);

        mouseLook += mouseMove;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -lookAngle, lookAngle);
    }
}
