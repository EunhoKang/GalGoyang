using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoButton : MonoBehaviour
{
    public void SceneChange()
    {
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        SoundManager.soundmanager.UIClick();
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        UIManager.uimanager.RemoveCanvas(1);
        UIManager.uimanager.ShowCanvas(2);
        CharacterManager.charmanager.Init();
        MapManager.mapmanager.Init();
    }
}
