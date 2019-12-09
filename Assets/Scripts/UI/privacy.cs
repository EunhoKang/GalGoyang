using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class privacy : MonoBehaviour
{
    public List<GameObject> listChecked;
    List<bool> listBool;

    public void OnEnable()
    {
        listBool = new List<bool>();
        for(int i = 0; i < listChecked.Count; i++)
        {
            listChecked[i].SetActive(false);
            listBool.Add(false);
        }
    }

    public void fClick(int num)
    {
        listChecked[num].SetActive(true);
        listBool[num] = true;
        if (allChecked())
        {
            //check
            SoundManager.soundmanager.UIClick();
            UIManager.uimanager.RemoveCanvas(6);
            UIManager.uimanager.ShowCanvas(1);
        }
    }
    bool allChecked()
    {
        for(int i = 0; i < listBool.Count; i++)
        {
            if (!listBool[i])
            {
                return false;
            }
        }
        return true;
    }
}



