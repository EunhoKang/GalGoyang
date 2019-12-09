using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainUI : MonoBehaviour
{
    public List<GameObject> imageList;
    public GameObject optionList;
    Dictionary<string, GameObject> imageDictionary;
    bool isOptionOn;

    private void Awake()
    {
        imageDictionary = new Dictionary<string, GameObject>();
        imageDictionary.Add("moomeong", imageList[0]);
        imageDictionary.Add("b312", imageList[1]);
        imageDictionary.Add("lucy", imageList[2]);
    }

    public void OnEnable()
    {
        isOptionOn = false;
    }

    public void catSelect(string catName)
    {
        //맵 파일과 스크립트 파일이 모두 마련되면 아래 if문을 지울 것
        if (catName != "lucy")
        {
            return;
        }

        if (!isOptionOn)
        {
            UIManager.uimanager.catName = catName;
            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].SetActive(false);
            }
            imageDictionary[catName].SetActive(true);
            SoundManager.soundmanager.UIClick();
        }
    }

    public void gotoPlaza()
    {
        if (!isOptionOn)
        {
            SoundManager.soundmanager.UIClick();
            UIManager.uimanager.RemoveCanvas(7);
            UIManager.uimanager.ShowCanvas(12);
        }
    }
    public void gotoAchivement()
    {
        if (!isOptionOn)
        {
            SoundManager.soundmanager.UIClick();
            UIManager.uimanager.RemoveCanvas(7);
            UIManager.uimanager.ShowCanvas(9);
        }
    }
    public void gotoMap()
    {
        if (!isOptionOn)
        {
            SoundManager.soundmanager.UIClick();
            UIManager.uimanager.RemoveCanvas(7);
            UIManager.uimanager.ShowCanvas(10);
        }
    }
    public void gotoBook()
    {
        if (!isOptionOn)
        {
            SoundManager.soundmanager.UIClick();
            UIManager.uimanager.RemoveCanvas(7);
            UIManager.uimanager.ShowCanvas(11);
        }
    }


    public void option()
    {
        if (!isOptionOn)
        {
            SoundManager.soundmanager.UIClick();
            optionList.SetActive(true);
            isOptionOn = true;
        }
    }
    public void backToMain()
    {
        SoundManager.soundmanager.UIClick();
        optionList.SetActive(false);
        isOptionOn = false;
    }
}
