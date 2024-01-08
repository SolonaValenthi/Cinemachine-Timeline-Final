using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    [SerializeField] private Light _errorLight;

    private WaitForSeconds _flashTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 0.08f);
    }

    public void FixColor()
    {
        _errorLight.color = new Color(0, 1, 0, 1);
        StopCoroutine("ErrorPulse");
    }

    public void BreakTrigger()
    {
        StartCoroutine(BreakLight());
    }

    IEnumerator BreakLight()
    {
        float elapsed = 0.0f;

        while (elapsed < 0.2f)
        {
            _errorLight.intensity = Random.Range(5, 21);
            yield return _flashTime;
            elapsed += 0.01f;
        }

        yield return new WaitForSeconds(0.1f);
        _errorLight.intensity = 0.0f;
        _errorLight.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.75f);

        StartCoroutine("ErrorPulse");
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
