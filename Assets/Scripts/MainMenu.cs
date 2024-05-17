using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int initialGridSize = 10;

    public GamePropertiesSO gameProperties;
    
    public Slider gridSizeSlider;
    public TextMeshProUGUI gridSizeText;

    public void Awake()
    {
        initialGridSize = gameProperties.gridSize;
        gridSizeSlider.value = initialGridSize;
        gridSizeText.text = initialGridSize.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetGridSize(float size)
    {
        gridSizeText.text = size.ToString();
        gameProperties.gridSize = (int)size;
    }
}
