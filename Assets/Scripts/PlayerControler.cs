using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    public float mouseSensitivity = 2.5f;
    float speed = 5f;

    Vector3 velocity = Vector3.zero;
    Vector3 _rotation = Vector3.zero;
    Vector3 camRotation = Vector3.zero;
    Rigidbody rg;

    private void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        #region movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _yMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * _xMov;
        Vector3 moveVertical = transform.forward * _yMov;

        velocity = (moveHorizontal + moveVertical).normalized * speed;

        float _yRot = Input.GetAxisRaw("Mouse X");
        _rotation = new Vector3(0f, _yRot, 0f) * mouseSensitivity * Time.deltaTime;

        float _xRot = Input.GetAxisRaw("Mouse Y");
        camRotation = new Vector3(_xRot, 0f, 0f) * mouseSensitivity * Time.deltaTime;
        #endregion

        transform.rotation = new Quaternion(0,cam.transform.rotation.y,0,cam.transform.rotation.w);
    }

    void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            Move();
        }
    }
    public void SetVariables(float ms, float s)
    {
        speed = s;
        mouseSensitivity = ms;
    }
    void Move()
    {
        rg.MovePosition(rg.position + velocity * Time.deltaTime);
    }
}
