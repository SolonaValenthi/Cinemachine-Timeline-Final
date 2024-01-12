using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FlightControls : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private PlayableDirector _repairDirector;
    [SerializeField] private PlayableDirector _warpDirector;
    [SerializeField] private PlayableDirector _outroDirector;
    [SerializeField] private ParticleSystem[] _sideEngines;
    [SerializeField] private ParticleSystem _mainEngine;

    private float _vertical;
    private float _horizontal;
    private float _roll;
    private bool _inCutscene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inCutscene == false)
        {
            ShipMovement();
        }
    }

    private void ShipMovement()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        _roll = Input.GetAxis("Roll");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSpeed += 0.3f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _moveSpeed -= 0.3f;
        }
        _moveSpeed = Mathf.Clamp(_moveSpeed, 0f, 15f);

        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left * _rotSpeed * _vertical * Time.deltaTime); // pitch control
        transform.Rotate(Vector3.up * _rotSpeed * _horizontal * Time.deltaTime); // yaw control
        transform.Rotate(Vector3.forward * (_rotSpeed * 1.5f) * _roll * Time.deltaTime); // roll control

        foreach (var emitter in _sideEngines)
        {
            var emission = emitter.emission;
            emission.rateOverTime = _moveSpeed;
        }

        var mainEmission = _mainEngine.emission;
        mainEmission.rateOverTime = _moveSpeed * 3;
    }

    public void CutsceneToggle()
    {
        if (_inCutscene)
        {
            _inCutscene = false;
        }
        else
        {
            _inCutscene = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Satellite")
        {
            if (_repairDirector.gameObject.activeInHierarchy == true)
            {
                _moveSpeed = 0.0f;
                _repairDirector.Play();
            }
        }
        if (other.tag == "Warp")
        {
            if (_warpDirector.gameObject.activeInHierarchy == true)
            {
                _warpDirector.Play();
            }
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Flagship")
        {
            _warpDirector.gameObject.SetActive(false);
            _outroDirector.gameObject.SetActive(true);
        }
    }
}
