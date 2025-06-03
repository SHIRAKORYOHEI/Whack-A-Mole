using UnityEngine;
using System.Collections;
using TMPro;

public class Mole : MonoBehaviour
{
    bool OnMole;
    bool isMoving;
    Vector3 hiddenPosition;
    Vector3 shownPosition;
    Vector3 targetPosition;
    Coroutine autoHideCoroutine;

    public static int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    
    void Start()
    {
        hiddenPosition = new Vector3(transform.position.x, -0.5f, transform.position.z);
        shownPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        transform.position = hiddenPosition;

        StartCoroutine(RandomPop());
    }
    
    void Update()
    {
        if (isMoving) Move();
    }

    void OnMouseDown()
    {
        if (OnMole && !isMoving)
        {
            // プレイヤーが叩いたら自動で下がる処理を止める
            if (autoHideCoroutine != null)
            {
                StopCoroutine(autoHideCoroutine);
                autoHideCoroutine = null;
            }
            targetPosition = hiddenPosition;
            isMoving = true;
            score++;
        }
    }
    
    void Move()
    {
        const float speed = 1f;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        //微調整
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
            isMoving = false;
            OnMole = targetPosition == shownPosition;

            if (OnMole)
            {
                autoHideCoroutine = StartCoroutine(AutoHideAfterDelay(1.0f)); // 1秒後に自動で隠れる
            }
        }
    }
    
    // ランダムな時間経過で出現
    IEnumerator RandomPop()
    {
        float waitMin = 1.5f;
        float waitMax = 6f;
        
        while (true)
        {
            float wait = Random.Range(waitMin, waitMax);
            yield return new WaitForSeconds(wait);

            if (!OnMole && !isMoving)
            {
                targetPosition = shownPosition;
                isMoving = true;
            }
        }
    }

    IEnumerator AutoHideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (OnMole && !isMoving)
        {
            targetPosition = hiddenPosition;
            isMoving = true;
        }
    }
}