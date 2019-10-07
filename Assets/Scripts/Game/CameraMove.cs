using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 cameraInitialPosition;
    public float howFarFromCamera;
    public float zoomRate;
    private Vector3 CameraPlace;
    private Vector3 CameraVector;
    private WaitForEndOfFrame CameraWait=new WaitForEndOfFrame();
    private Camera camera;
    private Vector3 ResetPos;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographicSize = zoomRate;
        ResetPos = transform.position;
    }

    public void CameraStart(){
        if (CharacterManager.charmanager.playerScript == null)
        {
            return;
        }
        StartCoroutine(CameraUpdate());
    }
    public void CameraReset()
    {
        StopCoroutine(CameraUpdate());
        transform.position = ResetPos;
    }
    IEnumerator CameraUpdate(){
        CameraPlace = CharacterManager.charmanager.player.transform.position;
        CameraVector = new Vector3(CameraPlace.x, cameraInitialPosition.y, cameraInitialPosition.z);
        while (CharacterManager.charmanager.player != null){
            CameraVector.x= CharacterManager.charmanager.player.transform.position.x+howFarFromCamera;
            transform.position=CameraVector;
            yield return null;
        }
    }

    public void PlayerDoNotExist()
    {
        StopCoroutine(CameraUpdate());
    }
}
