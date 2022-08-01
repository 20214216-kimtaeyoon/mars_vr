using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupCtrl : MonoBehaviour
{
    //������ ���
    public List<BottleCtrl.BottleType> receipt;
    //������ ��
    public List<float> amounts;

    public GameObject bottle;
    public float bottleRotationX;
    public Vector3 bottleAngles;

    //MLǥ��(UI)
    private GameObject ML;
    //amounts int��
    int newAmounts;

    //���ǰ� ������
    public bool isUsing;


    // Start is called before the first frame update
    void Start()
    {
        bottle = GameObject.Find("Bottle");
        ML = GameObject.Find("Canvas/ML");
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

        //MLǥ��(UI) CUP ����ٴϱ�
        ML.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.3f, 0.1f, 0.1f));
        newAmounts = (int)amounts[amounts.Count - 1];

        //���ǰ� ������ MLUI ����
        if (isUsing)
        {
            ML.gameObject.SetActive(true);
            if (amounts.Count > 0)
            {
                ML.GetComponent<Text>().text = newAmounts.ToString() + " ML";
            }
        }
        else
            ML.gameObject.SetActive(false);
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
