using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Clear_Mgr : MonoBehaviour
{
    public Button TitleGo_Btn = null;
    public Text BestScore = null;
    public Text CurScore = null;
    int m_CurScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        Sound_Mgr.Instance.PlayBGM("9. end scene bgm",0.4f);
        Maket_Mgr.Load();
        m_CurScore = PlayerPrefs.GetInt("CurScore",0);
        
        BestScore.text = Maket_Mgr.BestScore.ToString();
        CurScore.text = m_CurScore.ToString();


        if (TitleGo_Btn != null)
            TitleGo_Btn.onClick.AddListener(() => {
                SceneManager.LoadScene("TitleScene");
            });
    }

    // Update is called once per frame
    void Update()
    {

    }

}
