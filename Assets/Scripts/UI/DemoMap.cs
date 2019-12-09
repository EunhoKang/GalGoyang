using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMap : MonoBehaviour
{
    public void SceneChange(int num)
    {
        UIManager.uimanager.LoadingScreen();
        StartCoroutine(ChangeScene(num));
    }
    IEnumerator ChangeScene(int num)
    {
        SoundManager.soundmanager.UIClick();
        UIManager.uimanager.mapName = "stage"+num.ToString();
        yield return new WaitForSeconds(1f);
        CharacterManager.charmanager.Init();
        MapManager.mapmanager.Init();
        yield return null;
        UIManager.uimanager.ShowCanvas(3);
        UIManager.uimanager.EndLoading();
        UIManager.uimanager.RemoveCanvas(10);
    }
}
