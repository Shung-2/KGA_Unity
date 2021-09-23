using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girl : MonoBehaviour
{
    List<GameObject> myHairs   = new List<GameObject>();
    List<GameObject> myUppers  = new List<GameObject>();
    List<GameObject> myBottoms = new List<GameObject>();

    Transform hairGroup;
    Transform upperGroup;
    Transform bottomGroup;

    public int hairNum = 0;
    public int upperNum = 0;
    public int bottomNum = 0;

    private void Awake()
    {
        hairGroup = transform.Find("hairGroup");
        upperGroup = transform.Find("upbodyGroup");
        bottomGroup = transform.Find("downBodyGroup");
    }

    private void OnEnable()
    {
        // 해당 컴포넌트가 활성화 될 때 동작
    }

    void Start()
    {
        // 달걀귀신을 만들어줍시다. :)
        MakeDresse(hairGroup, myHairs);
        MakeDresse(upperGroup, myUppers);
        MakeDresse(bottomGroup, myBottoms);

        if(PlayerPrefs.HasKey("hair"))
        {
            LoadDresses();
        }
        else InitDresses();
    }

    void InitDresses()
    {
        // 헤어, 상, 하의 세팅!
        ShowDress(myHairs, hairNum);
        ShowDress(myUppers, upperNum);
        ShowDress(myBottoms, bottomNum);
    }

    public void SaveCuurrentDresses()
    {
        PlayerPrefs.SetInt("hair", hairNum);
        PlayerPrefs.SetInt("upper", upperNum);
        PlayerPrefs.SetInt("bottom", bottomNum);
    }

    void LoadDresses()
    {
        hairNum = PlayerPrefs.GetInt("hair");
        upperNum = PlayerPrefs.GetInt("upper");
        bottomNum = PlayerPrefs.GetInt("bottom");

        InitDresses();
    }

    public void ChangeHair()
    {
        hairNum++;

        if (hairNum > myHairs.Count - 1) hairNum = 0;

        ShowDress(myHairs, hairNum);
    }

    public void ChaneUpper()
    {
        upperNum++;
        
        if (upperNum > myUppers.Count - 1) upperNum = 0;

        ShowDress(myUppers, upperNum);
    }

    public void ChangeButtom()
    {
        bottomNum++;

        if (bottomNum > myBottoms.Count - 1) bottomNum = 0;

        ShowDress(myBottoms, bottomNum);
    }

    void MakeDresse(Transform dressGroup, List<GameObject> dressList)
    {
        foreach(Transform dress in dressGroup)
        {
            dressList.Add(dress.gameObject);
            dress.gameObject.SetActive(false);
        }
    }

    void ShowDress(List<GameObject> dreeList, int dressNum)
    {
        // Count == STL에서 Size()와 같다.
        for (int i = 0 ; i < dreeList.Count; ++i)
        {
            dreeList[i].SetActive(false);
        }

        dreeList[dressNum].SetActive(true);
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        // Update이후의 돌아가는 Update
    }

    private void FixedUpdate()
    {
        // 주기적으로 도는 물리 Update
        // 기본 호출타음을 보면 고정시간마다 호출된다.
        // ProjectSetting > time 0.02
        // Time.deltaTime을 안써도된다.
    }

    private void OnDisable() 
    {
        // 해당 컴포넌트가 비활성화 될때
    }

    private void OnDestroy() 
    {
        // 해당 컴포넌트가 부수어질때
    }

    private void OnBecameInvisible()
    {
        // 화면에 표시되지 않게 되었을 때   
    }

    private void OnBecameVisible()
    {
        // 화면에 표시됐을 때
    }

    private void OnDrawGizmos()
    {
        // 기즈모에 표시가 될 때
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(1, 1, 1));
        Gizmos.DrawCube(this.transform.position, new Vector3(1, 1, 1));
    }

    private void OnGUI()
    {
        // GUI에 표시가 될 때
    }
}
