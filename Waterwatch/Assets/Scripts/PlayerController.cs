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

   CharacterController characterController;

   void Start()
   {
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
       cameraRotation = 0.0f;
   }

   void Update()
   {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
       
       //Verificando se é preciso aplicar a gravidade
       float y = 0;
       
       //Tratando movimentação do mouse
       float mouse_dX = Input.GetAxis("Mouse X");
       float mouse_dY = Input.GetAxis("Mouse Y");
       //Tratando a rotação da câmera
       cameraRotation -= mouse_dY;
       Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

       Debug.Log(canJump);

       if(characterController.isGrounded){
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

   void LateUpdate()
   {
        Vector3 raycastPos = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.6f, playerCamera.transform.position.z);
        RaycastHit hit;
        Debug.DrawRay(raycastPos, transform.forward*10.0f, Color.magenta);
        if(Physics.Raycast(raycastPos, transform.forward, out hit, 100.0f))
        {
            Debug.Log(hit.collider.name);
        }
    }
}