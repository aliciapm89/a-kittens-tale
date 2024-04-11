using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/New Speaker")]
public class Speaker : ScriptableObject
{
    [SerializeField] private string Speaker_Name;
    [SerializeField] private Sprite Speaker_Sprite;

    public string GetName()
    {
        return Speaker_Name;
    }

    public Sprite GetSprite()
    {
        return Speaker_Sprite;
    }
}
