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

    IEnumerator RestartCoroutine()
    {
        SoundManager.soundmanager.UIClick();
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        yield return null;
        CharacterManager.charmanager.Init();
        MapManager.mapmanager.Init();
        lucy.gameObject.SetActive(false);
        mum.gameObject.SetActive(false);
        b3.gameObject.SetActive(false);
        UIManager.uimanager.RemoveCanvas(2);
        UIManager.uimanager.ShowCanvas(2);
        UIManager.uimanager.RemoveCanvas(4);
    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator GoBackToMenuCoroutine()
    {
        SoundManager.soundmanager.UIClick();
        int i = 0;
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        yield return null;
        lucy.gameObject.SetActive(false);
        mum.gameObject.SetActive(false);
        b3.gameObject.SetActive(false);
        UIManager.uimanager.RemoveCanvas(2);
        UIManager.uimanager.ShowCanvas(1);
        UIManager.uimanager.RemoveCanvas(4);
    }

    public void GoBackToMenu()
    {
        StartCoroutine(GoBackToMenuCoroutine());
    }
}
