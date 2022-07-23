using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCtrl : MonoBehaviour
{
    //������ ���
    public List<BottleCtrl.BottleType> receipt;
    //������ ��
    public List<int> amounts;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * ������ �߰� �Լ�
     * �����Ǹ� �߰��� �� ���� �ܰ�� ������ �������� ���� ������
     */
    public void AddReceipt(BottleCtrl.BottleType bottleType)
    {
        if(receipt.Count == 0)
        {
            receipt.Add(bottleType);
            amounts.Add(1);
        }
        else
        {
            if (receipt[receipt.Count - 1].Equals(bottleType))
            {
                amounts[amounts.Count - 1]++;
            }
            else
            {
                receipt.Add(bottleType);
                amounts.Add(1);
            }
        }
    }
}
