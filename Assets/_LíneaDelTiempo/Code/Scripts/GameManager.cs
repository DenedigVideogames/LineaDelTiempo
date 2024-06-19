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

    private List<List<FichaData>> LevelsContainer = new List<List<FichaData>>();
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

    // Add the public boolean
    public bool showFichasAtSlots = false;
    public bool spawnCorrectAndIncorrectFichas = false;

    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("UserName"));
        GetLevels();

        if (spawnCorrectAndIncorrectFichas)
        {
            SpawnCorrectAndIncorrectFichas();
        }
        else if (showFichasAtSlots)
        {
            SpawnFichasInSlots();
            showFichasAtSlots = false;
        }
        else
        {
            GenerateFichas();
        }
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

        for (int i = 0; i <= sortedList.Count - 1; i++)
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

    IEnumerator CheckCoroutine()
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
        aciertosLabel.text = Score + "/5";
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
            for (int i = 0; i < userNames.Count; ++i)
            {
                userNames[i].text = (msg[i].Rank + ".-" + msg[i].Username);
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
        }, (msg) =>
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

    //spawnear las fichas en los slots
    void SpawnFichasInSlots()
    {
        var actualLevel = LevelsContainer[_LevelIndex];
        var fichas = ShuffleList(actualLevel).Take(fichasCount).ToList();

        for (int i = 0; i <= fichasCount - 1; i++)
        {
            var ficha = Instantiate(fichaPrefab, slots[i].transform.position, Quaternion.identity).GetComponent<Ficha>();
            ficha.transform.SetParent(slots[i].transform);
            ficha._FichaData = fichas[i];
            ActualFichas.Add(ficha._FichaData);
            slots[i].year = ficha._FichaData.Year; // Asigna el año al slot
        }

        SortFichas();
    }

    void SpawnCorrectAndIncorrectFichas()
    {
        var actualLevel = LevelsContainer[_LevelIndex];
        var fichas = ShuffleList(actualLevel).Take(fichasCount).ToList();

        // Crear una lista para fichas correctas y otra para incorrectas
        var correctFichas = fichas.Take(3).ToList();
        var incorrectFichas = fichas.Skip(3).Take(2).ToList();

        // Barajar las posiciones de los slots para asignar fichas incorrectas
        var slotIndices = Enumerable.Range(0, slots.Length).ToList();
        ShuffleList2(slotIndices);

        // Asignar fichas correctas a los primeros 3 slots
        for (int i = 0; i < correctFichas.Count; i++)
        {
            var ficha = Instantiate(fichaPrefab, transform).GetComponent<Ficha>();
            ficha._FichaData = correctFichas[i];

            // Asignar el año correcto al slot
            slots[slotIndices[i]].year = ficha._FichaData.Year;

            // Inicializar la ficha en el slot correspondiente
            slots[slotIndices[i]].InitializeFicha(ficha.gameObject);
        }

        // Asignar fichas incorrectas a los slots restantes
        for (int i = 0; i < incorrectFichas.Count; i++)
        {
            var ficha = Instantiate(fichaPrefab, transform).GetComponent<Ficha>();
            ficha._FichaData = incorrectFichas[i];

            // Asignar un año incorrecto al slot
            int slotIndex = slotIndices[correctFichas.Count + i];
            slots[slotIndex].year = ficha._FichaData.Year;

            // Inicializar la ficha en el slot correspondiente
            slots[slotIndex].InitializeFicha(ficha.gameObject);
        }

        // Intercambiar las fichas entre los dos slots restantes
        var tempFicha = slots[slotIndices[3]].item;
        var tempYear = slots[slotIndices[3]].year;

        slots[slotIndices[3]].item = slots[slotIndices[4]].item;
        slots[slotIndices[3]].year = slots[slotIndices[4]].year;

        slots[slotIndices[4]].item = tempFicha;
        slots[slotIndices[4]].year = tempYear;

        SortFichas();
    }


    // Helper method to shuffle a list
    private List<T> ShuffleList2<T>(List<T> inputList)
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

    //vamos bien, funciona, pero los slots no reconocen que la ficha correcta esta puesta correctamente 
}