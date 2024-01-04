using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTimeout : MonoBehaviour
{
    [SerializeField] private PlayerCameras _playerCams;

    private float _idleTime = 0.0f; // time since last input
    private bool _isIdle = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        if (_isIdle == false && _idleTime > 10.0f)
        {
            _isIdle = true;
            _playerCams.Idle();
        }
    }

    private void CheckInput()
    {
        if (Input.anyKey || Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
        {
            if (_isIdle)
            {
                _playerCams.WakeUp();
            }

            _isIdle = false;
            _idleTime = 0.0f;
        }
        else
        {
            _idleTime += Time.deltaTime;
        }
    }
}
