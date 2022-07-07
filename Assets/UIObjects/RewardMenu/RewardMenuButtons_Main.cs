using UnityEngine;

public class RewardMenuButtons_Main : MonoBehaviour
{
    [SerializeField] GameObject PanelWin;
    [SerializeField] GameObject PanelLose;

    private void Awake()
    {
        PanelWin.SetActive(false);
        PanelLose.SetActive(false);

        switch (GlobalData.Data.LevelResult)
        {
            case ELevelResult.Win: PanelWin.SetActive(true); break;
            case ELevelResult.Lose: PanelLose.SetActive(true); break;
        }
    }

    public void OnButtonMenuClick() => GlobalData.LoadMainMenu();

}