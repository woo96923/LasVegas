using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class playerColtroler : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity;//마우스 감도

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    [SerializeField]
    private Camera TheCamera;

    [SerializeField]
    private float jumpPower;

    bool isJumping;
    
    private Rigidbody myRigid;//물리적인걸 구현


    // Start is called before the first frame update
    void Start()
    {
        
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
        Jump();

        

    }


    void Jump()
    {

        if (Input.GetButtonDown("Jump"))
            myRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;//방향 * 속도

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);


    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");//위아래 시점
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);//카메라 값 제한해주는 함수

        TheCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
