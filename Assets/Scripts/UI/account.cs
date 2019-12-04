using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class account : MonoBehaviour
{
    public void fCLick()
    {
        UIManager.uimanager.RemoveCanvas(8);
        UIManager.uimanager.ShowCanvas(7);
    }
}
