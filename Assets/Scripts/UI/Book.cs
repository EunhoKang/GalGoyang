using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public Image leftButton;
    public Image rightButton;
    public int maxPageNum;
    public List<GameObject> pageList;
    public List<GameObject> chapterList;
    private int pagenum=0;

    public void OnEnable()
    {
        pagenum = 1;
        showPage(pagenum);
        leftButton.gameObject.SetActive(false);
    }

    public void catSelect(string catName)
    {
        SoundManager.soundmanager.UIClick();
        //맵 파일과 스크립트 파일이 모두 마련되면 아래 if문을 지울 것
        if (catName != "lucy")
        {
            return;
        }

        UIManager.uimanager.catName = catName;
        //후에 필요한 리소스가 추가되면, 고양이에 따라 바뀌는 텍스트나 이미지를 구현할 것
    }

    public void buttonLeft()
    {
        SoundManager.soundmanager.UIClick();
        pagenum--;
        if (pagenum <= 1)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
        }
        else
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
        }
        showPage(pagenum);
    }

    public void buttonRight()
    {
        SoundManager.soundmanager.UIClick();
        pagenum++;
        if (pagenum >= maxPageNum)
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(false);
        }
        else
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
        }
        showPage(pagenum);
    }

    public void showPage(int num)
    {
        for(int i = 0; i < pageList.Count; i++)
        {
            pageList[i].SetActive(false);
        }
        pageList[num - 1].SetActive(true);
    }

    public void sideStory()
    {
        SoundManager.soundmanager.UIClick();
        pagenum = 5;
        showPage(pagenum);
    }

    public void album()
    {
        SoundManager.soundmanager.UIClick();
        pagenum = 6;
        showPage(pagenum);
    }

    public void ScriptChange(string name)
    {
        UIManager.uimanager.LoadingScreen();
        StartCoroutine(ScriptMode(name));
    }

    IEnumerator ScriptMode(string name)
    {
        SoundManager.soundmanager.UIClick();
        yield return new WaitForSeconds(0.5f);
        yield return null;
        UIManager.uimanager.scriptName = name;
        UIManager.uimanager.ShowCanvas(13);
        UIManager.uimanager.RemoveCanvas(11);
        UIManager.uimanager.EndLoading();
    }
}
