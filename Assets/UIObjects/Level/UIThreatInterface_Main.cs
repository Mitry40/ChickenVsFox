using UnityEngine;
using UnityEngine.UI;

public class UIThreatInterface_Main : MonoBehaviour
{
    [SerializeField] private Image ImageValue;

    public float Value
    {
        set
        {
            ImageValue.fillAmount = value;
        }
    }

    private void Awake()
    {
        Value = 0;
    }

}