using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupCtrl : MonoBehaviour
{
    //���ǰ� ������
    public bool isUsing;

    [Header("������ ����")]
    //������ ���
    public List<BottleCtrl.BottleType> receipt;
    //������ ��
    public List<float> amounts;

    public GameObject bottle;
    public float bottleRotationX;
    public Vector3 bottleAngles;

    [Header("UI ����")]
    //MLǥ��(UI)
    public Transform uiPos;
    public GameObject amountUI;
    //amounts int��
    int newAmounts;

    // Start is called before the first frame update
    void Start()
    {
        bottle = GameObject.Find("Bottle");
        //ML = GameObject.Find("Canvas/ML");
    }

    // Update is called once per frame
    void Update()
    {
        bottleAngles = bottle.transform.rotation.eulerAngles;

        if (bottleAngles.y >= 180 && bottleAngles.z >= 180)
        {
            if (bottleAngles.x <= 180)
            {
                bottleRotationX = 90 + (90 - bottleAngles.x);
            }
        }
        else
        {
            bottleRotationX = bottleAngles.x;
        }

        AmountUI();
    }

    private void AmountUI()
    {
        //isUsing�� ���� UI ���̱�
        amountUI.SetActive(isUsing);
        if (isUsing && amounts.Count > 0)
        {
            //��� ���̰� amounts List�� ũ�Ⱑ 0 �̻��� �� ������ �ε����� amounts�� ���� ������
            amountUI.GetComponentInChildren<Text>().text = ((int)amounts[amounts.Count - 1]).ToString() + "ml";
        }
    }

    /*
     * ������ �߰� �Լ�
     * �����Ǹ� �߰��� �� ���� �ܰ�� ������ �������� ���� ������
     */
    public void AddReceipt(BottleCtrl.BottleType bottleType)
    {
        if (receipt.Count == 0)
        {
            receipt.Add(bottleType);
            amounts.Add(1);
        }
        else
        {
            if (receipt[receipt.Count - 1].Equals(bottleType))
            {
                //amounts[amounts.Count - 1]++;
                
                if (bottleRotationX > 90 & bottleRotationX <= 180)
                {
                    if (bottleRotationX >= 140)
                    {
                        amounts[amounts.Count - 1] += 10.0f * Time.deltaTime;
                    }
                    else
                    {
                        amounts[amounts.Count - 1] += (bottleRotationX - 90) * 0.2f * Time.deltaTime;
                    }
                }

            }
            else
            {
                receipt.Add(bottleType);
                amounts.Add(1);
            }
        }
    }
}
