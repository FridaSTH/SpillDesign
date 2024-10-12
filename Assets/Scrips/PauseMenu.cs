using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    [SerializeField] RectTransform pauseTitlePanel;
    [SerializeField] float pauseTitleYPosBegin, pauseTitleYPosEnd;

    [SerializeField] RectTransform pauseContentPanel;
    [SerializeField] float pauseContentYPosBegin, pauseContentYPosEnd;

    [SerializeField] float tweenDuration;

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        PauseIntro();
    }

    public async void ResumeGame() {
        await PauseOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PauseIntro() {
        pauseTitlePanel.DOAnchorPosY(pauseTitleYPosEnd, tweenDuration).SetUpdate(true);
        pauseContentPanel.DOAnchorPosY(pauseContentYPosEnd, tweenDuration).SetUpdate(true);
    }

    async Task PauseOutro() {
        var pauseTitleTween = pauseTitlePanel.DOAnchorPosY(pauseTitleYPosBegin, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        var pauseContentTween = pauseContentPanel.DOAnchorPosY(pauseContentYPosBegin, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        await Task.WhenAll(pauseTitleTween, pauseContentTween);
    }

}
