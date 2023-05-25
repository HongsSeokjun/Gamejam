using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CropHandler : MonoBehaviour
{

    GameObject m_Target = null;
    //List<SpawnPos> m_SpawnPosList; 의 인덱스
    [HideInInspector] public int m_SpawnIdx = -1;

    int m_Level = 1;
    public Texture m_MonImg = null;

    CropHandler CropIndex;

    public static CropHandler Inst;
    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Inst.a_crop == null)
            return;
        CropIndex = PlayerController.Inst.a_crop.GetComponent<CropHandler>();



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerController.Inst.working == true && PlayerController.Inst.isFull == false)
            {
                PlayerController.Inst.working = false;


                if (CropGenerator.Inst != null)
                  CropGenerator.Inst.ReSetSpawn(CropIndex.m_SpawnIdx);

                PlayerController.Inst.cropNumSum++;
                Destroy(PlayerController.Inst.a_crop);


            }

        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {


            PlayerController.Inst.working = true;
           

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Inst.working = false;

        }
    }

    public void SetSpawnInfo(int Idx, int a_Lv)
    {
        m_SpawnIdx = Idx;
        m_Level = a_Lv;

    }

    public void checkIsFull()
    {

    }

}
