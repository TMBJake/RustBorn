using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public GameObject settingsCanvas;
    public GameObject menuCanvas;
    public Image BrightnessProp;
    public static bool isPaused = false;

    public Slider volumeSlider;
    public Slider brightnessSlider;
    public TMP_Text Volume;
    public TMP_Text Brightness;
    public Button settingsButton;
    public Button escapeButton;

    private const string VolumeKey = "Volume";
    private const string BrightnessKey = "Brightness";

    void Start()
    {
        MenuManager.isMenu = false;
        // Load saved settings or set to default values
        volumeSlider.value = PlayerPrefs.GetFloat(VolumeKey, 0.5f); // Default: 50%
        brightnessSlider.value = PlayerPrefs.GetFloat(BrightnessKey, 1f); // Default: 100%

        // Apply them immediately
        ApplyVolume(volumeSlider.value);
        ApplyBrightness(brightnessSlider.value);

        // Add listeners to save changes in real time
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

        settingsButton.onClick.AddListener(ShowSettings);

        //Escape button 
        escapeButton.onClick.AddListener(() => {
            ShowMenu();
        });
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }

    private void OnVolumeChanged(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        Volume.SetText($"Volume:\n{percent}%");
        PlayerPrefs.SetFloat(VolumeKey, value);
        ApplyVolume(value);
    }

    private void OnBrightnessChanged(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        Brightness.SetText($"Brightness:\n{percent}%");
        PlayerPrefs.SetFloat(BrightnessKey, value);
        ApplyBrightness(value);
    }

    private void ApplyVolume(float value)
    {
        // Adjust audio (Uses global AudioListener)
        AudioListener.volume = value;
    }

    private void ApplyBrightness(float value)
    {
        // Invert the value because 1 = bright (transparent), 0 = dark (opaque)
        float alpha = 1f - value;

            Color color = BrightnessProp.color;
            color.a = alpha;
            BrightnessProp.color = color;

        Debug.Log($"Brightness set to {value} (Alpha: {alpha})");
    }

    public void ShowMenu()
    {
        MenuManager.isMenu = true;
        settingsCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void ShowSettings()
    {
        MenuManager.isMenu = false;
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    private void OnDisable()
    {
        PlayerPrefs.Save(); // Save all changes when closing settings
    }

}