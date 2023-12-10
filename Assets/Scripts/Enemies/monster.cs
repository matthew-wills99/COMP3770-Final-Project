using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class monster : MonoBehaviour
{
    private Transform bosstarget;   // ����׷��Ŀ���λ��
    public float bossAIMaxHP;  // �������ֵ
    public Slider bossAISli;   // Ѫ��
    public float bossAICurrentHP;  // ��ǰ����ֵ
    public float bossguaiwujingyan;
    new private Rigidbody2D rigidbody;

    public float dis;  // ������Ŀ��ľ���
    private Animator bossanimator;
    public float speed;
    private float speed1;
    // public GameObject panl;
    public float guaiwugongji;  // ���﹥����
    public Collider2D coll;  // ��ɫ��ײ��
    public LayerMask ground;  // ����ͼ��
    private bool shoushang;  // �Ƿ�����
    private float intt;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bossanimator = GetComponent<Animator>();  // ��ȡ�������
        bosstarget = GameObject.FindWithTag("Player").transform;  // ��ȡ��Ϸ�����ǵ�λ�ã����ҵĹ����������ǵı�ǩ��Player
        bossAICurrentHP = bossAIMaxHP;  // ����ֵ��ʼ��

        bossanimator.Play("boss");  // ����boss����
    }

    void Update()
    {
        bossShowHPSlider();  // ����Ѫ����ʾ
        if (shoushang == true)
        {
            bossmoveinging();  // ���˺���ƶ�
        }
        if (shoushang == false)
        {
            bossmoveing();  // �����ƶ�
        }
        if (!coll.IsTouchingLayers(ground))  // �Ƿ�վ�ڵ�����
        {
            rigidbody.gravityScale = 50f;  // ʹ����������Ӱ��
        }
        else
        {
            rigidbody.gravityScale = 1f;  // ȡ������Ӱ��
            //y�����������Ծ
        }
    }

    public void bossmoveinging()
    {
        if (dis <= 300 && dis >= 1)  // �����Ŀ��ľ�����1��300֮��
        {
            if (intt < 0)
            {
                rigidbody.velocity = new Vector2(speed, speed1);  // �����ƶ�
                bossanimator.Play("bosswalkright");  // ���������ƶ��Ķ���
            }
            if (intt > 0)
            {
                rigidbody.velocity = new Vector2(-speed, speed1);  // �����ƶ�
                bossanimator.Play("bosswalkleft");  // ���������ƶ��Ķ���
            }
        }
    }

    public void bossmoveing()
    {
        dis = (transform.position - bosstarget.position).sqrMagnitude;  // ��ȡ��Ŀ��ľ����ƽ��
        intt = transform.position.x - bosstarget.position.x;  // ��ȡ��Ŀ���x�����ֵ

        if (dis >= 30)  // �����Ŀ��ľ�����ڵ���30
        {
            bossanimator.Play("boss");  // ���ž�ֹ����
            rigidbody.velocity = new Vector2(speed * 0, speed1);  // ֹͣ�ƶ�
        }

        if (dis <= 30 && dis >= 3)  // �����Ŀ��ľ�����3��30֮��
        {
            if (intt < 0)
            {
                rigidbody.velocity = new Vector2(speed, speed1);  // �����ƶ�
                bossanimator.Play("bosswalkright");  // ���������ƶ��Ķ���
            }
            if (intt > 0)
            {
                rigidbody.velocity = new Vector2(-speed, speed1);  // �����ƶ�
                bossanimator.Play("bosswalkleft");  // ���������ƶ��Ķ���
            }
        }

        if (dis <= 3)  // �����Ŀ��ľ���С�ڵ���3
        {
            if (intt < 0)
            {
                rigidbody.velocity = new Vector2(speed * 0, speed1);  // ֹͣ�ƶ�
                bossanimator.Play("bossattackright");  // �������ҹ����Ķ���
            }
            if (intt > 0)
            {
                rigidbody.velocity = new Vector2(speed * 0, speed1);  // ֹͣ�ƶ�
                bossanimator.Play("bossattackleft");  // �������󹥻��Ķ���
            }
        }
    }

    public void TakeDamage(float damage)//��Ѫ
    {
        bossAICurrentHP -= damage;  // �۳�����ֵ
        shoushang = true;  // ��������״̬Ϊtrue
        Invoke("shoushangfalse", 3f);  // ����3���ȡ������״̬
        if (bossAICurrentHP <= 0)  // �������ֵС�ڵ���0
        {
            bossAICurrentHP = 0;  // ����ֵ��Ϊ0
            //animator.SetBool("AIisdeath", true);
            //nisile.SetActive(true);
            //this.transform.tag = "siwang";
            bossAIxiaohui();  // ���ù�����ʧ�ĺ���
        }
    }

    public void shoushangfalse()  // ȡ������״̬
    {
        if (dis >= 30)
        {
            shoushang = false;
        }
    }

    public void bossShowHPSlider()  // ����Ѫ����ʾ
    {
        bossAISli.value = bossAICurrentHP;
    }
        //����Ѫû�˵��������
    public void bossAIxiaohui()  // ������ʧ
    {
    
        Destroy(gameObject);  // ���ٹ������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  // �����������ײ
        {
         // ���ǵ�Ѫ
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")  // ��������ǽ�����ײ
        {
            // ...
        }
    }
}
