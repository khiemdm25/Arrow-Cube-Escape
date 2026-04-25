using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private List<Button> levelButtons = new List<Button>();
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private string playSceneName = "PlayScene";

    private void Start()
    {
        SetupButtons();
    }

    void SetupButtons()
    {
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.Play();
        });

        for (int i = 0; i < levelButtons.Count; i++)
        {
            int index = i;
            levelButtons[i].onClick.RemoveAllListeners();
            levelButtons[i].onClick.AddListener(() =>
            {
                StartCoroutine(LoadScene());
            });

            UpdateButtonState(levelButtons[i], index);
        }
    }

    IEnumerator LoadScene()
    {
        loadingPanel.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(playSceneName);
        op.allowSceneActivation = false;
        float progress = 0f;
        while (!op.isDone)
        {
            float target = Mathf.Clamp01(op.progress / 0.9f);
            progress = Mathf.Lerp(progress, target, Time.deltaTime * 5f);
            loadingSlider.value = progress;
            if (progress >= 0.99f)
            {
                yield return new WaitForSeconds(0.2f);
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    void UpdateButtonState(Button button, int index)
    {
        bool isUnlocked = index <= LevelManager.Instance.highestUnlockedLevel;
        button.interactable = isUnlocked;
        ColorBlock colors = button.colors;
        colors.normalColor = isUnlocked ? Color.white : Color.gray;
        button.colors = colors;
    }
}