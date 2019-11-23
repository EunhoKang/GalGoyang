using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource[] SFX;
    public AudioClip m_UI_Click;

    public static SoundManager soundmanager;
    private void Awake()
    {
        if (soundmanager == null)
        {
            soundmanager = this;
        }
        else if (soundmanager != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void BGMSet(AudioClip a)
    {
        BGM.clip = a;
        BGM.Play();
    }
    public void BGMStop()
    {
        BGM.Stop();
    }
    public void SoundPause()
    {
        BGM.Pause();
        //for(int i = 0; i < SFX.Length; i++)
        //{
        //    SFX[i].Pause();
        //}
    }
    public void SoundResume()
    {
        BGM.Play();
        //for (int i = 0; i < SFX.Length; i++)
        //{
        //    SFX[i].Play();
        //}
    }
    public void SFXSet(AudioClip a, int source)
    {
        SFX[source].clip = a;
        SFX[source].Play();
    }

    public void UIClick()
    {
        SFX[1].clip = m_UI_Click;
        SFX[1].Play();
    }
}
