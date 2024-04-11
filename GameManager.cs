using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//de NPC
public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public float PlayerWalkSpeed;
    public float PlayerRunSpeed;
    public float PlayerJumpForce;
    public float PlayerJumpGravityScale;

    [Header("Trees")]
    public Sprite GreenTree;
    public Sprite DarkTree;
    public Sprite BrownTree;

    [Header("Dialogues")]
    public Conversation MotherConvoStart;
    public Conversation MotherConvoYes;
    public Conversation MotherConvoNo;
    public Conversation MotherActiveConvo;
    public Conversation MotherConvoEnd;

    [Space(10)]
    public Conversation ChildFound;

    [Space(10)]
    public Conversation EmoConvoStart;
    public Conversation EmoConvoYes;
    public Conversation EmoConvoNo;
    public Conversation EmoConvoActive;
    public Conversation EmoConvoEndGood;
    public Conversation EmoConvoEndBad;

    [Header("Selections")]
    public Transform MotherSelection1;
    public Transform EmoSelection1;

    [Header("NPCs")]
    public GameObject ChildNPC;

    [Header("UI")]
    public GameObject FruitSelectorUI;
    public Transform FruitSelectorContent;
    [Space(10)]
    public GameObject AppleButton;
    public GameObject OrangeButton;
    public GameObject PearButton;

    [Header("Transforms")]
    public Transform ChildNewPosition;
    public Transform FruitTreesPosition;

    [HideInInspector] public bool motherMissionStarted = false;
    [HideInInspector] public bool childFound = false;
    [HideInInspector] public bool motherMissionCompleted = false;
    [HideInInspector] public bool emoMissionStarted = false;
    [HideInInspector] public bool fruitFound = false;
    [HideInInspector] public bool emoMissionCompleted = false;

    public bool hasApple = false;
    public bool hasPear = false;
    public bool hasOrange = false;

    [HideInInspector] public string playerName = "<color=orange>NAME</color>";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartConvo(Conversation convo)
    {
        DialogueManager.StartConversation(convo);
    }

    public void ResetMother()
    {
        motherMissionStarted = false;
        ChildNPC.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ResetEmo()
    {
        emoMissionStarted = false;
    }

    public void ComponentMove(string component, Transform NewPosition)
    {
        if (component == "Child")
        {
            ChildNPC.transform.position = NewPosition.position;
        }
    }

    public void SelectFruit()
    {
        if (hasApple)
            AppleButton.SetActive(true);
        if (hasPear)
            PearButton.SetActive(true);
        if (hasOrange)
            OrangeButton.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
