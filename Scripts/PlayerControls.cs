using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [HideInInspector] public Vector2 mousePosition;

    [SerializeField] public float baseSpeed;
    [SerializeField] public float speed;
    [SerializeField] float jumpForce;

    [SerializeField] public int healt;

    PlayerInputActions playerInputActions;

    CameraBehaviour cameraBehaviourScript;

    AudioSource gameplayAudioSource;

    Weapons currentWeaponsScript;

    GameObject muzzle;
    GameObject itemAtHand;
    GameObject menuUI;

    InventorySystem playerInventory;

    Rigidbody2D playerRB;

    WeaponRayCast weaponRayScript;

    public Vector2 movementDirection;
    

    public bool isGrounded = true;
    static bool doubleDamageTimerActive = false;
    static bool grenadesTimerActive = false;
    public static bool isPaused = true;
    public static bool theFirstStart = false;
    public static bool playerIsDead;

    private void Awake()
    {
        ComponentGetter();
        playerIsDead = false;
    }

    private void Update()
    {
        if (!isPaused)
        {
            PlayerMovement();
            Fire();
            PlayerPositionClamp();
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            movementDirection = Vector2.zero;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && context.performed && !isPaused)
        {
            isGrounded = false;
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Time.timeScale = 0;
            isPaused = true;
            menuUI.SetActive(true);
            playerIsDead = true;
            gameObject.GetComponent<PlayerControls>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void PlayerMovement()
    {
        PlayerFacingDirection();
        playerRB.AddForce(new Vector2(movementDirection.x, 0f) * speed * Time.deltaTime, ForceMode2D.Force);
        playerRB.velocity = new Vector2(0f, playerRB.velocity.y);
        if (isGrounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        }
    }
    
    void ComponentGetter()
    {
        cameraBehaviourScript = Camera.main.gameObject.GetComponent<CameraBehaviour>();
        gameplayAudioSource = GameObject.Find("Gameplay Audio Source").GetComponent<AudioSource>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Fire.Enable();
        playerInventory = GameObject.Find("Game Manager").GetComponent<InventorySystem>();
        playerRB = GetComponent<Rigidbody2D>();
        menuUI = GameObject.Find("Menu UI");
    }

    void PlayerFacingDirection()
    {
        if (movementDirection.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (movementDirection.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void SelectAK()
    {
        if (itemAtHand == null || itemAtHand.name != playerInventory.items[0].name + "(Clone)" && !isPaused)
        {
            if (itemAtHand)
            {
                Destroy(itemAtHand);
            }
            itemAtHand = Instantiate(playerInventory.items[0], gameObject.transform);
            SetWeaponSettings();
        }
    }

    public void SelectShotgun()
    {
        if (itemAtHand == null || itemAtHand.name != playerInventory.items[2].name + "(Clone)" && !isPaused)
        {
            if (itemAtHand)
            {
                Destroy(itemAtHand);
            }
            itemAtHand = Instantiate(playerInventory.items[2], gameObject.transform);
            SetWeaponSettings();
        }
    }

    public void SelectSniper()
    {
        if (itemAtHand == null || itemAtHand.name != playerInventory.items[3].name + "(Clone)" && !isPaused)
        {
            if (itemAtHand)
            {
                Destroy(itemAtHand);
            }
            itemAtHand = Instantiate(playerInventory.items[3], gameObject.transform);
            SetWeaponSettings();
        }
    }

    public void SelectMinigun()
    {
        if (itemAtHand == null || itemAtHand.name != playerInventory.items[1].name + "(Clone)" && !isPaused)
        {
            if (itemAtHand)
            {
                Destroy(itemAtHand);
            }
            itemAtHand = Instantiate(playerInventory.items[1], gameObject.transform);
            SetWeaponSettings();
        }
    }

    public void ActivateDoubleDamage(InputAction.CallbackContext context)
    {
        if (context.performed && InventorySystem.doubleDamageCount > 0 && !isPaused)
        {
            --InventorySystem.doubleDamageCount;
            weaponRayScript = GameObject.Find("Helmet").GetComponentInChildren<WeaponRayCast>();
            weaponRayScript.damage *= 2;
            if (!doubleDamageTimerActive)
            {
                StartCoroutine(DoubleDamageTimer());
            }
        }
    }

    public void ThrowGrenade(InputAction.CallbackContext context)
    {
        if (context.performed && InventorySystem.grenadesCount > 0 && !isPaused)
        {
            --InventorySystem.grenadesCount;

            GameObject spawnedGranade = Instantiate(playerInventory.granade, transform.position + new Vector3(0f, 1.3f, 0f), Quaternion.identity);
            spawnedGranade.GetComponent<Rigidbody2D>().AddForce(new Vector3(transform.localScale.x * 500f, 300f, 0f));

            if (!grenadesTimerActive)
            {
                StartCoroutine(GrenadesTimer());
            }
        }
    }

    public void LeaveMine( InputAction.CallbackContext context)
    {
        if (context.performed && InventorySystem.minesCount > 0 && !isPaused)
        {
            --InventorySystem.minesCount;

            Instantiate(playerInventory.mine, transform.position, Quaternion.identity);
        }
    }

    public void OpenMenu()
    {
        if (theFirstStart && !playerIsDead)
        {
            if (!menuUI.activeSelf)
            {
                menuUI.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;
            }
            else if (menuUI.activeSelf)
            {
                menuUI.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }

    void SetWeaponSettings()
    {
        currentWeaponsScript = itemAtHand.GetComponent<Weapons>();
        muzzle = itemAtHand.transform.GetChild(0).gameObject;
        weaponRayScript = itemAtHand.transform.GetChild(1).GetComponent<WeaponRayCast>();
    }

    void Fire()
    {
        if (itemAtHand != null && currentWeaponsScript.CanShoot && playerInputActions.Player.Fire.ReadValue<float>() > 0.1f && itemAtHand.name == "AK47(Clone)" && playerInventory.akBullets > 0)
        {
            FiringMechanic();
            playerInventory.akBullets--;
        }
        else if(itemAtHand != null && currentWeaponsScript.CanShoot && playerInputActions.Player.Fire.ReadValue<float>() > 0.1f && itemAtHand.name == "Minigun(Clone)" && playerInventory.minigunBullets > 0)
        {
            FiringMechanic();
            playerInventory.minigunBullets--;
        }
        else if (itemAtHand != null && currentWeaponsScript.CanShoot && playerInputActions.Player.Fire.ReadValue<float>() > 0.1f && itemAtHand.name == "Pump Action Shotgun(Clone)" && playerInventory.shotgunBullets > 0)
        {
            FiringMechanic();
            playerInventory.shotgunBullets--;
        }
        else if(itemAtHand != null && currentWeaponsScript.CanShoot && playerInputActions.Player.Fire.ReadValue<float>() > 0.1f && itemAtHand.name == "Sniper(Clone)" && playerInventory.sniperBullets > 0)
        {
            FiringMechanic();
            playerInventory.sniperBullets--;
        }
    }

    void FiringMechanic()
    {
        if(itemAtHand.name == "AK47(Clone)")
        {
            gameplayAudioSource.PlayOneShot(cameraBehaviourScript.AK47ShottingSFX);
        }
        else if(itemAtHand.name == "Minigun(Clone)")
        {
            gameplayAudioSource.PlayOneShot(cameraBehaviourScript.minigunShootingSFX);
        }
        else if (itemAtHand.name == "Sniper(Clone)")
        {
            gameplayAudioSource.PlayOneShot(cameraBehaviourScript.sniperShootingSFX);
        }
        else if (itemAtHand.name == "Pump Action Shotgun(Clone)")
        {
            gameplayAudioSource.PlayOneShot(cameraBehaviourScript.shotgunShootingSFX);
        }
        muzzle.SetActive(true);
        weaponRayScript.ApplyDamage();
        currentWeaponsScript.CanShoot = false;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    void PlayerPositionClamp()
    {
        if(transform.position.x < -40)
        {
            transform.position = new Vector3(-40, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 40)
        {
            transform.position = new Vector3(40, transform.position.y, transform.position.z);
        }
    }

    IEnumerator DoubleDamageTimer()
    {
        Debug.Log("Timer Activated");
        doubleDamageTimerActive = true;
        yield return new WaitForSeconds(10f);
        doubleDamageTimerActive = false;
        weaponRayScript.damage = weaponRayScript.baseDamage;
    }

    IEnumerator GrenadesTimer()
    {
        grenadesTimerActive = true;
        yield return new WaitForSeconds(30f);
        grenadesTimerActive = false;
        speed = baseSpeed;
    }
}


