using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameras : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcamShip;
    [SerializeField] private CinemachineVirtualCamera _vcamCockpit;
    [SerializeField] private GameObject _cockpit;
    [SerializeField] private GameObject[] _idleCams;

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
            _vcamShip.Priority = 100;
            _vcamCockpit.Priority = 10;
            _cockpit.SetActive(false);
            _shipRender.enabled = true;
        }
        else
        {
            _vcamCockpit.Priority = 100;
            _vcamShip.Priority = 10;
            _cockpit.SetActive(true);
            _shipRender.enabled = false;
        }
    }

    public void Idle()
    {
        _cockpit.SetActive(false);
        _vcamCockpit.Priority = 10;
        _vcamShip.Priority = 10;
        _shipRender.enabled = true;

        IdleSwitch(true);
    }

    public void WakeUp()
    {
        IdleSwitch(false);
    }

    private void IdleSwitch(bool idle)
    {
        if (idle)
        {
            StartCoroutine("IdleCycle");
        }
        else
        {
            _vcamShip.Priority = 100;
            StopCoroutine("IdleCycle");
        }
    }

    IEnumerator IdleCycle()
    {
        int camIndex = Random.Range(0, _idleCams.Length);

        while (true)
        {
            for (int i = 0; i < _idleCams.Length; i++)
            {
                if (_idleCams[i].TryGetComponent(out CinemachineVirtualCamera vcam))
                {
                    if (i == camIndex)
                    {
                        vcam.Priority = 11;
                    }
                    else
                    {
                        vcam.Priority = 10;
                    }
                }
                else if (_idleCams[i].TryGetComponent(out CinemachineBlendListCamera blend))
                {
                    if (i == camIndex)
                    {
                        blend.Priority = 11;
                    }
                    else
                    {
                        blend.Priority = 10;
                    }
                }
                else
                {
                    Debug.LogError("Failed to detect camera type of " + _idleCams[i]);
                }
            }

            camIndex++;

            if (camIndex > _idleCams.Length - 1)
            {
                camIndex = 0;
            }

            yield return new WaitForSeconds(10.0f);
        }
    }
}
