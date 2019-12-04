using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;



public class privacy : MonoBehaviour

{

    public void fClick()

    {
        UIManager.uimanager.RemoveCanvas(6);
        UIManager.uimanager.ShowCanvas(8);

    }

}



