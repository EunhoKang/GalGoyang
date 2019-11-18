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
    public Image[] TagImages;
    private int TaggedCount=30000;
    public Slider tagSlider;
    public RectTransform tagSliderPos;
    public GameObject pauseMenu;

    private Vector3[] TagPos = new Vector3[3];
    private Vector3[] TagVel = new Vector3[3];
    private Vector3 Oscale = new Vector3(1f, 1f, 1f);
    private Vector3 Bscale = new Vector3(0.5f, 0.5f, 1f);
    private Vector4 Ocolor = new Vector4(1f, 1f, 1f, 1f);
    private Vector4 Bcolor = new Vector4(0.5f, 0.5f, 0.5f, 1f);
    float ovalScaleX = Screen.width * 0.06f;
    float ovalScaleY = Screen.height * 0.04f;

    public Image[] BINKY_On;
    private bool[] alphabetOn = { false, false, false, false, false };

    private void OnEnable()
    {
        ResetTags();
        TaggedCount = 30000;
        scoreText.text = "00000000";
        scoreStringBuilder= new StringBuilder("00000000");

        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(true);
        }
        for(int i = 0; i < alphabetOn.Length; i++)
        {
            BINKY_On[i].gameObject.SetActive(false);
            alphabetOn[i] = false;
        }
        
        if(CharacterManager.charmanager!=null)
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

    bool SliderMove = false;
    public void SliderMoveNext(bool isLeft)
    {
        if (isLeft)
        {
            CharacterManager.charmanager.Tag(true);
            StartCoroutine(SliderLeftMove());
        }
        else
        {
            CharacterManager.charmanager.Tag(false);
            StartCoroutine(SliderRightMove());
        }
    }
    IEnumerator SliderLeftMove()
    {
        TaggedCount--;
        WaitForSeconds wfs = new WaitForSeconds(0.01f);
        Vector3 temp=new Vector3(0,0,0);
        for (float i = -1; i >= -20; i--)
        {
            temp.x = tagSliderPos.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * ((i / 30f) - 0.5f));
            temp.y = tagSliderPos.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * ((i / 30f) - 0.5f));
            TagImages[0].rectTransform.position= temp;
            temp.x = tagSliderPos.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * ((i / 30f) + (1f / 6f)));
            temp.y = tagSliderPos.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * ((i / 30f) + (1f / 6f)));
            TagImages[1].rectTransform.position = temp;
            temp.x = tagSliderPos.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * ((i / 30f) + (5f / 6f)));
            temp.y = tagSliderPos.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * ((i / 30f) + (5f / 6f)));
            TagImages[2].rectTransform.position = temp;
            TagImages[0].rectTransform.localScale = Vector3.Lerp(Bscale, Oscale, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) - 0.5f)) + 0.5f);
            TagImages[1].rectTransform.localScale = Vector3.Lerp(Bscale, Oscale, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (1f / 6f))) + 0.5f);
            TagImages[2].rectTransform.localScale = Vector3.Lerp(Bscale, Oscale, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (5f / 6f))) + 0.5f);
            TagImages[0].color = Vector4.Lerp(Bcolor, Ocolor, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) - 0.5f)) + 0.5f);
            TagImages[1].color = Vector4.Lerp(Bcolor, Ocolor, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (1f / 6f))) + 0.5f);
            TagImages[2].color = Vector4.Lerp(Bcolor, Ocolor, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (5f / 6f))) + 0.5f);
            yield return wfs;
        }
        Image temp2 = TagImages[0];
        TagImages[0] = TagImages[1];
        TagImages[1] = TagImages[2];
        TagImages[2] = temp2;
       
        SliderMove = false;
    }
    IEnumerator SliderRightMove()
    {
        TaggedCount++;
        WaitForSeconds wfs = new WaitForSeconds(0.01f);
        Vector3 temp = new Vector3(0, 0, 0);
        for (float i = 1; i <= 20; i++)
        {
            temp.x = tagSliderPos.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * ((i / 30f) - 0.5f));
            temp.y = tagSliderPos.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * ((i / 30f) - 0.5f));
            TagImages[0].rectTransform.position = temp;
            temp.x = tagSliderPos.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * ((i / 30f) + (1f / 6f)));
            temp.y = tagSliderPos.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * ((i / 30f) + (1f / 6f)));
            TagImages[1].rectTransform.position = temp;
            temp.x = tagSliderPos.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * ((i / 30f) + (5f / 6f)));
            temp.y = tagSliderPos.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * ((i / 30f) + (5f / 6f)));
            TagImages[2].rectTransform.position = temp;
            TagImages[0].rectTransform.localScale = Vector3.Lerp(Bscale, Oscale, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) - 0.5f)) + 0.5f);
            TagImages[1].rectTransform.localScale = Vector3.Lerp(Bscale, Oscale, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (1f / 6f))) + 0.5f);
            TagImages[2].rectTransform.localScale = Vector3.Lerp(Bscale, Oscale, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (5f / 6f))) + 0.5f);
            TagImages[0].color = Vector4.Lerp(Bcolor, Ocolor, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) - 0.5f)) + 0.5f);
            TagImages[1].color = Vector4.Lerp(Bcolor, Ocolor, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (1f / 6f))) + 0.5f);
            TagImages[2].color = Vector4.Lerp(Bcolor, Ocolor, -0.5f * Mathf.Sin(Mathf.PI * ((i / 30f) + (5f / 6f))) + 0.5f);
            yield return wfs;
        }
        Image temp2 = TagImages[2];
        TagImages[2] = TagImages[1];
        TagImages[1] = TagImages[0];
        TagImages[0] = temp2;
        SliderMove = false;
    }
    private void Update()
    {
        float val = tagSlider.value;
        if (val != 0 )
        {
            tagSlider.value = 0;
            if (SliderMove || CharacterManager.charmanager.cantTouchTag || CharacterManager.charmanager.playerScript.playerstate != 0)
            {
                return;
            }
            if (val < 0)
            {
                SliderMoveNext(true);
            }
            else{
                SliderMoveNext(false);
            }
            
            SliderMove = true;
        }
    }
    public void ResetTags()
    {
        for (int i = 0; i < TaggedCount%3; i++)
        {
            Image temp2 = TagImages[0];
            TagImages[0] = TagImages[1];
            TagImages[1] = TagImages[2];
            TagImages[2] = temp2;
        }
        TagPos[0] = new Vector3(tagSlider.gameObject.transform.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * 3 / 2), 
            tagSlider.gameObject.transform.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * 3 / 2), 0);
        TagPos[1] = new Vector3(tagSlider.gameObject.transform.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * 1 / 6), 
            tagSlider.gameObject.transform.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * 1 / 6), 0);
        TagPos[2] = new Vector3(tagSlider.gameObject.transform.position.x + ovalScaleX * Mathf.Cos(Mathf.PI * 5 / 6), 
            tagSlider.gameObject.transform.position.y + ovalScaleY * Mathf.Sin(Mathf.PI * 5 / 6), 0);
        for (int i = 0; i < TagImages.Length; i++)
        {
            TagImages[i].rectTransform.position = TagPos[i];
            TagImages[i].rectTransform.localScale = Vector3.Lerp(Bscale, Oscale, (Mathf.Cos(Mathf.PI * 2 / 3 * i) + 1) / 2);
            TagImages[i].color = Vector4.Lerp(Bcolor, Ocolor, (Mathf.Cos(Mathf.PI * 2 / 3 * i) + 1) / 2);
        }
    }
}
