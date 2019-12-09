using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    public Text scoreText;
    public Text catFoodText;
    public Text catFoodsText;
    public Text canFoodText;
    public Text catleafText;
    public Image endImage;

    private void OnEnable()
    {
        scoreText.text = "000000000000";
        catFoodText.text = "00";
        catFoodsText.text = "00";
        canFoodText.text = "00";
        catleafText.text = "00";
        if (CharacterManager.charmanager != null)
            PopUp();
    }

    public void PopUp()
    {
        if (CharacterManager.charmanager.playerScript == null)
        {
            return;
        }
        StartCoroutine(PopUpUI());
        StartCoroutine(ScoreUpdate(CharacterManager.charmanager.playerScore, 0));
        StartCoroutine(ScoreUpdate(CharacterManager.charmanager.catFoodCount, 1));
        StartCoroutine(ScoreUpdate(CharacterManager.charmanager.catFoodsCount, 2));
        StartCoroutine(ScoreUpdate(CharacterManager.charmanager.canFoodCount, 3));
        StartCoroutine(ScoreUpdate(CharacterManager.charmanager.catLeafCount, 4));
    }
    IEnumerator PopUpUI()
    {
        Vector3 temp = new Vector3(0, -850,0);
        endImage.rectTransform.localPosition = temp;
        WaitForSeconds wfs = new WaitForSeconds(0.005f);
        for (int i = 0; i <= 100; i++)
        {
            temp.y= -0.16f * (i - 75) * (i - 75) + 100;
            endImage.rectTransform.localPosition = temp;
            yield return wfs;
        }
    }
    IEnumerator ScoreUpdate(int score, int num)
    {
        if (num == 0)
        {
            for (int i = 0; i <= score; i+=1000)
            { 
                scoreText.text = i.ToString();
                yield return null;
            }
            scoreText.text = score.ToString();
        }
        else if(num == 1)
        {
            for (int i = 0; i <= score; i+=5)
            {
                catFoodText.text = i.ToString();
                yield return null;
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i <= score; i++)
            {
                catFoodsText.text = i.ToString();
                yield return null;
            }
        }
        else if (num == 3)
        {
            for (int i = 0; i <= score; i++)
            {
                canFoodText.text = i.ToString();
                yield return null;
            }
        }
        else if (num == 4)
        {
            for (int i = 0; i <= score; i++)
            {
                catleafText.text = i.ToString();
                yield return null;
            }
        }
        catFoodText.text = CharacterManager.charmanager.catFoodCount.ToString();
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
        UIManager.uimanager.ShowCanvas(7);
        yield return null;
        UIManager.uimanager.EndLoading();
        UIManager.uimanager.RemoveCanvas(4);
    }

    public void GoBackToMenu()
    {
        StartCoroutine(GoBackToMenuCoroutine());
        UIManager.uimanager.LoadingScreen();
    }
}
