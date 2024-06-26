using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button restartButton;
    private ActorController actorController;

    void Start()
    {
        // ActorControllerスクリプトを見つける
        actorController = FindObjectOfType<ActorController>();

        // ボタンを初期的に非アクティブにする
        restartButton.gameObject.SetActive(false);

        // ボタンにリスナーを追加
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowRestartButton()
    {
        Debug.Log("ShowRestartButton called");
        restartButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        Debug.Log("RestartGame called");
        // ActorControllerのResetPositionメソッドを呼び出す
        actorController.ResetPosition();

        // リスタートボタンを非表示にする
        restartButton.gameObject.SetActive(false);
    }
}