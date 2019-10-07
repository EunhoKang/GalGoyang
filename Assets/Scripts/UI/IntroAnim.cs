using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroAnim : MonoBehaviour
{
    public Image intro;

    private void OnEnable()
    {
        StartCoroutine(StartEffect());
    }
    private void OnDisable()
    {
        StopCoroutine(StartEffect());
        Color c = intro.color;
        c.a = 0;
        intro.color = c;
    }

    IEnumerator StartEffect()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.05f);
        Color c = intro.color;
        c.a = 0;
        for (float i=0; i<=1.1f;i+=0.05f)
        {
            c.a = i;
            intro.color = c;
            yield return wfs;
        }
        Invoke("Change", 1f);
    }

    void Change()
    {
        UIManager.uimanager.RemoveCanvas(1);
        UIManager.uimanager.ShowCanvas(2);
    }
}
