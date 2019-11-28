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
    public void OnEnable()
    {
        index = 0;
        string scriptPath = UIManager.uimanager.scriptName + "/script";
        if (UIManager.uimanager.scriptName == "")
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
            list[1].Add(values[2]);
            list[2].Add(values[3]);
            list[3].Add(values[4]);
            Debug.Log(list[1][list[1].Count - 1]);
            Debug.Log(list[3][list[3].Count - 1]);
        }
        nameArea.text = list[0][list[0].Count - 1];
        textArea.text = list[2][list[2].Count - 1];
        return list;
    }

    public void WindowUpdate()
    {
        if (index > csvListed[0].Count-1)
        {
            return;
        }
        nameArea.text = csvListed[0][index];
        textArea.text = csvListed[2][index];
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
        UIManager.uimanager.RemoveCanvas(6);
        UIManager.uimanager.EndLoading();
    }
}
