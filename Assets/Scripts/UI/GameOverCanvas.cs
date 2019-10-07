using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    private Image endImage;
    public Image lucy;
    public Image mum;
    public Image b3;

    private void OnEnable()
    {
        lucy.gameObject.SetActive(false);
        mum.gameObject.SetActive(false);
        b3.gameObject.SetActive(false);
        if (CharacterManager.charmanager!=null)
            PopUp();
    }

    public void PopUp()
    {
        if (CharacterManager.charmanager.playerScript == null)
        {
            return;
        }
        if (CharacterManager.charmanager.playerScript.PlayerChar == Player.PlayerType.b312)
        {
            b3.gameObject.SetActive(true);
            endImage = b3;
        }
        else if(CharacterManager.charmanager.playerScript.PlayerChar == Player.PlayerType.rucy)
        {
            lucy.gameObject.SetActive(true);
            endImage = lucy;
        }
        else
        {
            mum.gameObject.SetActive(true);
            endImage = mum;
        }
        StartCoroutine(PopUpUI());
    }
    IEnumerator PopUpUI()
    {
        Vector3 temp = new Vector3(0, -850, 0);
        endImage.rectTransform.localPosition = temp;
        WaitForSeconds wfs = new WaitForSeconds(0.005f);
        for (int i = 0; i <= 100; i++)
        {
            temp.y = -0.16f * (i - 75) * (i - 75) + 100;
            endImage.rectTransform.localPosition = temp;
            yield return wfs;
        }
    }

    public void Restart()
    {
        SoundManager.soundmanager.UIClick();
        UIManager.uimanager.GameRestart();
    }

    IEnumerator GoBackToMenuCoroutine()
    {
        SoundManager.soundmanager.UIClick();
        CharacterManager.charmanager.ResetAll();
        MapManager.mapmanager.ResetAll();
        yield return new WaitForSeconds(0.5f);
        UIManager.uimanager.RemoveCanvas(3);
        UIManager.uimanager.ShowCanvas(2);
        UIManager.uimanager.EndLoading();
        UIManager.uimanager.RemoveCanvas(5);
    }

    public void GoBackToMenu()
    {
        StartCoroutine(GoBackToMenuCoroutine());
        UIManager.uimanager.LoadingScreen();
    }
}
