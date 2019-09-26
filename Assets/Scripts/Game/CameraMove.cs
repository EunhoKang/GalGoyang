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

    private void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographicSize = zoomRate;
    }

    public void CameraStart(){
        if (CharacterManager.charmanager.playerScript == null)
        {
            return;
        }
        StartCoroutine("CameraUpdate");
    }
    IEnumerator CameraUpdate(){ //코드 개선 필요(이벤트 방식)
        CameraPlace = CharacterManager.charmanager.player.transform.position;
        CameraVector = new Vector3(CameraPlace.x, cameraInitialPosition.y, cameraInitialPosition.z);
        while (true){
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
