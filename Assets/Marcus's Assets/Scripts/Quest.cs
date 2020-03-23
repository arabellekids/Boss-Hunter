using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Interactable))]
public class Quest : MonoBehaviour
{
    public GameObject questMenu;
    public GameObject questObjective;

    public AudioClip acceptSound;
    public AudioClip declineSound;
    public AudioClip offerQuestSound;
    public AudioClip questTextSound;

    Interactable interactable;

    private AudioSource clipSource;
    private bool questAccepted = false;
    // Start is called before the first frame update
    void Start()
    {
        clipSource = GetComponent<AudioSource>();

        interactable = GetComponent<Interactable>();
        interactable.onInteract += OfferQuest;
    }

    private void Update()
    {
        if (questMenu.activeSelf)
        {
            if (Input.GetButtonDown("Submit"))
            {
                AcceptQuest();
            }
            else if (Input.GetButtonDown("DeclineQuest"))
            {
                DeclineQuest();
            }
        }
    }
    void OfferQuest()
    {
        if(questAccepted == false)
        {
            questMenu.SetActive(true);
            if(offerQuestSound != null)
            {
                AudioSource.PlayClipAtPoint(offerQuestSound, transform.position, 1);
            }
            if(questTextSound != null)
            {
                clipSource.Stop();
                clipSource.clip = questTextSound;
                clipSource.Play();
            }
            Time.timeScale = 0;
        }
    }
    public void AcceptQuest()
    {
            questMenu.SetActive(false);
            questObjective.SetActive(true);
            questAccepted = true;
            clipSource.Stop();
            if (acceptSound != null)
            {
                AudioSource.PlayClipAtPoint(acceptSound, transform.position, 1);
            }
            Time.timeScale = 1;
    }
    public void DeclineQuest()
    {
        questMenu.SetActive(false);
        clipSource.Stop();
        if(declineSound != null)
        {
            AudioSource.PlayClipAtPoint(declineSound, transform.position, 1);
        }
        Time.timeScale = 1;
    }
}
