using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoButton : MonoBehaviour
{
    public void SceneChange()
    {
        SoundManager.soundmanager.UIClick();
        UIManager.uimanager.RemoveCanvas(1);
        UIManager.uimanager.ShowCanvas(2);
        CharacterManager.charmanager.Init();
        MapManager.mapmanager.Init();
    }
}
