using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class FichaGlosario : MonoBehaviour
{
    private int level;

    public int year;

    private string title;

    private string invention;

    private string description;

    private Sprite image;

    public FichaData _FichaData;

    [SerializeField] private TextMeshProUGUI yearLabel;
    [SerializeField] private TextMeshProUGUI titleLabel;
    [SerializeField] private TextMeshProUGUI inventionLabel;
    [SerializeField] private TextMeshProUGUI descriptionLabel;
    [SerializeField] private Image sourceImage;
    void Start()
    {
        GetData();
        SetData();

    }

    void SetData()
    {
        yearLabel.text = year.ToString();
        titleLabel.text = title;
        inventionLabel.text = invention;
        descriptionLabel.text = description;
        sourceImage.sprite = image;
    }
    void GetData()
    {
        level = _FichaData.Level;
        year = _FichaData.Year;
        title = _FichaData.Title[PlayerPrefs.GetInt("Idioma")];
        invention = _FichaData.Invention[PlayerPrefs.GetInt("Idioma")];
        description = _FichaData.Description[PlayerPrefs.GetInt("Idioma")];
        // description = _FichaData.Description;
        image = _FichaData.Image;
    }

}
