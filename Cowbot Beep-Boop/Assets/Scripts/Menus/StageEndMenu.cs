using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndMenu : MonoBehaviour
{
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
