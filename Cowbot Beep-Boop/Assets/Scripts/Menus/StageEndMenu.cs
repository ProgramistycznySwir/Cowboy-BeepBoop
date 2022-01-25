using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndMenu : MonoBehaviour
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

    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
