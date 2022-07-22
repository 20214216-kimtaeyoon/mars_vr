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

    [Header("Bottle Type ���� (Ÿ��, etc...)")]
    public BottleType bottleType;
    public Transform bottleTop;                     //�� �Ա� (���⿡�� �״� ����Ʈ �־��ֱ�)
    public Transform bottleBottom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.LogWarning("���� ����������? : " + isPoured());
    }

    /*
     * ���� ���� �������� �ִ� �� �Ǵ� ����
     * Top y�� Bottom y���� ���� �� true
     */
    public bool isPoured()
    {
        Debug.Log(bottleTop.position.y+", "+ bottleBottom.position.y);
        if (bottleTop.position.y > bottleBottom.position.y)
            return false;
        else
            return true;
    }
}
