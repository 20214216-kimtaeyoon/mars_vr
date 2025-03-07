using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluateManager : MonoBehaviour
{
    [Header("종료 UI 관련")]
    public GameObject endUI;
    public Text endUIText;
    public Image background;
    public Image loadingBar;
    [Header("로딩 바가 전부 채워지기 까지의 시간")]
    [SerializeField]
    [Range(1f, 3f)]
    private float enableTime = 2f;
    public bool isHolding = false;
    private bool isEnd = false;
    private float elapsedTime = 0f;
    private GameObject cup;

    // Start is called before the first frame update
    void Start()
    {
        endUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Loading();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Cup"))
        {
            cup = other.gameObject;
            isHolding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Cup"))
        {
            cup = null;
            isHolding = false;
        }
    }

    //컵홀더 위에 컵이 올라갔을 때 로딩 바 늘어남, 로딩이 됐을 경우엔 EndUI를 보여줌
    private void Loading()
    {
        if (isHolding && elapsedTime < enableTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else if (!isHolding && elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
        }
        loadingBar.fillAmount = elapsedTime / enableTime;
        if(isEnd != (elapsedTime / enableTime >= 1f))
        {
            isEnd = true;
            ShowEndUI();
        }
    }

    //UI보여주는 메소드 (페이드 인, 컵에 담긴 레시피 출력)
    private void ShowEndUI()
    {
        if (cup == null)
            return;
        endUI.SetActive(true);
        StartCoroutine(FadeUI(0.5f));
        List<BottleCtrl.BottleType> receipts = cup.GetComponent<CupCtrl>().receipt;
        List<float> amount = cup.GetComponent<CupCtrl>().amounts;

        endUIText.text = "Receipt\n";
        for (int i = 0; i < receipts.Count; i++)
        {
            endUIText.text += receipts[i].ToString() + " : " + Mathf.Round(amount[i]).ToString() + "\n";
        }
    }

    IEnumerator FadeUI(float fadeTime)
    {
        float a = 0;
        Color backColor = background.color;
        Color textColor = endUIText.color;
        while (a < 1)
        {
            a += Time.deltaTime / fadeTime;
            backColor.a = a * (float)(200f / 255f);
            textColor.a = a;
            background.color = backColor;
            endUIText.color = textColor;
            yield return null;
        }
    }
}
