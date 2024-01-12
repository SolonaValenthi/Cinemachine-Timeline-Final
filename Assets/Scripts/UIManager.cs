using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _controlText;
    [SerializeField] private GameObject _idleText;
    [SerializeField] private GameObject _outroDisplay;

    private bool _inCutscene;

    // Start is called before the first frame update
    void Start()
    {
        _idleText.SetActive(false);
        _outroDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
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

    public void OutroDisplay()
    {
        _outroDisplay.SetActive(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
