using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public float HP = 20;
    NavMeshAgent agent;
    Animator ani;
    float juli;
    public Transform player;
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        ani= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()

    {
        if (HP<=0)//血量0时，播放死亡动画
        {
            agent.enabled = false;
            ani.SetBool("Die", true);
        }


        // 一定位置内跟随过去
        if (Vector3.Distance(transform.position, player.position) >= 2.5f && Vector3.Distance(transform.position, player.position) <= 10f)
        {
            agent.enabled = true;
            agent.destination = player.position;
            ani.SetBool("Run", true);
        }
        else//超过范围 停止跟随，并播放待机动画
        {
            transform.LookAt(player.position);  
            agent.enabled = false;
            ani.SetBool("Run", false);
        }
        //靠近目标时，停止导航，播放攻击动画
        if (Vector3.Distance(transform.position, player.position) <= 2.5f) 
        {
           
           // ani.SetBool("Run", false);
            ani.SetTrigger("Att");
        }

    }
    public void TakeDamage(float damage=5f) //改方法 每次调用减20血量
    {
        HP -= damage;
    }
}
