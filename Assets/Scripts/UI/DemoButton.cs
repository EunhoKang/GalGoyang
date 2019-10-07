﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoButton : MonoBehaviour
{
    public void SceneChange()
    {
        UIManager.uimanager.LoadingScreen();
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        SoundManager.soundmanager.UIClick();
        yield return new WaitForSeconds(1f);
        //SceneManager.UnloadSceneAsync("Game");
        //yield return new WaitForSeconds(1f);
        //SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        //yield return new WaitForSeconds(1f);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        //yield return new WaitForSeconds(0.5f);
        CharacterManager.charmanager.Init();
        MapManager.mapmanager.Init();
        yield return null;
        UIManager.uimanager.ShowCanvas(3);
        UIManager.uimanager.RemoveCanvas(2);
        UIManager.uimanager.EndLoading();
    }
}
