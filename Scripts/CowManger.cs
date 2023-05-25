using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CowManger : MonoBehaviour
{
    public Image CurHP_bar = null;
    [HideInInspector] public float m_MaxHP = 100.0f;
    [HideInInspector] public float m_CurHP = 100.0f;
    public GameObject GameOverPanel = null;
    public float HpSpeed = 5.0f;
    public Button GoTitle_Btn = null;
    public Button Exit_Btn = null;

    public static CowManger Inst;
    void Awake()
    {
        Inst = this;   
    }


    // Start is called before the first frame update
    void Start()
    {
        if (Exit_Btn != null)
            Exit_Btn.onClick.AddListener(() => {
                Application.Quit();
            });
        if (GoTitle_Btn != null)
            GoTitle_Btn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("TitleScene");
            });
        m_CurHP = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
  
       //생명 수치에 따라 Filled 이미지의 색상 변경
       if (CurHP_bar.fillAmount > 0.6f)
                CurHP_bar.color = Color.green;
       if (CurHP_bar.fillAmount <= 0.6f)
                CurHP_bar.color = Color.yellow;
       if (CurHP_bar.fillAmount <= 0.3f)
                CurHP_bar.color = Color.red;
       

    }

    public void TakeDamage()
    {   //소의 체력감소
        m_CurHP -= Time.deltaTime * HpSpeed;

        if (m_CurHP <= 0)
        {
            m_CurHP = 0.0f;
            GameOverPanel.gameObject.SetActive(true);
            Sound_Mgr.Instance.PlayBGM("9. end scene bgm");
            Time.timeScale = 0.0f;
        }
        if (CurHP_bar != null)
            CurHP_bar.fillAmount = m_CurHP / m_MaxHP;
        //소의 체력감소
    }

    public void EatItem()
    {   //여물을 받으면
        m_CurHP += 50.0f;
        if (m_CurHP >= 100.0f)
            m_CurHP = 100.0f;
        if (CurHP_bar != null)
            CurHP_bar.fillAmount = m_CurHP / m_MaxHP;
        //여물을 받으면
    }
}
