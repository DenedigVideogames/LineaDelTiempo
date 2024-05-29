
using UnityEngine;

[CreateAssetMenu(fileName ="New Ficha", menuName = "Ficha")]
public class FichaData : ScriptableObject
{
    [SerializeField] private int level;
    [SerializeField] private int year;
    [SerializeField] private string[] title;
    [SerializeField] private string[] invention;
    [SerializeField] private string[] description;
    [SerializeField] private Sprite image;


    public int Level { get { return level; } }

    public int Year { get { return year; } }

    public string[] Title { get { return title; } }

    public string[] Invention { get { return invention; } }
   
    public string[] Description { get { return description; } }

    public Sprite Image { get { return image; } }
}
