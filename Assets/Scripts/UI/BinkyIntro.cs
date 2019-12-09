using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinkyIntro : MonoBehaviour
{
    public Image intro;
    WaitForSeconds wfs;

    private void Awake()
    {
        wfs = new WaitForSeconds(0.05f);
    }

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

        Color c = intro.color;
        c.a = 0;
        for (float i = 0; i <= 1.1f; i += 0.05f)
        {
            c.a = i;
            intro.color = c;
            yield return wfs;
        }
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        yield return null;
        UIManager.uimanager.RemoveCanvas(14);
        UIManager.uimanager.ShowCanvas(6);
        
    }
}
