using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject
{
    public int ID;
    public string ItemName;
    public int Value;
    public Sprite Icon;
}
