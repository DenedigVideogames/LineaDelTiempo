using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private TMP_InputField userNameInputField;
    [SerializeField] private TextMeshProUGUI wariningField;
    [SerializeField] private GameObject idiomaScreen;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private AnimationsManager animationsManager;

    [SerializeField] private Scenes sceneManager;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject reglasPanel;
    [SerializeField] private GameObject omitirButton;

    private static string publicKey = "4d20065bde55aba7263e07336e9700cc9ed70e2784ecc740ae4253ae301e2e5a";

    [SerializeField] private List<TextMeshProUGUI> userNames;
    [SerializeField] private List<TextMeshProUGUI> scores;

    public GameObject SeleccionCanvas;
    private void Start()
    {
        GetLeaderBoard();
        if (Globals.idiomaWasSelected){
            idiomaScreen.SetActive(false);
            menuScreen.SetActive(true);
            animationsManager.EntraMenu();
        }
    }

    public void idiomaSelected()
    {
        Globals.idiomaWasSelected = true;
        
    }

    public void Play()
    {
        if (Globals.firstPlay)
        {
            reglasPanel.SetActive(true);
            AudioManager.Instance.StopSound();
            AudioManager.Instance.PlaySound2D("01");
            Globals.firstPlay = false;
            menuPanel.SetActive(false);
            SeleccionCanvas.SetActive(false);
            omitirButton.SetActive(true);
        }
        else
        {
            Debug.Log("Cambio de escena");
            SceneManager.LoadScene("InventoresMexicanosGame");
        }
    }

    public void Test()
    {
        SceneManager.LoadScene("TESTNEW");
    }

    public void SetLanguage(int language)
    {
        PlayerPrefs.SetInt("Idioma", language);
        Debug.Log(PlayerPrefs.GetInt("Idioma"));
    }

    public void GetLeaderBoard()
    {

        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => {
            for (int i = 0; i < userNames.Count; ++i)
            {
                userNames[i].text = (msg[i].Rank + ".-" + msg[i].Username);
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }
}
