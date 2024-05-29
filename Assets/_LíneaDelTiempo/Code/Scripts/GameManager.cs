using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using Dan.Main;
public class GameManager : MonoBehaviour
{
    public int _LevelIndex;

    [SerializeField] private int fichasCount;


    [SerializeField] private GameObject fichaPrefab;

    [SerializeField] private List<FichaData> Level01 = new List<FichaData>();
    [SerializeField] private List<FichaData> Level02 = new List<FichaData>();
    [SerializeField] private List<FichaData> Level03 = new List<FichaData>();

    private List<List<FichaData>> LevelsContainer= new List<List<FichaData>>();

    public List<FichaData> ActualFichas = new List<FichaData>();

    public DropSlot[] slots;

    
    [SerializeField] private TextMeshProUGUI aciertosLabel;

    public int Score = 0;

    [SerializeField] private Timer time;

    [SerializeField] private TextMeshProUGUI scoreLabel;

    [SerializeField] private int idioma;

    private static string publicKey = "4d20065bde55aba7263e07336e9700cc9ed70e2784ecc740ae4253ae301e2e5a";

    [SerializeField] private List<TextMeshProUGUI> userNames;
    [SerializeField] private List<TextMeshProUGUI> scores;
    [SerializeField] private TextMeshProUGUI myUserName;
    [SerializeField] private TextMeshProUGUI myScore;

    [SerializeField] private TMP_InputField userNameInputField;
    [SerializeField] private TextMeshProUGUI wariningField;
    [SerializeField] private TextMeshProUGUI wariningField2;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject checkButton;
    private int FinalScore;
  

    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("UserName"));
        GetLevels();
        GenerateFichas();
    }

    void GetLevels()
    {
        LevelsContainer.Add(Level01);
        LevelsContainer.Add(Level02);
        LevelsContainer.Add(Level03);
    }
    void GenerateFichas()
    {
        var actualLevel = LevelsContainer[_LevelIndex];
        var fichas = ShuffleList(actualLevel).Take(fichasCount).ToList();

        for (int i = 0; i <= fichasCount - 1; i++)
        {
            var actualObject = Instantiate(fichaPrefab, transform).GetComponent<Ficha>();
            actualObject._FichaData = fichas[i];
            ActualFichas.Add(actualObject._FichaData);
        }

        SortFichas();
    }

    private List<T> ShuffleList<T>(List<T> inputList)
    {
        System.Random rng = new System.Random();
        int n = inputList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = inputList[k];
            inputList[k] = inputList[n];
            inputList[n] = value;
        }
        return inputList;
    }

    void SortFichas()
    {
        var sortedList = ActualFichas.OrderByDescending(x => x.Year).ToList();

        ActualFichas.Clear();
        
        for(int i = 0 ; i  <= sortedList.Count-1; i++)
        {
            ActualFichas.Add(sortedList[i]);
        }
        AsignSlots();
    }

    public void AsignSlots()
    {
        for (int i = 0; i <= slots.Length - 1; i++)
        {
            slots[i].year = ActualFichas[i].Year;
        } 
    }
  
    public void Check()
    {
        StartCoroutine("CheckCoroutine");
    }

    IEnumerator  CheckCoroutine()
    {
        
        time._timmerIsRunning = false;
        Score = 0;
        for (int i = 0; i <= slots.Length - 1; i++)
        {
            if (slots[i].Correct)
            {
                slots[i].image.color = Color.green;
                slots[i].yearLabel.color = Color.green;

                Score++;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                slots[i].image.color = Color.red;
                slots[i].yearLabel.color = Color.red;
                yield return new WaitForSeconds(0.5f);
            }
        }

        float finalScore = Score * Globals.Score;
        aciertosLabel.text =Score + "/5";

        scoreLabel.text = Math.Round(finalScore).ToString();
        FinalScore = ((int)finalScore);
        GetLeaderBoard();
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlaySound2D("exito");
        AudioManager.Instance.StopMusic();
        gameOverPanel.SetActive(true);
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
        {
            for(int i = 0; i < userNames.Count; ++i)
            {
                userNames[i].text = (msg[i].Rank +".-"+ msg[i].Username);
                scores[i].text = msg[i].Score.ToString();             
            }
        }));
    }

    public void GetPlayerPosition()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => 
        {
            foreach (var item in msg)
            {
                if (PlayerPrefs.GetString("UserName") == item.Username)
                {
                    if (item.Rank > 5)
                    {
                        myUserName.text = item.Rank + ".-" + item.Username;
                        myScore.text = item.Score.ToString();
                    }
                }
            }
        }));
    }

    public void SetLeaderBoardEntry(string username, int score)
    {
            LeaderboardCreator.UploadNewEntry(publicKey, username, score, (msg) =>
            {
                LeaderboardCreator.ResetPlayer();
                checkButton.GetComponent<Button>().enabled = false;
                wariningField2.gameObject.SetActive(false);
                GetLeaderBoard();
                GetPlayerPosition();
            }, (msg)=>
            {
                checkButton.GetComponent<Button>().enabled = true;
                wariningField2.gameObject.SetActive(true);
            });
    }

    public void SetUserName()
    {
        if (userNameInputField.text.Length < 5)
        {
            wariningField2.gameObject.SetActive(false);
            wariningField.gameObject.SetActive(true);
            return;
        }
        wariningField.gameObject.SetActive(false);
        PlayerPrefs.SetString("UserName", userNameInputField.text);
        SetLeaderBoardEntry(userNameInputField.text, FinalScore);
    }
}
