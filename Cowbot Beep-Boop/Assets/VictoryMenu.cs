using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void OnOpenStartMenu()
    {
        Debug.Log("HERE");
        SceneManager.LoadScene("GameStart");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
