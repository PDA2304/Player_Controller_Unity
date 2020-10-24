using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; //Скорость передвижения 
    public float jumpForce; //Высота прыжков 
    public float gravityScale; //Сила гравитации 
    public Animator anim; // 
    public CharacterController controller; //Объект контроллера персонажа 
    public GameObject playerModel;// 
    public float rotateSpeed;// Скорость поворота камеры
    private Vector3 _moveDirection;// Переменная отвечающая за движение
    private Camera theCam; // Камера

    public static PlayerController instance; //переменная для переопределения класса
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        theCam = Camera.main;
    }

    void Update()
    {
        float yStore = _moveDirection.y;
        //_moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, _moveDirection.y, Input.GetAxisRaw("Vertical") * moveSpeed); 
        _moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        _moveDirection.Normalize();// Нормализация движения
        _moveDirection = _moveDirection * moveSpeed;
        _moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            _moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
                _moveDirection.y = jumpForce;
        }//Защита от Бесконечных прыжков

        _moveDirection.y = _moveDirection.y + (Physics.gravity.y * gravityScale);//Гравитация
        controller.Move(_moveDirection * Time.deltaTime);//скорость обновления

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(_moveDirection.x, 0f, _moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }//Корректное перемещение персонажа 

        anim.SetFloat("Speed", Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z));// для работы переменной которая отвечает за анимацию бега
        anim.SetBool("Grounded", controller.isGrounded);// для работы переменной которая отвечает за анимацию прыжка
    }
}