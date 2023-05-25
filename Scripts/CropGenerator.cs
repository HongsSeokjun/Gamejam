using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpawnPos
{
    public Vector3 m_Pos = Vector3.zero;
    public float m_SpDelay = 0.0f;
    public int m_Level = 1;
    public SpawnPos()
    {
        m_SpDelay = 0.0f;
    }

    public bool Update_SpPos(float a_DeltaTime)
    {
        if (0.0f < m_SpDelay)
        {
            m_SpDelay -= a_DeltaTime;
            if (m_SpDelay <= 0.0f)
            {
                //�޸�Ǯ���� ��� ������Ű�� ����ʿ� ���� ��ġ �ε����� �����Ѵ�.
                m_SpDelay = 0.0f;
                return true;
            }
        }// if(0.0f < m_SpDelay)

        return false;
    }//public bool Update_SpPos(float  a_DeltaTime)
}


public class CropGenerator : MonoBehaviour
{   
    //��� ���� ��ġ
    Transform m_CropPos = null;
    GameObject m_CropPrefab = null;
    List<SpawnPos> m_SpawnPosList = new List<SpawnPos>();
    public Texture[] m_CropImg = null;


    public static CropGenerator Inst = null;

    //List<Crop> m_CropList = new List<Crop>();
    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {


        m_CropPos = this.transform;
        m_CropPrefab = Resources.Load("CropPrefab") as GameObject;
        CropHandler[] m_CropList;
        m_CropList = this.transform.GetComponentsInChildren<CropHandler>();
        //Crop�� �پ��ִ� �ֵ鸸 ã��
        for (int ii = 0; ii < m_CropList.Length; ii++)
        {
            SpawnPos a_Node = new SpawnPos();
            a_Node.m_Pos = m_CropList[ii].gameObject.transform.position;
            m_CropList[ii].m_SpawnIdx = ii; //m_SpawnPos�� �ε��� ������ ���ش�.
            m_SpawnPosList.Add(a_Node);

        }//for(int ii = 0; ii< m_CropList.Length; ii++)

    }

    // Update is called once per frame
    void Update()
    {
        for (int ii = 0; ii < m_SpawnPosList.Count; ii++)
        {
            if (m_SpawnPosList[ii].Update_SpPos(Time.deltaTime) == false)
                continue;
            //���� ���� ��Ų��.
            GameObject newObj = (GameObject)Instantiate(m_CropPrefab);
            newObj.transform.SetParent(m_CropPos);
            newObj.transform.position = m_SpawnPosList[ii].m_Pos;
            int a_Lv = m_SpawnPosList[ii].m_Level;
            newObj.GetComponent<CropHandler>().SetSpawnInfo(ii, a_Lv);
        }
    }

    public void ReSetSpawn(int idx)
    {
        if (idx < 0)
            return;
        m_SpawnPosList[idx].m_Level++;
        m_SpawnPosList[idx].m_SpDelay = Random.Range(1.0f, 4.0f);
    }



}
