using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    public static Player player;

    [Header("Compenets\n------------------------------------------------------")]
    [SerializeField] private PlayerInput inputSystem;
    [SerializeField] private Transform bulletSpawn;

    
    [Header("Player Controls\n------------------------------------------------------")]
    [SerializeField] private float speed;
    [SerializeField] private float lookAngle;
    [SerializeField] private float mouseSens;
    [SerializeField] private float smoothing;

    [Header("Player Stats\n------------------------------------------------------")]
    [SerializeField] private int life;
    [SerializeField] private int shield;
    [SerializeField] private int armor;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int damage;
    [SerializeField] private float fireRate;

    private float movementX;
    private float movementY;

    private Vector2 mouseMove;

    private Vector2 mouseLook;


    private void Awake() 
    {
        if (player != null && player != this)
        {
            Destroy(gameObject);
        }
        else
        {
            player = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() 
    {
        
    }

    private void LateUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        transform.Translate(movement * speed * Time.deltaTime);
        
        inputSystem.camera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right); 
        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x * 0.75f;
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

    public void OnFire()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

    public void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            shield -= damage;

            if (shield < 0)
            {
                shield = 0;
            }
        }
        else
        {
            life -= damage;

            if (life <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {

    }
}
