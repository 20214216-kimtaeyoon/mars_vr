using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ������ ��Ģ��
    Class = �Ľ�Į
    Method = �Ľ�Į
    Variable = ī��
    ���ϸ� = �Ľ�Į

    �� BottleCtrl ��ü�� ������ BottleType�� ������ ����.
    Bottle ��ü�� �Ա����� RayCast�� �Ͽ� Cup Bottom�� �ݶ��̴��� ���� ��� �ſ� ������ ������ �Ǵ�.
    Cup ��ü�� ���� �� CupCtrl�� receipt ����Ʈ�� �߰�
    �� receipt �ʵ带 ���� ���ϴ� ������ ����

 */

public class BottleCtrl : MonoBehaviour
{
    public enum BottleType
    {
        NONE,
        BOTTLETYPE1,
        BOTTLETYPE2,
        BOTTLETYPE3,
    }

    public bool isUsing;                            //���� ��� ���� ������
    [Header("Bottle Type ���� (Ÿ��, etc...)")]
    public BottleType bottleType;
    [Header("Raycast")]
    public Transform bottleTop;                     //�� �Ա� (���⿡�� �״� ����Ʈ �־��ֱ�)
    public Transform bottleBottom;
    [Range(1f, 10f)]
    public float rayDist;                           //����ĳ��Ʈ ��Ÿ�
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PouringLiquid();
    }

    /*
     * ���� ���� �������� �ִ� �� �Ǵ� ����
     * Top y�� Bottom y���� ���� �� true
     */
    public bool IsPoured()
    {
        Debug.Log(bottleTop.position.y+", "+ bottleBottom.position.y);
        if (bottleTop.position.y > bottleBottom.position.y)
            return false;
        else
            return true;
    }

    public void PouringLiquid()
    {
        if (IsPoured())
        {
            //������, �� �信�� ����ĳ��Ʈ�� �ð������� ������ (�ΰ��ӿ��� �Ⱥ���)
            Debug.DrawRay(bottleTop.position, Vector3.down * rayDist, Color.blue);
            if (Physics.Raycast(bottleTop.position, Vector3.down, out hit, rayDist))
            {
                //����ĳ��Ʈ�� �浿�� ��ü�� �±װ� Cup�� ���
                if (hit.transform.gameObject.CompareTag("Cup"))
                {
                    Debug.LogWarning("�� ������!");
                    hit.transform.gameObject.GetComponent<CupCtrl>().AddReceipt(bottleType);
                }
            }
        }
    }
}
