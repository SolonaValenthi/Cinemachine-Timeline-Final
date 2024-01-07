using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControls : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotSpeed;

    private float _vertical;
    private float _horizontal;
    private float _roll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShipMovement();
    }

    private void ShipMovement()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        _roll = Input.GetAxis("Roll");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_moveSpeed < 15.0f)
            {
                _moveSpeed += 0.2f;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (_moveSpeed > 0.3f)
            {
                _moveSpeed -= 0.2f;
            }
        }

        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left * _rotSpeed * _vertical * Time.deltaTime); // pitch control
        transform.Rotate(Vector3.up * _rotSpeed * _horizontal * Time.deltaTime); // yaw control
        transform.Rotate(Vector3.forward * (_rotSpeed * 1.5f) * _roll * Time.deltaTime); // roll control
    }
}
