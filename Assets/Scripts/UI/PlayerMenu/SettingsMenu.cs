using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : PlayerMenuPage
{
    public override void OnPageOpen()
    {

    }

    public override bool CloseSubMenu()
    {
        return false;
    }

    public void LoadGame()
    {
        DataPersistenceManager.instance.LoadGame();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveGame()
    {
        DataPersistenceManager.instance.SaveGame();
    }
}
