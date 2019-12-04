using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CSVReader : MonoBehaviour
{
    public Text nameArea;
    public Image emotionImage;
    public Text textArea;
    public Image BG;
    public List<List<string>> csvListed;

    int index = 0;

    Dictionary<string, Sprite> emotions;
    Dictionary<string, Sprite> BGs;
    Color nullSprite;
    Color notNullSprite;

    public void Awake()
    {
        nullSprite = new Color(1, 1, 1, 0);
        notNullSprite = new Color(1, 1, 1, 1);
        emotions = new Dictionary<string, Sprite>();
        object[] temp = Resources.LoadAll("emotion");
        List<object> tempList = new List<object>();
        for(int i=1; i < temp.Length; i+=2)
        {
            tempList.Add(temp[i]);
        }
        for(int i=0; i < tempList.Count; i++)
        {
            emotions.Add(tempList[i].ToString().Split(' ')[0], tempList[i] as Sprite);
        }
    }

    public void OnEnable()
    {
        index = 0;
        BGs = new Dictionary<string, Sprite>();
        string scriptPath = UIManager.uimanager.scriptName + "/script_"+UIManager.uimanager.catName;
        if (UIManager.uimanager.scriptName == "" || UIManager.uimanager.catName == "")
        {
            return;
        }
        csvListed =Read(scriptPath);
        WindowUpdate();
    }

    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public List<List<string>> Read(string file)
    {
        List<List<string>> list = new List<List<string>>();
        for(int i = 0; i < 4; i++)
        {
            list.Add(new List<string>());
        }
        TextAsset data = Resources.Load(file) as TextAsset;
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) return list;

        for(int i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], ",");
            if (values.Length == 0 || values[0] == "") continue;
            values[1] = values[1].Replace("^", ",");
            values[3] = values[3].Replace("^", ",");
            list[0].Add(values[1]);
            if (values[1] == "루시")
            {
                list[1].Add("lucy_" + values[2]);
            }
            else if (values[1] == "B-312")
            {
                list[1].Add("b312_" + values[2]);
            }
            else if (values[1] == "무명")
            {
                list[1].Add("moomeong_" + values[2]);
            }
            else if (values[1] == "나비")
            {
                list[1].Add("naby_" + values[2]);
            }
            else if (values[1] == "삼월이")
            {
                list[1].Add("b312_" + values[2]);
            }
            else
            {
                list[1].Add(values[2]);
            }
            list[2].Add(values[3]);
            list[3].Add(values[4]);
            if (values[4] != "-")
            {
                Sprite temp = Resources.Load<Sprite>(UIManager.uimanager.scriptName + "/" + values[4]);
                BGs.Add(temp.name, temp);
            }
            Debug.Log(list[0][list[0].Count - 1]+"||"+ list[1][list[1].Count - 1]+"||"+ list[2][list[2].Count - 1] + "||"+ list[3][list[3].Count - 1]);
        }
        return list;
    }

    public void WindowUpdate()
    {
        SoundManager.soundmanager.UIClick();
        if (index > csvListed[0].Count-1)
        {
            return;
        }
        if (csvListed[0][index] == "-")
        {
            nameArea.text = "";
        }
        else
        {
            nameArea.text = csvListed[0][index];
        }
        if (csvListed[1][index] == "-")
        {
            emotionImage.sprite = null;
            emotionImage.color = nullSprite;
        }
        else
        {
            emotionImage.sprite = emotions[csvListed[1][index]];
            emotionImage.color = notNullSprite;
        }
        textArea.text = csvListed[2][index];
        if (csvListed[3][index] != "-")
        {
            BG.color = notNullSprite;
            BG.sprite = BGs[csvListed[3][index]];
        }
        index++;
    }
    public void ReturnToMain()
    {
        UIManager.uimanager.LoadingScreen();
        StartCoroutine(ScriptSceneEnd());
    }
    IEnumerator ScriptSceneEnd()
    {
        SoundManager.soundmanager.UIClick();
        yield return new WaitForSeconds(0.4f);
        UIManager.uimanager.ShowCanvas(2);
        UIManager.uimanager.RemoveCanvas(13);
        UIManager.uimanager.EndLoading();
    }
}
