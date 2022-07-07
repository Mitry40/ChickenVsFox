using UnityEngine;
using UnityEngine.SceneManagement;

public enum ELevelResult
{
    Lose,
    Win
}

public struct TGlobalData
{
    public ELevelResult LevelResult;
}


public static class GlobalData
{
    public static TGlobalData Data;


    public static void QuitGame()
    {
        Application.Quit();
    }


    public static void LoadLevel()
    {
        Data.LevelResult = ELevelResult.Lose;
        SceneManager.LoadScene("SCLevel", LoadSceneMode.Single);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("SCMainMenu", LoadSceneMode.Single);
    }

    public static void LoadLevelReward()
    {
        SceneManager.LoadScene("SCRewardMenu", LoadSceneMode.Single);
    }

}