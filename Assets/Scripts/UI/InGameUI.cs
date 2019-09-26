using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class InGameUI : MonoBehaviour
{
    [Header("Score UI")]
    public Text scoreText;
    StringBuilder scoreStringBuilder;

    [Header("Health UI")]
    public int maxHealthCount = 7;
    public GameObject[] hearts;

    [Header("Other UI")]
    public GameObject JumpButton;
    public GameObject ActionButton;

    public Image[] BINKY_On;
    private bool[] alphabetOn = { false, false, false, false, false };

    private void OnEnable()
    {
        scoreText.text = "00000000";
        scoreStringBuilder= new StringBuilder("00000000");
        //ScoreUI(0);
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(true);
        }
        for(int i = 0; i < alphabetOn.Length; i++)
        {
            BINKY_On[i].gameObject.SetActive(false);
            alphabetOn[i] = false;
        }
        CharacterManager.charmanager.GetCallback(ScoreUI, HealthUI,AlphabetUI,ReadyForAction,ReadyForJump);
    }

    public void AlphabetUI(int index)
    {
        BINKY_On[index].gameObject.SetActive(true);
        alphabetOn[index] = true;
        for (int i = 0; i < alphabetOn.Length; i++)
        {
            if (!alphabetOn[i])
            {
                return;
            }
        }
        for (int i = 0; i < alphabetOn.Length; i++)
        {
            BINKY_On[i].gameObject.SetActive(false);
            alphabetOn[i] = false;
        }
    }

    public void HealthUI(int health)
    {
        for (int i = 0; i < maxHealthCount; i++)
        {
            hearts[i].SetActive(false);
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public void ScoreUI(int score)
    {
        scoreStringBuilder.Remove(0, 8);
        string temp =CharacterManager.charmanager.playerScore.ToString();
        for (int i = 0; i < 8 - temp.Length; i++)
        {
            scoreStringBuilder.Append('0');
        }
        if (temp.Length <= 8)
        {
            scoreStringBuilder.Append(temp);
        }
        else
        {
            scoreStringBuilder.Append("99999999");
        }
        scoreText.text = scoreStringBuilder.ToString();
    }

    public void Jump()
    {
        CharacterManager.charmanager.PlayerJump();
    }
    public void Parkour()
    {
        CharacterManager.charmanager.PlayerParkour();
    }
    public void Tag()
    {
        CharacterManager.charmanager.Tag();
    }

    public void ReadyForAction()
    {
        JumpButton.SetActive(false);
        ActionButton.SetActive(true);
    }
    public void ReadyForJump()
    {
        JumpButton.SetActive(true);
        ActionButton.SetActive(false);
    }


}
