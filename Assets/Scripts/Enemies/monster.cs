using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class monster : MonoBehaviour
{
    private Transform bosstarget;   // 设置追踪目标的位置
    public float bossAIMaxHP;  // 最大生命值
    public Slider bossAISli;   // 血条
    public float bossAICurrentHP;  // 当前生命值
    public float bossguaiwujingyan;
    new private Rigidbody2D rigidbody;

    public float dis;  // 怪物与目标的距离
    private Animator bossanimator;
    public float speed;
    private float speed1;
    // public GameObject panl;
    public float guaiwugongji;  // 怪物攻击力
    public Collider2D coll;  // 角色碰撞器
    public LayerMask ground;  // 地面图层
    private bool shoushang;  // 是否受伤
    private float intt;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bossanimator = GetComponent<Animator>();  // 获取动画组件
        bosstarget = GameObject.FindWithTag("Player").transform;  // 获取游戏中主角的位置，在我的工程里面主角的标签是Player
        bossAICurrentHP = bossAIMaxHP;  // 生命值初始化

        bossanimator.Play("boss");  // 播放boss动画
    }

    void Update()
    {
        bossShowHPSlider();  // 更新血条显示
        if (shoushang == true)
        {
            bossmoveinging();  // 受伤后的移动
        }
        if (shoushang == false)
        {
            bossmoveing();  // 正常移动
        }
        if (!coll.IsTouchingLayers(ground))  // 是否站在地面上
        {
            rigidbody.gravityScale = 50f;  // 使怪物受重力影响
        }
        else
        {
            rigidbody.gravityScale = 1f;  // 取消重力影响
            //y轴刚体受力跳跃
        }
    }

    public void bossmoveinging()
    {
        if (dis <= 300 && dis >= 1)  // 如果与目标的距离在1到300之间
        {
            if (intt < 0)
            {
                rigidbody.velocity = new Vector2(speed, speed1);  // 向右移动
                bossanimator.Play("bosswalkright");  // 播放向右移动的动画
            }
            if (intt > 0)
            {
                rigidbody.velocity = new Vector2(-speed, speed1);  // 向左移动
                bossanimator.Play("bosswalkleft");  // 播放向左移动的动画
            }
        }
    }

    public void bossmoveing()
    {
        dis = (transform.position - bosstarget.position).sqrMagnitude;  // 获取与目标的距离的平方
        intt = transform.position.x - bosstarget.position.x;  // 获取与目标的x坐标差值

        if (dis >= 30)  // 如果与目标的距离大于等于30
        {
            bossanimator.Play("boss");  // 播放静止动画
            rigidbody.velocity = new Vector2(speed * 0, speed1);  // 停止移动
        }

        if (dis <= 30 && dis >= 3)  // 如果与目标的距离在3到30之间
        {
            if (intt < 0)
            {
                rigidbody.velocity = new Vector2(speed, speed1);  // 向右移动
                bossanimator.Play("bosswalkright");  // 播放向右移动的动画
            }
            if (intt > 0)
            {
                rigidbody.velocity = new Vector2(-speed, speed1);  // 向左移动
                bossanimator.Play("bosswalkleft");  // 播放向左移动的动画
            }
        }

        if (dis <= 3)  // 如果与目标的距离小于等于3
        {
            if (intt < 0)
            {
                rigidbody.velocity = new Vector2(speed * 0, speed1);  // 停止移动
                bossanimator.Play("bossattackright");  // 播放向右攻击的动画
            }
            if (intt > 0)
            {
                rigidbody.velocity = new Vector2(speed * 0, speed1);  // 停止移动
                bossanimator.Play("bossattackleft");  // 播放向左攻击的动画
            }
        }
    }

    public void TakeDamage(float damage)//减血
    {
        bossAICurrentHP -= damage;  // 扣除生命值
        shoushang = true;  // 设置受伤状态为true
        Invoke("shoushangfalse", 3f);  // 经过3秒后取消受伤状态
        if (bossAICurrentHP <= 0)  // 如果生命值小于等于0
        {
            bossAICurrentHP = 0;  // 生命值置为0
            //animator.SetBool("AIisdeath", true);
            //nisile.SetActive(true);
            //this.transform.tag = "siwang";
            bossAIxiaohui();  // 调用怪物消失的函数
        }
    }

    public void shoushangfalse()  // 取消受伤状态
    {
        if (dis >= 30)
        {
            shoushang = false;
        }
    }

    public void bossShowHPSlider()  // 更新血条显示
    {
        bossAISli.value = bossAICurrentHP;
    }
        //怪物血没了的情况调用
    public void bossAIxiaohui()  // 怪物消失
    {
    
        Destroy(gameObject);  // 销毁怪物对象
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  // 如果与主角碰撞
        {
         // 主角掉血
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")  // 如果与主角结束碰撞
        {
            // ...
        }
    }
}
