using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlashcardListUIController : MonoBehaviour {

    [SerializeField]
    public VisualTreeAsset termTemplate;

    private UIManager manager;

    private int unitNumber;
    private string screenHeader;

    public Label header;
    public Button backButton;
    public Button allFlashcardsButton;
    public Button starredButton;
    public Button startButton;

    public GroupBox[] terms;

    public List<(string, string)>[] flashcards;

    void OnEnable() {
        manager = transform.parent.transform.parent.GetComponent<UIManager>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        header = root.Q<Label>("Header");
        header.text = screenHeader;

        backButton = root.Q<Button>("BackButton");
        backButton.clicked += BackButtonPressed;

        allFlashcardsButton = root.Q<Button>("AllFlashcardsButton");
        allFlashcardsButton.clicked += AllFlashcardsButtonPressed;

        starredButton = root.Q<Button>("StarredButton");
        starredButton.clicked += StarredButtonPressed;

        startButton = root.Q<Button>("StartButton");
        startButton.clicked += StartButtonPressed;

        ScrollView scrollView = root.Q<ScrollView>("FlashcardScrollView");
        for (int i = 0; i < terms.Length; i++) {
            scrollView.contentContainer.Add(terms[i]);
        }
    }

    void BackButtonPressed() {
        manager.SwitchScreen(transform.parent.gameObject, manager.unitOverview);
    }

    void AllFlashcardsButtonPressed() {
        Debug.Log("All flashcards pressed");
    }

    void StarredButtonPressed() {
        Debug.Log("Starred pressed");
    }

    void StartButtonPressed() {
        manager.flashcard.GetComponentInChildren<FlashcardUIController>().UpdateFlashcards(flashcards[unitNumber - 1]);
        manager.SwitchScreen(transform.parent.gameObject, manager.flashcard);
    }

    public void UpdateFlashcards(int unitNumber) {
        this.unitNumber = unitNumber;
        screenHeader = "Unit " + unitNumber;

        // Change to read from file later
        flashcards = new List<(string, string)>[6];
        for (int i = 0; i < flashcards.Length; i++) {
            flashcards[i] = new List<(string, string)>();
        }

        for (int i = 0; i < flashcards.Length; i++) {
            flashcards[i].Add(("??????", "name"));
            flashcards[i].Add(("???", "truly, really, very"));
            flashcards[i].Add(("????????????", "science"));
            flashcards[i].Add(("??????", "here"));
            flashcards[i].Add(("??????", "building"));
            flashcards[i].Add(("???", "yes"));
            flashcards[i].Add(("???", "what"));
            flashcards[i].Add(("?????????", "pretty"));
            flashcards[i].Add(("??????", "major"));
            flashcards[i].Add(("???", "1"));
            flashcards[i].Add(("??????", "school year"));
            flashcards[i].Add(("???", "Oh!, Ah!, Alas!"));
            flashcards[i].Add(("?????????", "computer"));
            flashcards[i].Add(("??????", "really"));
        }

        terms = new GroupBox[flashcards[unitNumber - 1].Count];

        for (int i = 0; i < terms.Length; i++) {
            GroupBox groupBox = termTemplate.CloneTree().Q<GroupBox>();
            groupBox.Q<Label>("Term").text = flashcards[unitNumber - 1][i].Item1;
            groupBox.Q<Label>("Definition").text = flashcards[unitNumber - 1][i].Item2;
            terms[i] = groupBox;
        }
    }

}
