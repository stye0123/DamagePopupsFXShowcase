using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        // 檢查是否按下 ESC 鍵
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 在控制台輸出日誌
            Debug.Log("正在關閉遊戲...");
            // 關閉遊戲
            Application.Quit();
        }
    }
} 