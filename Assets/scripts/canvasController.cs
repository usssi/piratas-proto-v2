using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasController : MonoBehaviour
{
    public void ActionHideShowCanvas(GameObject canvas)
    {
        if (canvas.activeInHierarchy == true)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }
    }

    public void ActionSizeChange(GameObject canvas)
    {
        if (canvas.transform.localScale == Vector3.one)
        {
            canvas.transform.localScale = Vector3.zero;
            
            Time.timeScale = 1;

        }
        else
        {
            canvas.transform.localScale = Vector3.one;
            Time.timeScale = 0;

        }
    }

    public void ActionHideSizeOther(GameObject canvas)
    {
        if (canvas.transform.localScale == Vector3.one)
        {
            canvas.transform.localScale = Vector3.zero;

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
