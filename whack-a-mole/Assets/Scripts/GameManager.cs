using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [Header("每隔兩秒鐘讓怪物秀出來")]
    public float showMonsterIntervalSeconds = 2;
    public float countDownShowMonsterSeconds;
    [Header("場上最多一次出現怪物數量")]
    int MAX_MONSTERS_ON_SCREEN = 3;
    [Tooltip("怪物列")]
    public List<Monster> monsters;
    public Text score;
    public int scoreNumber;
    /// <summary>
    /// 隱藏怪物
    /// </summary>
    /// <param name="monster"></param>
    public void HideMonster(GameObject monster)
    {
        monster.SetActive(false);
    }
    /// <summary>
    /// 顯示怪物
    /// </summary>
    /// <param name="monster"></param>
    void ShowMonster(GameObject monster)
    {
        monster.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        InitScore();
        InitMonsterList();
        HideAllMonsters();
        ShowRandomMoster();
        ResetShowMonserSeconds();
    }

    private void ResetShowMonserSeconds()
    {
        countDownShowMonsterSeconds = showMonsterIntervalSeconds;
    }

    //把所有的怪物隱藏起來
    void HideAllMonsters()
    {
        foreach (var m in monsters)
        {
            HideMonster(m.gameObject);
        }
    }
    /// <summary>
    /// 初始化怪物清單
    /// </summary>
    private void InitMonsterList()
    {
        monsters = GameObject.FindObjectsOfType<Monster>().ToList();
    }

    /// <summary>
    /// 加分
    /// </summary>
    public void AddScore()
    {
        //初始遊戲分數預設為10分
        scoreNumber += 10;
        //將它轉換成文字顯示出來
        score.text = scoreNumber.ToString();
    }
    /// <summary>
    /// 初始化UI
    /// </summary>
    private void InitScore()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        score.text = "";
    }
    /// <summary>
    /// 取得目前隱藏在場面上的怪物
    /// </summary>
    List<Monster> HiddenMonsters
    {
        get
        {
            var result = new List<Monster>();   //建立站存器清單

            foreach (var m in monsters)         //從monster裡面檢查
            {
                if (!m.IsActive)                //如果monster不是啟動狀態
                {
                    result.Add(m);              //把它加到清單
                }
            }
            return result;                      //回傳清單
        }
    }
    int MonsterCountOnScreen
    {
        get
        {
            int result = 0;                     //結果=0
            foreach (var m in monsters)
            {
                if (m.IsActive)
                {
                    result += 1;
                }
            }
            return result;
        }
    }
    void ShowRandomMoster()
    {
        int r = Random.Range(0, HiddenMonsters.Count);  //隨機生成0~目前場面上所有的怪物
        Monster m = HiddenMonsters[r];
        ShowMonster(m.gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        TryCountDownToShowMonster();
    }
    bool CountShowMonsterTimeUp => countDownShowMonsterSeconds <= 0;
    bool MonsterOnScreenAreFull => MonsterCountOnScreen >= MAX_MONSTERS_ON_SCREEN;
    private void TryCountDownToShowMonster()
    {
        countDownShowMonsterSeconds -= Time.fixedDeltaTime;
        if (CountShowMonsterTimeUp)
        {
            ResetShowMonserSeconds();
            if (!MonsterOnScreenAreFull)
            {
                ShowRandomMoster();
            }
        }
    }
    void Win()
    {
        if (scoreNumber == 1000)
        {

        }
    }
}
