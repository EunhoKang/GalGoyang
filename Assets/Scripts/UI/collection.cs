using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collection : MonoBehaviour
{
    public void fclick()
    {
        UIManager.uimanager.RemoveCanvas(7);
        UIManager.uimanager.ShowCanvas(11);
    }
}
