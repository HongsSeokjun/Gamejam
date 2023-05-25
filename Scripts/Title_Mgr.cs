using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Title_Mgr : MonoBehaviour
{
    public Button Start_Btn = null;
    public Button Option_Btn = null;
    public Button Exit_Btn = null;
    public RawImage Option_Image = null;
    public RawImage Tutorial_Image = null;
    public Button Cancle_Btn = null;
    public Slider BGM_Slider = null;
    public Button Tutorial_Btn = null;
    public Button Cancle2_Btn = null;

    //애니메이션 관련 변수
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        animator = GetComponentInChildren<Animator>();

        if (Start_Btn != null)
            Start_Btn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("InGame");
                Sound_Mgr.Instance.PlayGUISound("3. click", 1.0f);
            });
        if (Exit_Btn != null)
            Exit_Btn.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        if (Option_Btn != null)
            Option_Btn.onClick.AddListener(OptionClick);

        if (Cancle_Btn != null)
            Cancle_Btn.onClick.AddListener(() =>
            {
                Option_Image.gameObject.SetActive(false);
                Sound_Mgr.Instance.PlayGUISound("3. click", 1.0f);
            });
        if (Cancle2_Btn != null)
            Cancle2_Btn.onClick.AddListener(() =>
            {
                Tutorial_Image.gameObject.SetActive(false);
                Sound_Mgr.Instance.PlayGUISound("3. click", 1.0f);
            });
        if (Tutorial_Btn != null)
            Tutorial_Btn.onClick.AddListener(() =>
            {
                Tutorial_Image.gameObject.SetActive(true);
                Sound_Mgr.Instance.PlayGUISound("3. click", 1.0f);
            });

        Sound_Mgr.Instance.PlayBGM("1. title bgm");

        if (BGM_Slider != null)
            BGM_Slider.onValueChanged.AddListener(BGM_SliderChange);

        if (BGM_Slider != null)
            BGM_Slider.value = PlayerPrefs.GetFloat("SoundVolume", 1.0f);


    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void OptionClick()
    {
        Option_Image.gameObject.SetActive(true);
        Sound_Mgr.Instance.PlayGUISound("3. click", 1.0f);
    }
    public void BGM_SliderChange(float value)
    {   //value 0.0 ~ 1.0 //�����Ƶ� ���� ����
        if (BGM_Slider != null)
        {
            PlayerPrefs.SetFloat("SoundVolume", value);
            Sound_Mgr.Instance.SoundVolume(value);
        }

    }


}
