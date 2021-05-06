using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _baseSpeed = 10.0f;
    float _gravidade = 9.8f;
    private Vector3 playerVelocity;
    private float jumpHeight = 2.0f;
    private bool canJump = false;

    //Referência usada para a câmera filha do jogador
    GameObject playerCamera;
    //Utilizada para poder travar a rotação no angulo que quisermos.
    float cameraRotation;
    string object_hit;
    public Inventory inventory;
    public GameObject hand;
    CharacterController characterController;
    public HUD hud;
    public GameObject ScrollPanel;

    public float stamina = 100.0f;

    AudioSource walkingSound;
    public AudioSource tiredSFX;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
        inventory.ItemUsed += Inventory_ItemUsed;
        walkingSound = GetComponent<AudioSource>();
    }

    private IInventoryItem mCurrentItem = null;
    public GameObject goItem;
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (mCurrentItem != null)
        {
            goItem.SetActive(false);
        }

        IInventoryItem item = e.Item;

        if (item != null)
        {
            goItem = (item as MonoBehaviour).gameObject;
            goItem.SetActive(true);
            Rigidbody goItemRB = goItem.GetComponent<Rigidbody>();
            goItemRB.isKinematic = true;

            goItem.transform.parent = hand.transform;
            goItem.transform.localPosition = (item as InventoryItemBase).PickPosition;
            goItem.transform.localEulerAngles = (item as InventoryItemBase).PickRotation;

            mCurrentItem = e.Item;
        }

    }
    private bool mLockPickup = false;

    private void DropCurrentItem()
    {
        if (mCurrentItem != null)
        {
            mLockPickup = true;
            goItem = (mCurrentItem as MonoBehaviour).gameObject;

            inventory.RemoveItem(mCurrentItem);

            Rigidbody rbItem = goItem.GetComponent<Rigidbody>();
            rbItem.transform.parent = null;
            rbItem.isKinematic = false;
            rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);

            Invoke("DoDropItem", 0.25f);
        }
    }

    public void DoDropItem()
    {
        mLockPickup = false;
        mCurrentItem = null;
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DropCurrentItem();
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ScrollPanel.activeSelf)
            {
                hud.CloseScrollPanel();
            }

            if (mItemToPickup != null)
            {
                inventory.AddItem(mItemToPickup);
                mItemToPickup.OnPickup();
                hud.CloseMessagePanel("");
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if ((x != 0 || z != 0) && Input.GetKey(KeyCode.LeftShift) && stamina > .5f)
        {
            _baseSpeed = 10.0f * 1.75f;
            stamina -= Time.deltaTime * 17.5f;
            Debug.Log(stamina);
            if (stamina < 0.0f)
                stamina -= 25.0f;
            else if (stamina < 30.0f)
            {
                tiredSFX.mute = false;
                // https://www.fesliyanstudios.com/royalty-free-sound-effects-download/person-sighing-160
                if (!tiredSFX.isPlaying)
                    tiredSFX.Play();
            }
        }
        else
        {
            _baseSpeed = 10.0f;
            if (stamina < 100)
                stamina += Time.deltaTime * 5;
        }

        if (x != 0 || z != 0 && characterController.isGrounded)
        {
            if (!walkingSound.isPlaying)
                walkingSound.Play();
        }
        else
        {
            walkingSound.Stop();
        }

        //Verificando se é preciso aplicar a gravidade
        float y = 0;

        //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        //Tratando a rotação da câmera
        if (cameraRotation >= -20 && cameraRotation <= 45)
        {
            cameraRotation -= mouse_dY;
            Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
        }
        if (cameraRotation < -20)
        {
            cameraRotation = -20;
        }

        if (cameraRotation > 45)
        {
            cameraRotation = 45;
        }

        if (characterController.isGrounded)
        {
            canJump = true;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * -_gravidade);
            canJump = false;
        }

        playerVelocity.y += -_gravidade * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

        characterController.Move(direction * _baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, mouse_dX);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
    }

    IInventoryItem mItemToPickup = null;
    private void OnTriggerEnter(Collider other)
    {
        if (mLockPickup)
        {
            return;
        }

        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            mItemToPickup = item;
            hud.OpenMessagePanel("");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            hud.CloseMessagePanel("");
            mItemToPickup = null;

        }
    }
}