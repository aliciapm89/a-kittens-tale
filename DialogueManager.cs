using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI Speaker_Name, Dialogue, Navigation_Button_Text;
    public Image Speakers_Sprite;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    private Animator anim;
    private Coroutine typing;

    public PlayerController player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            anim = GetComponent<Animator>();
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.anim.SetBool("isOpen", true);
        instance.player.speaking = true;
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.Speaker_Name.text = "";
        instance.Dialogue.text = "";
        instance.Navigation_Button_Text.text = ">";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        if (currentIndex > currentConvo.GetLength())
        {
            instance.anim.SetBool("isOpen", false);
            instance.player.speaking = false;
            return;
        }
        Speaker_Name.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();

        if (typing == null)
        {
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }
        else
        {
            instance.StopCoroutine(typing);
            typing = null;
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }

        Speakers_Sprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        currentIndex++;

        if (currentIndex >= currentConvo.GetLength())
        {
            Navigation_Button_Text.text = "X";
        }
    }

    private IEnumerator TypeText(string text)
    {
        Dialogue.text = "";
        bool complete = false;
        int index = 0;

        while (!complete)
        {
            Dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(.02f);

            if (index == text.Length)
                complete = true;
        }

        typing = null;
    }
}
