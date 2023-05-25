using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] LevelManager levelManager;
    [SerializeField] float totalTime = 120f;
    public Transform Sky = null;
    public Image Moon = null;


    float timerValue;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        timerValue = totalTime;
    }
    void Update()
    {
        UpdateTimer();
        Sky.transform.position = new Vector3(Sky.transform.position.x - Time.deltaTime * 0.55f, Sky.transform.position.y, Sky.transform.position.z);
        //Sky.transform.position = new Vector3(Sky.transform.position.x, Sky.transform.position.y + Time.deltaTime * 0.05f, Sky.transform.position.z);
        Moon.transform.position = new Vector3(Moon.transform.position.x - Time.deltaTime * 1.7f, Moon.transform.position.y, Moon.transform.position.z);
        // Moon.transform.position = new Vector3(Moon.transform.position.x - Time.deltaTime *0.58f , Moon.transform.position.y, Moon.transform.position.z);
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (timerValue < 0f)
        {
            SceneManager.LoadScene("Clear");
        }
        timeText.text = ((int)timerValue).ToString();
    }
}
