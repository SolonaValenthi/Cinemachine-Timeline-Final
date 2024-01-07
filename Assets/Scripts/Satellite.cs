using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    [SerializeField] private Light _errorLight;

    private WaitForSeconds _blinkDelay = new WaitForSeconds(0.25f);

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("ErrorBlink");
        //StartCoroutine("ErrorPulse");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ErrorBlink()
    {
        while (true)
        {
            _errorLight.enabled = false;
            yield return _blinkDelay;
            _errorLight.enabled = true;
            yield return _blinkDelay;
        }
    }

    IEnumerator ErrorPulse()
    {
        while (true)
        {
            while (_errorLight.intensity > 0)
            {
                _errorLight.intensity -= 0.5f;
                yield return null;
            }
            while (_errorLight.intensity < 25)
            {
                _errorLight.intensity += 0.5f;
                yield return null;
            }
        }
    }
}
