using UnityEngine;

public class MainMenuButtons_Main : MonoBehaviour
{
    public void OnButtonPlayClick() => GlobalData.LoadLevel();
    public void OnButtonExitClick() => GlobalData.QuitGame();
}