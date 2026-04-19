using UnityEngine;

public class ResetUI : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    public void OpenConfirm()
    {
        confirmPanel.SetActive(true);
    }

    public void CancelReset()
    {
        confirmPanel.SetActive(false);
    }

    public void ConfirmReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("RESET DONE");

        confirmPanel.SetActive(false);

        // reload lại scene menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
}