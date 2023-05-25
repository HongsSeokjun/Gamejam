using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Mgr : G_Singleton<Sound_Mgr>
{
    [HideInInspector] public AudioSource m_AudioSrc = null;
    Dictionary<string, AudioClip> m_ADClipList = new Dictionary<string, AudioClip>();

    //--- 효과음 최적화 변수
    int m_EffSdCount = 4;
    int m_iSndCount = 0;
    //최대 4개까지 동시 출력
    List<GameObject> m_sndObjList = new List<GameObject>();
    List<AudioSource> m_sndSrcList = new List<AudioSource>();
    float[] m_EffVolume = new float[10];
    //효과음 최적화 변수

    [HideInInspector] public bool m_SoundOnOff = true;
    [HideInInspector] public float m_SoundVolume = 1.0f;


    float m_bgmVolume = 0.2f;

    protected override void Init()
    {
        base.Init();

        LoadChildGameObj();
    }


    // Start is called before the first frame update
    void Start()
    {
        //사운드 미리 로드
        AudioClip a_GAudioClip = null;
        object[] temp = Resources.LoadAll("Sounds");
        for(int ii = 0; ii< temp.Length; ii++)
        {
            a_GAudioClip = temp[ii] as AudioClip;
            if (m_ADClipList.ContainsKey(a_GAudioClip.name) == true)
                continue;

            m_ADClipList.Add(a_GAudioClip.name, a_GAudioClip);

        }
        //사운드 미리 로드
    }

    // Update is called once per frame
    //void Update()
    //{

    //}



    public void LoadChildGameObj()
    {
        m_AudioSrc = this.gameObject.AddComponent<AudioSource>();

        for(int ii = 0; ii < m_EffSdCount; ii++)
        {
            GameObject newSoundOBJ = new GameObject();
            newSoundOBJ.transform.SetParent(this.transform);
            newSoundOBJ.transform.localPosition = Vector3.zero;
            AudioSource a_AudioSrc = newSoundOBJ.AddComponent<AudioSource>();
            a_AudioSrc.playOnAwake = false;
            a_AudioSrc.loop = false;
            newSoundOBJ.name = "SoundEffObj";

            m_sndSrcList.Add(a_AudioSrc);
            m_sndObjList.Add(newSoundOBJ);

        }

        float a_Value = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        
        SoundVolume(a_Value);
      

    }// public void LoadChildGameObj()

    public void PlayBGM(string a_FileName, float fVolume = 0.2f)
    {
        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;
        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }

        if (m_AudioSrc == null)
            return;

        if (m_AudioSrc.clip != null && m_AudioSrc.clip.name == a_FileName)
            return;

        m_AudioSrc.clip = a_GAudioClip;
        m_AudioSrc.volume = fVolume * m_SoundVolume;
        m_bgmVolume = fVolume;
        m_AudioSrc.loop = true;
        m_AudioSrc.Play();
    }


    public void PlayEffSound(string a_FileName, float fVolume = 0.2f)
    {

        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;
        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }

        if (a_GAudioClip != null && m_sndSrcList[m_iSndCount] != null)
        {

            m_sndSrcList[m_iSndCount].volume = fVolume * m_SoundVolume;
            m_sndSrcList[m_iSndCount].PlayOneShot(a_GAudioClip, fVolume * m_SoundVolume);
            m_EffVolume[m_iSndCount] = fVolume;

            m_iSndCount++;
            if (m_EffSdCount <= m_iSndCount)
                m_iSndCount = 0;
        }//if(a_GAudioClip != null && m_sndSrcList[m_iSndCount] != null)
    } //public void PlayEffSound(string a_FileName, float fVolume = 0.2f)

    public void PlayGUISound(string a_FileName, float fVolume = 0.2f)
    { //GUI 효과음 플레이 하기 위한 함수

        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;
        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }

        if (m_AudioSrc == null)
            return;

        m_AudioSrc.PlayOneShot(a_GAudioClip, fVolume * m_SoundVolume);

    }//public void PlayGUISound(string a_FileName, float fVolum = 0.2f)

    public void SoundVolume(float fVolume)
    {
        if (m_AudioSrc != null)
            m_AudioSrc.volume = m_bgmVolume * fVolume;

        for (int ii = 0; ii < m_sndSrcList.Count; ii++)
        {
            if (m_sndSrcList[ii] != null)
                m_sndSrcList[ii].volume = m_EffVolume[ii] * fVolume;
        }

        m_SoundVolume = fVolume;
    }
  
}
