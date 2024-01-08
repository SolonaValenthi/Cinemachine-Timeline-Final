using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _controlText;
    [SerializeField] private GameObject _idleText;

    private bool _inCutscene;

    // Start is called before the first frame update
    void Start()
    {
        _idleText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void IdleToggle(bool idle)
    {
        if (idle)
        {
            _controlText.SetActive(false);
            _idleText.SetActive(true);
        }
        else
        {
            _controlText.SetActive(true);
            _idleText.SetActive(false);
        }
    }

    public void CutsceneToggle()
    {
        if (_inCutscene)
        {
            _controlText.SetActive(true);
            _idleText.SetActive(false);
            _inCutscene = false;
        }
        else
        {
            _controlText.SetActive(false);
            _idleText.SetActive(false);
            _inCutscene = true;
        }
    }
}
