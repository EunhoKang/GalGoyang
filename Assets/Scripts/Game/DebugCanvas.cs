using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    public Slider fastenSlider;
    public Text fastenText;
    StringBuilder fastenStringBuilder = new StringBuilder("x");
    public void Update()
    {
        fastenStringBuilder.Append((Mathf.Round(fastenSlider.value*10)*0.1f).ToString());
        fastenText.text = fastenStringBuilder.ToString();
        fastenStringBuilder.Remove(1,fastenStringBuilder.Length-1);
    }
    public void JumpSpeedManage()
    {
        CharacterManager.charmanager.playerScript.SpeedChange(fastenSlider.value);
        CharacterManager.charmanager.speedMultiplier = fastenSlider.value;
    }
}
