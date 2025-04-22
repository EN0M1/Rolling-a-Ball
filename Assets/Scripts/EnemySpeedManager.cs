using UnityEngine;
using TMPro;

public enum EnemySpeed {Slow, Medium, Fast}

public class EnemySpeedManager : MonoBehaviour
{
    public static EnemySpeedManager instance;
    public static EnemySpeed SpeedLevel;

    public TextMeshProUGUI speedText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            EnemySpeedManager.SpeedLevel = EnemySpeed.Slow;
        }
    }

    private void Update()
    {
        UpdateSpeedText();
    }

    public void SetSlow()
    {
        NewSpeedSelected(EnemySpeed.Slow);
    }

    public void SetMedium()
    {
        NewSpeedSelected(EnemySpeed.Medium);
    }

    public void SetFast()
    {
        NewSpeedSelected(EnemySpeed.Fast);
    }

    private void NewSpeedSelected(EnemySpeed spdlvl)
    {
        EnemySpeedManager.SpeedLevel = spdlvl;
        Debug.Log("Speed selected: " + spdlvl);
    }

    private void UpdateSpeedText()
    {
        speedText.text = "Enemy Movement Speed: " + EnemySpeedManager.SpeedLevel.ToString();
    }
}
