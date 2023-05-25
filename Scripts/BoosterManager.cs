using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoosterManager : MonoBehaviour
{   
    //새참 스폰주기 변수
    int spawnMinTime = 5;
    int spawnMaxTime = 10;
    //새참 시간 변수
    float activeTime = 5f;
    float noticeTime = 2f;
    float boosterOnTime = 5f;
    //새참 속도 변수
    [SerializeField] float boosterSpeed = 12.0f;
    [SerializeField] float originalSpeed;

    PlayerController playerController;
    public bool isBoosterExists = false;
    public bool Booster = false;
    bool cycleFinished = false;
    Coroutine spawnCoroutine;

    public GameObject noticeImage;

    public static BoosterManager Inst = null;

    void Awake()
    {
        Inst = this;
        playerController = FindObjectOfType<PlayerController>();
        originalSpeed = playerController.MoveSpeed;
    }
     void Update()
    {
        if(Booster == true)
        playerController.MoveSpeed = boosterSpeed;
        else
            playerController.MoveSpeed = originalSpeed;

        if (PlayerController.Inst.cropNumSum >= 20&& Booster ==false)
        {
            playerController.MoveSpeed = 4.0f;
        }

        spawnBooster();
    }
    public void spawnBooster()
    {
        //booster does not exists and spawnCoroutine not start yet
        if (!isBoosterExists && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(boosterContinuously(spawnMinTime, spawnMaxTime, activeTime, noticeTime));
        }
        if (cycleFinished)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
            cycleFinished = false;
        }

    }
    IEnumerator boosterContinuously(int spawnMin, int spawnMax, float activeDelay, float noticeDuration)
    {
        int spawnRand = Random.Range(spawnMin, spawnMax);
        yield return new WaitForSeconds(spawnRand);
        isBoosterExists = true;
        noticeImage.SetActive(true);

        yield return new WaitForSeconds(activeDelay);
        noticeImage.SetActive(false);
        isBoosterExists = false;
        cycleFinished = true;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isBoosterExists == true)
        {
            Booster = true;
            isBoosterExists = false;
            StartCoroutine(playerBoosted());
        }
    }
    IEnumerator playerBoosted()
    {
        yield return new WaitForSeconds(boosterOnTime);
        Booster = false;
        
    }
}
