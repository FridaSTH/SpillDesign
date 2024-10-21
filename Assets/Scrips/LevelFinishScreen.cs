using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class LevelFinishScreen : MonoBehaviour
{
    [SerializeField] GameObject finishScreen;

    [SerializeField] RectTransform finishScreenTitlePanel;
    [SerializeField] float titleYPosEnd;

    [SerializeField] RectTransform finishScreenContentPanel;
    [SerializeField] float contentYPosEnd;

    [SerializeField] float tweenDuration;
    [SerializeField] float tweenDuration2;

    [SerializeField] CanvasGroup backgroundCanvasGroup;
    [SerializeField] CanvasGroup titleCanvasGroup;
    [SerializeField] CanvasGroup contentCanvasGroup;

    public Animator transition;
    public float transitionTime = 1f;

    public async void FinishLevel() {
        finishScreen.SetActive(true);
        Time.timeScale = 0;
        await FinishLevelIntro();
    }

    async Task FinishLevelIntro() {
        backgroundCanvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        await titleCanvasGroup.DOFade(1, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        finishScreenTitlePanel.DOAnchorPosY(titleYPosEnd, tweenDuration2).SetUpdate(true);
        contentCanvasGroup.DOFade(1, tweenDuration2).SetUpdate(true);
        finishScreenContentPanel.DOAnchorPosY(contentYPosEnd, tweenDuration2).SetUpdate(true);
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }

    public void ContinueLevel() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 3) {
            // Completed last level, show ending animation?
            ReturnToMainMenu();  // <----------------- temp
        } else {
            StartCoroutine(LoadLevel(currentLevel + 1));
            Time.timeScale = 1;
        }
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(levelIndex);
    }
}
