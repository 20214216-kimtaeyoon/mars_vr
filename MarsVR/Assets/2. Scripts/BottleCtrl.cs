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
    private Liquid liquid = null;
    public GameObject liquidPrefab;

    private bool isPouring = false;
    private GameObject bottleLiquid;                        //Liquid ui ����
    private int fpsNum;

    // Start is called before the first frame update
    void Start()
    {
        //bottleLiquid = Resources.Load("BottleLiquid") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        PouringLiquid();
        //MakeLiquid();
        //PlusFpsNum();
        //LiquidMotion();

        //�� ���� (IsPoured()�� ���� �ٲ� ������ ȣ���
        if(isPouring != IsPoured())
        {
            isPouring = IsPoured();
            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }

    }

    private void StartPour()
    {
        liquid = CreateLiquid();
        liquid.Begin();
    }
    private void EndPour()
    {
        liquid.End();
        liquid = null;
    }

    private Liquid CreateLiquid()
    {
        GameObject temp = Instantiate(liquidPrefab, bottleTop.position, Quaternion.identity, transform);
        return temp.GetComponent<Liquid>();
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
            //�� ��(x��, z��)�� �����̼� ��, �� ���� ������ ����� �� �� 0 -> 10���� ���� �������
            float xRot = (transform.eulerAngles.x < 180 ? 90 - transform.eulerAngles.x : transform.eulerAngles.x - 270) / 90 * 10;
            float zRot = Mathf.Abs(Mathf.Abs(transform.localEulerAngles.z - 180) - 90) / 90 * 10;

            //�� ���� ���� �� �������� �������(0 ~ 100�� ���� 0 ~ 10���� �������)
            float amount = Mathf.Sqrt(xRot * zRot);
            //������, �� �信�� ����ĳ��Ʈ�� �ð������� ������ (�ΰ��ӿ��� �Ⱥ���)
            Debug.DrawRay(bottleTop.position, Vector3.down * rayDist, Color.blue);


            if (Physics.Raycast(bottleTop.position, Vector3.down, out hit, rayDist))
            {
                //����ĳ��Ʈ�� �浿�� ��ü�� �±װ� Cup�� ���
                if (hit.transform.gameObject.CompareTag("Cup"))
                {
                    //������� ȯ��, �ʴ� 0 ~ 10��ŭ ä����
                    hit.transform.gameObject.GetComponent<CupCtrl>().AddReceipt(bottleType, amount);
                }
            }
        }
    }
}
