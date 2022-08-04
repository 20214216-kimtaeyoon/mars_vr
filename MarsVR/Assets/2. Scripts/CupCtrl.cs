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


    [Header("UI ����")]
    //MLǥ��(UI)
    public Transform uiPos;
    public GameObject amountUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
    public void AddReceipt(BottleCtrl.BottleType bottleType, float inputAmount)
    {
        if (receipt.Count == 0)
        {
            receipt.Add(bottleType);
            amounts.Add(inputAmount);
        }
        else
        {
            if (receipt[receipt.Count - 1].Equals(bottleType))
            {
                amounts[amounts.Count - 1] += inputAmount * Time.deltaTime;
            }
            else
            {
                receipt.Add(bottleType);
                amounts.Add(inputAmount);
            }
        }
    }
}
