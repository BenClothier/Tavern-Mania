using UnityEngine;

[CreateAssetMenu(fileName = "Liquid", menuName = "Game/Liquid")]
public class Liquid : ScriptableObject
{
    public int ID;
    public string Name;
    public Color colour;
    public Sprite symbol;
}
