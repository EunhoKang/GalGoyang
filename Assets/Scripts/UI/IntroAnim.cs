using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroAnim : MonoBehaviour
{
    public Image intro;
    public Text version;
    public Text copyright;
    public Text touchInfo;
    bool gotonext;
    WaitForSeconds wfs;
    WaitForSeconds wfs2;

    private void Awake()
    {
        wfs = new WaitForSeconds(0.05f);
        wfs2 = new WaitForSeconds(0.8f);
    }

    private void OnEnable()
    {
        StartCoroutine(StartEffect());
        gotonext = false;
        version.gameObject.SetActive(false);
        copyright.gameObject.SetActive(false);
        touchInfo.gameObject.SetActive(false);
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
        for (float i=0; i<=1.1f;i+=0.05f)
        {
            c.a = i;
            intro.color = c;
            yield return wfs;
        }
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        yield return wfs2;
        version.gameObject.SetActive(true);
        copyright.gameObject.SetActive(true);
        touchInfo.gameObject.SetActive(true);
        Color c = touchInfo.color;
        c.a = 0;
        int cnt = 0;
        while (!gotonext)
        {
            if (cnt % 2 == 1)
            {
                c.a = 1;
            }
            else
            {
                c.a = 0;
            }
            touchInfo.color = c;
            cnt++;
            yield return wfs2;
        }
        SoundManager.soundmanager.UIClick();
        UIManager.uimanager.RemoveCanvas(1);
        UIManager.uimanager.ShowCanvas(7);
    }
    public void GotoNext()
    {
        gotonext = true;
    }
}
