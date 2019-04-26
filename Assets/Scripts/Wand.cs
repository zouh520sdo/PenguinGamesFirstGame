using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class Wand : MonoBehaviour {

    public bool isPaused;
    public float heat;
    public float minHeat;
    public float maxHeat;
    public float wandRange;
    public Wandable wandable;
    public Pickable pickable;
    public float absorbRatio;
    public float releaseRatio;
    public DistanceIndicator distanceIndicator;
    public Animator handAnimator;
    public ParticleSystem wandEffect;
    public GameManager gameManager;
    public GameObject frogWandObj;
    public GameObject normalWandObj;

    private float holdingTime;

    // Data that needs to reset
    [HideInInspector]public float originalHeat;
    [HideInInspector]public Vector3 originalPos;

    // For heat UI
    public Text heatText;
    public GameObject HTUI;
    public Text instruction;

    //FOR MARKER
    public Sprite Normal;
    public Sprite CanInreract;
    public Sprite CanAOrB;
    public Sprite CanNotAOrB;
    public Sprite Aing;
    public Sprite Ring;
    public Sprite Picking;

    public Image Marker;

    // For dialogue 
    public GameObject dialogueObj;
    protected Image dialoguePanel;
    protected Text dialogue;
    public Note note;
    public FirstPersonController fpc;

    // For dialogue selection
    public GameObject selectionObj;
    protected Button aButton;
    protected Button sButton;
    protected Button dButton;
    protected Text aText;
    protected Text sText;
    protected Text dText;
    protected MakeSelectionDelegate makeSelectHolder;
    protected List<DialogueOption> optionsHolder;

    // For diary
    public GameObject diaryPanelObj;
    protected Text diaryText;

    // For wand effect
    public Beam beam;

    // For debug
    public NextLevelTrigger NLT;

    public void OnReset()
    {
        heat = originalHeat;
        transform.position = originalPos;

        handAnimator.ResetTrigger("Show");
        handAnimator.SetTrigger("Hide");
        wandEffect.gameObject.SetActive(false);
        //beam.gameObject.SetActive(false);
    }

    void OnSave()
    {
        JSONSaveLoad.WriteJSON(name, originalPos);
    }

    void OnLoad()
    {
        originalPos = JSONSaveLoad.LoadJSON<Vector3>(name);
        transform.position = originalPos;
    }

    private void Awake()
    {
        tag = "Player";
        if (!dialogueObj)
        {
            print("Dialogue text needs to be assigned.");
        }
        else
        {
            dialogue = dialogueObj.GetComponentInChildren<Text>();
            dialoguePanel = dialogueObj.GetComponentInChildren<Image>();
            dialogue.enabled = false;
            //dialoguePanel.enabled = false;
            dialoguePanel.gameObject.SetActive(false);
            dialogue.text = "";
        }
        HTUI.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        holdingTime = 0;
        originalHeat = heat;
        originalPos = transform.position;
        wandable = null;
        if (pickable)
        {
            pickable.Drop(this);
            pickable = null;
        }

        if (!distanceIndicator)
        {
            GameObject indicatorObj = GameObject.Find("DistanceIndicator");
            if (indicatorObj)
            {
                distanceIndicator = indicatorObj.GetComponent<DistanceIndicator>();
            }
        }
        Marker.sprite = Normal;
        instruction.text = "";
        HTUI.SetActive(false);
        wandEffect.gameObject.SetActive(false);
        //beam.gameObject.SetActive(false);

        if (!NLT)
        {
            NLT = GameObject.FindGameObjectWithTag("NextLevel").GetComponent<NextLevelTrigger>();
        }

        if (!fpc)
        {
            fpc = GetComponent<FirstPersonController>();
        }

        if (diaryPanelObj)
        {
            diaryText = diaryPanelObj.transform.Find("DiaryText").GetComponent<Text>();
            diaryPanelObj.SetActive(false);
            diaryText.text = "";
        }

        if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            print("Diary amount " + gameManager.GetDiaryAmount());
            print("Has gua " + gameManager.GetHasGua());
            gameManager.ResetFoundThisDiary();
            print("Found this diary " + gameManager.GetFoundThisDiary());
        }

        if (selectionObj)
        {
            aButton = selectionObj.transform.Find("AButton").GetComponent<Button>();
            aText = aButton.transform.Find("Text").GetComponent<Text>();
            sButton = selectionObj.transform.Find("SButton").GetComponent<Button>();
            sText = sButton.transform.Find("Text").GetComponent<Text>();
            dButton = selectionObj.transform.Find("DButton").GetComponent<Button>();
            dText = dButton.transform.Find("Text").GetComponent<Text>();

            selectionObj.SetActive(false);
        }

        SetWand(PlayerPrefs.GetInt("HasGua"));
    }

    public void ShowDialogueSelection(MakeSelectionDelegate onSelection, List<DialogueOption> options)
    {
        makeSelectHolder = onSelection;
        optionsHolder = options;
        selectionObj.SetActive(true);
        fpc.GetMouseLook().SetCursorLock(false);
        if (options.Count == 2)
        {
            aButton.gameObject.SetActive(true);
            aButton.onClick.AddListener(() => MakeDialogueSelection(onSelection, 0));
            sButton.gameObject.SetActive(false); // Hide the middle option
            dButton.gameObject.SetActive(true);
            dButton.onClick.AddListener(() => MakeDialogueSelection(onSelection, 1));

            aText.text = options[0].content;
            dText.text = options[1].content;
        }
        else if (options.Count == 3)
        {
            aButton.gameObject.SetActive(true);
            aButton.onClick.AddListener(() => MakeDialogueSelection(onSelection, 0));
            sButton.gameObject.SetActive(true);
            sButton.onClick.AddListener(() => MakeDialogueSelection(onSelection, 1));
            dButton.gameObject.SetActive(true);
            dButton.onClick.AddListener(() => MakeDialogueSelection(onSelection, 2));

            aText.text = options[0].content;
            sText.text = options[1].content;
            dText.text = options[2].content;
        }
    }

    public void MakeDialogueSelection(MakeSelectionDelegate onSelection, int index)
    {
        string next = onSelection(index);
        if (next.Equals(""))
        {
            if (typeof(Diary).IsAssignableFrom(note.GetType()))
            {
                note.gameObject.SetActive(false);
            }

            note = null;
            dialogue.enabled = false;
            dialoguePanel.gameObject.SetActive(false);
            diaryPanelObj.SetActive(false);
            diaryText.text = "";
            fpc.enabled = true;
        }
        else
        {
            if (typeof(Diary).IsAssignableFrom(note.GetType()))
            {
                // Found diary
                diaryText.text = next;
            }
            else
            {
                dialogue.text = next;
            }
        }
    }



    public void HideDialogueSelection()
    {
        makeSelectHolder = null;
        optionsHolder = null;
        selectionObj.SetActive(false);
        fpc.GetMouseLook().SetCursorLock(true);
        aButton.onClick.RemoveAllListeners();
        sButton.onClick.RemoveAllListeners();
        dButton.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.E))
        {
            handAnimator.ResetTrigger("Hide");
            handAnimator.SetTrigger("Show");
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            handAnimator.ResetTrigger("Show");
            handAnimator.SetTrigger("Hide");
        }

        if (optionsHolder != null && optionsHolder.Count > 0)
        {
            if (optionsHolder.Count == 2)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    MakeDialogueSelection(makeSelectHolder, 0);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    MakeDialogueSelection(makeSelectHolder, 1);
                }
            }
            else if (optionsHolder.Count == 3)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    MakeDialogueSelection(makeSelectHolder, 0);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    MakeDialogueSelection(makeSelectHolder, 1);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    MakeDialogueSelection(makeSelectHolder, 2);
                }
            }
        }

        if (pickable)
        {
            distanceIndicator.transform.position = pickable.transform.position;
            Marker.sprite = Picking;
            instruction.text = "Click again to DROP\nMousewheel to move";
            HTUI.SetActive(false);
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1") || !pickable.isPickedUp)
            {
                handAnimator.SetBool("IsCasting", false);
                handAnimator.ResetTrigger("Show");
                handAnimator.SetTrigger("Hide");
                wandEffect.gameObject.SetActive(false);
                //beam.gameObject.SetActive(false);

                pickable.Drop(this);
                pickable = null;
                Marker.sprite = Normal;
                instruction.text = null;
                HTUI.SetActive(false);
            }
        }
        else
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            // For interaction with dialogue
            if (note)
            {
                if (Input.GetButtonDown("Fire1") && !isPaused && !note.isInOption)
                {
                    string next = note.nextLine();
                    if (next.Equals(""))
                    {
                        if (typeof(Diary).IsAssignableFrom(note.GetType()))
                        {
                            note.gameObject.SetActive(false);
                            gameManager.SetFoundThisDiary();
                            print("Got this level's diary " + gameManager.GetFoundThisDiary());
                        }

                        note = null;
                        dialogue.enabled = false;
                        dialoguePanel.gameObject.SetActive(false);
                        diaryPanelObj.SetActive(false);
                        diaryText.text = "";
                        fpc.enabled = true;
                    }
                    else
                    {
                        if (typeof(Diary).IsAssignableFrom(note.GetType()))
                        {
                            // Found diary
                            diaryText.text = next;
                        }
                        else
                        {
                            dialogue.text = next;
                        }
                    }
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, wandRange))
                {
                    Note description = hit.collider.gameObject.GetComponent<Note>();
                    if (description && (!description.getIsFinished() || description.canRepeat))
                    {
                        dialoguePanel.gameObject.SetActive(true);
                        dialogue.enabled = true;
                        dialogue.text = description.note;

                        if (Input.GetButtonDown("Fire1") && description.canStartParagraph)
                        {

                            if (description.paragraph.Count != 0) {
                                fpc.enabled = false;
                                note = description;

                                if (typeof(Diary).IsAssignableFrom(note.GetType()))
                                {
                                    dialoguePanel.gameObject.SetActive(false);
                                    dialogue.text = "";
                                    dialogue.enabled = false;

                                    gameManager.UpdateDiaryAmount();
                                    print("Got diary amount " + gameManager.GetDiaryAmount());
                                    diaryPanelObj.SetActive(true);
                                    diaryText.text = note.nextLine();
                                }
                                else
                                {
                                    dialogue.text = note.nextLine();
                                }
                            }
                            else  // Don't have paragraphs
                            {
                                description.finished = true;
                            }
                        }
                    }
                    else
                    {
                        dialoguePanel.gameObject.SetActive(false);
                        dialogue.text = "";
                        dialogue.enabled = false;
                    }
                }
                else
                {
                    dialogue.text = "";
                    dialogue.enabled = false;
                    dialoguePanel.gameObject.SetActive(false);
                }
            }

            if (Physics.Raycast(ray, out hit, wandRange))
            {
                Spoon spoon = hit.collider.gameObject.GetComponent<Spoon>();
                if (spoon && Input.GetButtonDown("Fire1"))
                {
                    spoon.pot.stir();
                }
            }

            int wandableLayer = 1 << LayerMask.NameToLayer("Wandable");
            int pickableLayer = 1 << LayerMask.NameToLayer("Pickable");
            int layerMask = wandableLayer | pickableLayer;
            if (Physics.Raycast(ray, out hit, wandRange, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<Wandable>() || hit.collider.gameObject.GetComponent<Pickable>())
                {
                    Marker.sprite = CanInreract;
                    instruction.text = "Click to Pick\nE to Hitte";
                    HTUI.SetActive(false);
                }
                else
                {
                    Marker.sprite = Normal;
                    instruction.text = null;
                    heatText.text = null;
                    HTUI.SetActive(false);
                }

                if (wandable && wandable != hit.collider.gameObject.GetComponent<Wandable>())
                {
                    wandable.OffAiming();
                    wandable = null;
                }

                if (Input.GetKey(KeyCode.E))
                {
                    wandable = hit.collider.gameObject.GetComponent<Wandable>();
                    if (wandable)
                    {
                        Marker.sprite = CanAOrB;
                        instruction.text = "Click to Hitte";
                        HTUI.SetActive(false);
                        wandable.OnAiming();
                        // 
                        if (Input.GetButton("Fire1"))
                        {
                            // Absorb
                            handAnimator.SetBool("IsCasting", true);
                            wandEffect.gameObject.SetActive(true);
                            beam.line = hit.collider.gameObject;

                            if (wandable)
                            {
                                holdingTime += (Time.deltaTime);
                            }
                            absorbFrom(wandable);
                            Marker.sprite = Aing;
                            instruction.text = "Absorbing";
                            HTUI.SetActive(true);
                        }
                        else if (Input.GetButton("Fire2"))
                        {
                            // Release
                            handAnimator.SetBool("IsCasting", true);
                            wandEffect.gameObject.SetActive(true);
                            beam.line = hit.collider.gameObject;

                            if (wandable)
                            {
                                holdingTime += (Time.deltaTime);
                            }
                            releaseTo(wandable);
                            Marker.sprite = Ring;
                            instruction.text = "Releasing";
                            HTUI.SetActive(true);
                        }
                        else
                        {
                            handAnimator.SetBool("IsCasting", false);
                            wandEffect.gameObject.SetActive(false);
                            //beam.gameObject.SetActive(false);

                            holdingTime = 0;
                        }
                    }
                    else
                    {
                        Marker.sprite = CanNotAOrB;
                        instruction.text = "Cannot Hitte";
                        HTUI.SetActive(false);
                        holdingTime = 0;
                    }
                }
                else
                {
                    if (!pickable)
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            pickable = hit.collider.GetComponent<Pickable>();
                            
                            if (pickable && pickable.isPickable)
                            {

                                handAnimator.ResetTrigger("Hide");
                                handAnimator.SetTrigger("Show");
                                handAnimator.SetBool("IsCasting", true);
                                wandEffect.gameObject.SetActive(true);
                                beam.line = hit.collider.gameObject;
                                pickable.Pick(this);
                            }
                            else
                            {
                                pickable = null;
                            }
                        }
                    }

                    if (wandable != null)
                    {
                        wandable.OffAiming();
                        wandable = null;
                        wandEffect.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Marker.sprite = Normal;
                instruction.text = null;
                HTUI.SetActive(false);
                holdingTime = 0;
                wandEffect.gameObject.SetActive(false);
                if (wandable != null)
                {
                    wandable.OffAiming();
                    wandable = null;
                }
            }
        }

        if (heatText)
        {
            heatText.text = ((int)heat).ToString();
            heatText.color = Color.Lerp(Color.blue, Color.red, (heat - minHeat) / (maxHeat - minHeat));
        }

        if (Input.GetKeyDown(KeyCode.N))
        {

            if (NLT && NLT.loadingScreenPanel && !NLT.isActive)
            {
                //loadingScreenPanel.SetActive(true);
                NLT.gameManager.StartEndingNote();
                NLT.isActive = true;
            }
        }
    }

    /// <summary>
    /// Absorb heat from the given wandable object if it is not null
    /// </summary>
    /// <param name="w">Given wandable object</param>
    public void absorbFrom(Wandable w)
    {
        if (w)
        {
            heat = Mathf.Min(maxHeat, heat + w.heatLose(absorbRatio * holdingTime, maxHeat - heat));
        }
    }

    public void releaseTo(Wandable w)
    {
        if (w)
        {
            heat = Mathf.Max(minHeat, heat - w.heatGain(releaseRatio * holdingTime, heat - minHeat));
        }
    }

    public void SetDialogueText(string content)
    {
        dialoguePanel.gameObject.SetActive(true);
        dialogue.enabled = true;
        dialogue.text = content;
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangePlayerResetPosition CPRP = other.GetComponent<ChangePlayerResetPosition>();
        if (CPRP)
        {
            originalPos = CPRP.resettingPos;
        }
    }

    public void SetWand(int hasGua)
    {
        if (hasGua == 0)
        {
            SetNormalWand();
        }
        else
        {
            SetFrogWand();
        }
    }

    public void SetNormalWand()
    {
        frogWandObj.SetActive(false);
        normalWandObj.SetActive(true);
    }

    public void SetFrogWand()
    {
        frogWandObj.SetActive(true);
        normalWandObj.SetActive(false);
    }
}