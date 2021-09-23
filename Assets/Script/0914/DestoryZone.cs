using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name.Contains("Enemy"))
        {
            other.gameObject.SetActive(false);
        }

        // 1. 배열 오브젝트 풀링 
        /*
        if (other.gameObject.name.Contains("Bullet"))
        {
            other.gameObject.SetActive(false);
            PlayerFire0914 pf = GameObject.Find("Player").GetComponent<PlayerFire0914>();
            pf.bulletPool.Add(other.gameObject);
        }
        */

        // 2. 리스트 오브젝트 풀링 + 레이어 마스크를 이용한 방법
        /*
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            other.gameObject.SetActive(false);
            PlayerFire0914 pf = GameObject.Find("Player").GetComponent<PlayerFire0914>();
            pf.bulletPool.Add(other.gameObject);
        }
        */

        // 3. 큐 오브젝트 풀링 + 레이어 마스크를 이용한 방법
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            other.gameObject.SetActive(false);
            PlayerFire0914 pf = GameObject.Find("Player").GetComponent<PlayerFire0914>();
            pf.bulletPool.Enqueue(other.gameObject);
        }
    }
}