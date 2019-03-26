using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wand : MonoBehaviour {

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

    private float holdingTime;

    // Data that needs to reset
    [HideInInspector]public float originalHeat;
    [HideInInspector]public Vector3 originalPos;

    //FOR MARKER
    public Sprite Normal;
    public Sprite CanInreract;
    public Sprite CanAOrB;
    public Sprite CanNotAOrB;
    public Sprite Aing;
    public Sprite Ring;
    public Sprite Picking;

    public Image Marker;

    public GameObject dialogueObj;
    public Image dialoguePanel;
    public Text dialogue;


    public void OnReset()
    {
        heat = originalHeat;
        transform.position = originalPos;

        handAnimator.ResetTrigger("Show");
        handAnimator.SetTrigger("Hide");
        wandEffect.gameObject.SetActive(false);
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
            dialoguePanel.enabled = false;
            dialogue.text = "";
        }
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
        wandEffect.gameObject.SetActive(false);
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

        if (pickable)
        {
            distanceIndicator.transform.position = pickable.transform.position;
            Marker.sprite = Picking;
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
            {
                handAnimator.SetBool("IsCasting", false);
                handAnimator.ResetTrigger("Show");
                handAnimator.SetTrigger("Hide");
                wandEffect.gameObject.SetActive(false);

                pickable.Drop(this);
                pickable = null;
                Marker.sprite = Normal;
            }
        }
        else
        {

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, wandRange))
            {
                Note description = hit.collider.gameObject.GetComponent<Note>();
                if (description)
                {
                    dialoguePanel.enabled = true;
                    dialogue.enabled = true;
                    dialogue.text = description.note;
                }
                else
                {
                    dialoguePanel.enabled = false;
                    dialogue.text = "";
                    dialogue.enabled = false;
                }
            }
            else
            {
                dialogue.text = "";
                dialogue.enabled = false;
            }

            int wandableLayer = 1 << LayerMask.NameToLayer("Wandable");
            int pickableLayer = 1 << LayerMask.NameToLayer("Pickable");
            int layerMask = wandableLayer | pickableLayer;
            if (Physics.Raycast(ray, out hit, wandRange, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<Wandable>() || hit.collider.gameObject.GetComponent<Pickable>())
                {
                    Marker.sprite = CanInreract;
                }
                else
                {
                    Marker.sprite = Normal;
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
                        wandable.OnAiming();
                        // 
                        if (Input.GetButton("Fire1"))
                        {
                            // Absorb
                            handAnimator.SetBool("IsCasting", true);
                            wandEffect.gameObject.SetActive(true);

                            if (wandable)
                            {
                                holdingTime += (Time.deltaTime);
                            }
                            absorbFrom(wandable);
                            Marker.sprite = Aing;
                        }
                        else if (Input.GetButton("Fire2"))
                        {
                            // Release
                            handAnimator.SetBool("IsCasting", true);
                            wandEffect.gameObject.SetActive(true);

                            if (wandable)
                            {
                                holdingTime += (Time.deltaTime);
                            }
                            releaseTo(wandable);
                            Marker.sprite = Ring;
                        }
                        else
                        {
                            handAnimator.SetBool("IsCasting", false);
                            wandEffect.gameObject.SetActive(false);

                            holdingTime = 0;
                        }
                    }
                    else
                    {
                        Marker.sprite = CanNotAOrB;
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
                    }
                }
            }
            else
            {
                Marker.sprite = Normal;
                holdingTime = 0;
                if (wandable != null)
                {
                    wandable.OffAiming();
                    wandable = null;
                }
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

}