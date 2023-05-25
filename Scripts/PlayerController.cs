using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 9f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    //Crop Object
    public List<GameObject> FoundObjects;
    [HideInInspector] public GameObject a_crop;
    public float shortDis;
    //Crop Object

    public Text m_ItemCount = null;
    float m_range = 1.0f;

    Rigidbody Rigidbodyrb;
    GameObject collideObject;
    Transform m_tfTarget = null;


    //Meal 관련 변수
    bool SaveMeal = false;
    public RawImage Meal_Image = null;
    //Booster 
    [HideInInspector] public bool working = false;


    Animator animator;
    public Text Warning_Text = null;
    float alpha = 0.0f;//투명도 변화 속도
    float waitTime = 1.0f;

    public int cropNumSum = 0;
    public bool StateWork = false;
    public bool isFull = false;
    public static PlayerController Inst;
    void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Sound_Mgr.Instance.PlayBGM("4. game bgm");
        Application.targetFrameRate = 60;
        //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        SaveMeal = false;
        Rigidbodyrb = GetComponent<Rigidbody>();

        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        m_ItemCount.text = "x " + cropNumSum.ToString();

        FindCrop();
        inputcheck();
        checkIsFull();

        if (cropNumSum >= 20 && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            WarningDirect();
            waitTime = 1.0f;
            return;
        }



    }
    void inputcheck()
    {
        //좌우 반전 함수
        int key = 0;
        
        if (Input.GetKey(KeyCode.W))
        {   // y축 1.6 x축 -7 y축 -4.7 x축 7
            transform.position += (new Vector3(0, moveSpeed * Time.deltaTime, 0));
            Vector3 pos = transform.position;
            if (pos.y >= 1.6f) pos.y = 1.6f;
            transform.position = pos;
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (new Vector3(0, -moveSpeed * Time.deltaTime, 0));
            Vector3 pos = transform.position;
            if (pos.y <= -4.7f) pos.y = -4.7f;
            transform.position = pos;
            if (transform.position.y >= -4.7f)
                animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            key = -1;
            transform.position += (new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            Vector3 pos = transform.position;
            if (pos.x <= -7.0f) pos.x = -7.0f;
            transform.position = pos;
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            key = 1;
            transform.position += (new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            Vector3 pos = transform.position;
            if (pos.x >= 7.0f) pos.x = 7.0f;
            transform.position = pos;
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sound_Mgr.Instance.PlayEffSound("6. oat_get", 1.0f);
            animator.SetBool("Work", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Work", false);
        }


        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

    }
 

    void OnTriggerEnter(Collider other)
    {

        collideObject = other.gameObject;

        if (other.tag == "CowMeal")
        {
            if (SaveMeal == true)
                return;
            else
            {
                SaveMeal = true;
                Meal_Image.gameObject.SetActive(SaveMeal);
            }
        }
        if (other.tag == "Cow")
        {
            if (SaveMeal == false)
                return;
            else
            {
                Sound_Mgr.Instance.PlayEffSound("10. cow_howl", 1.0f);
                SaveMeal = false;
                Meal_Image.gameObject.SetActive(SaveMeal);
                CowManger.Inst.EatItem();
            }

        }


    }// void OnTriggerEnter(Collider other)

    void FindCrop()
    {

        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Crop"));
        if (FoundObjects.Count <= 0)
            return;
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);

        a_crop = FoundObjects[0];//첫번째를 먼저
        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
            if (Distance < shortDis)
            {
                a_crop = found;
                shortDis = Distance;
            }
        }
    }

    void WarningDirect()
    {

        if (Warning_Text.color.a >= 1.0f)
            alpha = -6.0f;
        else if (Warning_Text.color.a <= 0.0f)
            alpha = 6.0f;

        Warning_Text.color = new Color(255.0f, 0.0f, 0.0f, Warning_Text.color.a + alpha * Time.deltaTime);
    }

    public bool checkIsFull()
    {
        if (cropNumSum >= 20)
        {
            isFull = true;
            
        }
        else
        {
            isFull = false;
            moveSpeed = 9;
        }
        return isFull;
    }//public bool checkIsFull()


}
