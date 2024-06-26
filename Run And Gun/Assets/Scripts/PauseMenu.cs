using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    /// <summary>
    /// Als pause menu aan staat wordt die uitgezet en als die uit staat wordt die aan gezet
    /// </summary>
    public void Pause()
    {
        PauseObject.SetActive(!PauseObject.activeSelf);

        if (!PauseObject.activeSelf)
        {
            Time.timeScale = 1.0f;
        }
        if (PauseObject.activeSelf)
        { 
            Time.timeScale = 0f;
        }
    }

}
