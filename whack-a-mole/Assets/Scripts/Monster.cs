using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private GameManager gm;
    [Header("最大時間")]
    public float maxSceondsOnScreen = 2.5f;
    [Header("目前秒數")]
    public float currentSecondsOnScreen;

    // Start is called before the first frame update
    void Start()
    {
        //尋找遊戲物件以及組件
        //使遊戲一開始老鼠即可抓取到遊戲管理器
        Init();
    }
    /// <summary>
    /// 初始化的準備工作
    /// </summary>
    private void Init()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        RestCurrentSecondsOnScreen();
    }

    /// <summary>
    /// 點擊到老鼠
    /// </summary>
    void OnMouseDown()
    {
        gm.AddScore();
        RestCurrentSecondsOnScreen();
        HIde();
    }

    private void HIde()
    {
        gm.HideMonster(gameObject);
    }

    public bool IsActive => gameObject.activeInHierarchy;
    bool OnScreenTineUp => currentSecondsOnScreen < 0;
    /// <summary>
    /// 更新時間再用的(每0.02秒會更新一次)
    /// </summary>
    private void FixedUpdate()
    {
        TryCountDownToHide();
    }

    private void TryCountDownToHide()
    {
        //如果怪物出生的話
        if (IsActive)
        {
            //呼叫開始倒數秒數
            CountDownCurrentSecondsOnScreen();
        }
        if (OnScreenTineUp)
        {
            RestCurrentSecondsOnScreen();
            //呼叫怪物消失
            HIde();
        }
    }

    private void CountDownCurrentSecondsOnScreen()
    {
        //開始倒數秒數
        currentSecondsOnScreen -= Time.fixedDeltaTime;
    }

    private void RestCurrentSecondsOnScreen()
    {
        //重新倒數計時的秒數
        currentSecondsOnScreen = maxSceondsOnScreen;
    }
}
