using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study00 : MonoBehaviour
{
    // 컬렉션 종류 : ArrayList List<t>, List, Queue, Stack, HashTable, Dictionary (map)

    // C++ 배열 = int bullet[25];
    // C# 배열 = int [] = new int[25];

    ArrayList arrList = new ArrayList();
    List<int> list = new List<int>();
    Hashtable hashtable = new Hashtable();
    Dictionary<string, int> map = new Dictionary<string, int>();

    // 박싱, 언박싱 > 
    // ArrayList 타입 상관없이 자료를 담을 수 있는데
    // 매게변수를 object형으로 받고 있어서
    // 애초에 메모리 공간 자첼를 박스 형태로 가지고 있다.
    // Add()할때 박싱처리가 되어서 저장이되고
    // 내부요소에 접근에서 처리할 때 언박싱 과정을 거침
    // 호출 될 때마다 박싱, 언박싱을 하기 때문에 데이터가 많으면
    // 성능저하가 엄~청 일어난다.
    // 결론은 뭐다? 안쓴다.

    void Start()
    {
        arrList.Add(10);
        arrList.Add(3.14f);
        arrList.Add("삼각김밥");
        // 정수, 실수, 문자열 상관없이 다 들어가는 매직

        arrList.Insert(1, "춘춘이는 오늘도 뚠둔");

        //arrList.Remove(1);

        arrList.RemoveRange(1, 3); // 1번 인덱스부터 3개를 지우겠다

        hashtable.Add("춘춘", 33);
        hashtable.Add("종혁", 70);

        // hashtable은 무조건 키값으로 출력해야한다!
        print(hashtable["춘춘"]);

        // 아래는 실행해보면 null값이 나온다
        print(hashtable[99]);
    }

    void Update()
    {
        
    }
}
