using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameras : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcamShip;
    [SerializeField] private CinemachineVirtualCamera _vcamCockpit;
    [SerializeField] private GameObject _cockpit;

    MeshRenderer _shipRender;

    // Start is called before the first frame update
    void Start()
    {
        _shipRender = GetComponent<MeshRenderer>();

        if (_shipRender == null)
        {
            Debug.LogError("Failed to cache ship's renderer!");
        }

        _vcamShip.Priority = 100;
        _vcamCockpit.Priority = 10;
        _cockpit.SetActive(false);
        _shipRender.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if (_cockpit.activeInHierarchy == true)
        {
            _vcamCockpit.Priority = 10;
            _cockpit.SetActive(false);
            _vcamShip.Priority = 100;
            _shipRender.enabled = true;
        }
        else
        {
            _vcamShip.Priority = 10;
            _cockpit.SetActive(true);
            _vcamCockpit.Priority = 100;
            _shipRender.enabled = false;
        }
    }
}
