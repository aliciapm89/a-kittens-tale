using UnityEngine;
//hola
[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] private DialogueLine[] All_Lines;

    public DialogueLine GetLineByIndex(int index)
    {
        return All_Lines[index];
    }

    public int GetLength()
    {
        return All_Lines.Length - 1;
    }
}
