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

    private GameObject bottleLiquid;                        //Liquid ui ����
    private int fpsNum;

    // Start is called before the first frame update
    void Start()
    {
        bottleLiquid = Resources.Load("BottleLiquid") as GameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PouringLiquid();
        PlusFpsNum();
        LiquidMotion();
    }

    public void PlusFpsNum()
    {
        if (bottleTop.position.y < bottleBottom.position.y)
            fpsNum++;
        else fpsNum = 0;
    }

    public void LiquidMotion()
    {
        //isPoured true�϶� �ʴ� 6�� Liquid����� ����
        if (IsPoured() && fpsNum % 10 == 0 && fpsNum != 0)
        {
            GameObject prefab_obj = Instantiate(bottleLiquid);
            prefab_obj.name = "liquidUI";
            Vector3 pos = new Vector3(bottleTop.position.x, bottleTop.position.y - 0.3f, bottleTop.position.z);
            prefab_obj.transform.position = pos;
            Destroy(prefab_obj, 1.0f);
        }
    }

    /*
     * ���� ���� �������� �ִ� �� �Ǵ� ����
     * Top y�� Bottom y���� ���� �� true
     */
    public bool IsPoured()
    {
        if (bottleTop.position.y > bottleBottom.position.y)
            return false;
        else
            return true;
    }

    public void PouringLiquid()
    {
        if (IsPoured())
        {
            //z�� �����̼� �� = ���� ���� ��(0 ~ 90)
            float zRot = Mathf.Abs(Mathf.Abs(transform.localEulerAngles.z - 180) - 90);
            //������, �� �信�� ����ĳ��Ʈ�� �ð������� ������ (�ΰ��ӿ��� �Ⱥ���)
            Debug.DrawRay(bottleTop.position, Vector3.down * rayDist, Color.blue);
            if (Physics.Raycast(bottleTop.position, Vector3.down, out hit, rayDist))
            {
                //����ĳ��Ʈ�� �浿�� ��ü�� �±װ� Cup�� ���
                if (hit.transform.gameObject.CompareTag("Cup"))
                {
                    //������� ȯ��, �ʴ� 1 ~ 10��ŭ ä����
                    hit.transform.gameObject.GetComponent<CupCtrl>().AddReceipt(bottleType, zRot / 90 * 10);
                }
            }
        }
    }
}
