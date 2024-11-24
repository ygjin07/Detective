using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Talkcontroller : MonoBehaviour
{
    public TMP_Text talkField;
    public TMP_InputField inputField;
    public GameObject EvidencePanel;
    public Button EvidenceBackOffBtn;
    public Button[] EvidenceImageBtn;
    TextGenerater TG;
    Coroutine TextGenCor;
    string emotion;
    string condition;
    EvidenceData evidenceData;

    // Start is called before the first frame update
    void Start()
    {
        TG = new TextGenerater(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendInput()
    {
        if (TextGenCor != null)
        {
            TextGenCor = StartCoroutine(TG.GenerateText("²¿¸¶ ¿ëÀÇÀÚ", inputField.text, emotion, condition, (evidenceData != null) ? evidenceData.description : ""));
        }
    }

    public void OnInput()
    {
        talkField.gameObject.SetActive(false);
        inputField.gameObject.SetActive(true);
        EvidencePanel.SetActive(true);
        EvidenceBackOffBtn.enabled = false;
        for(int i = 0;i < EvidencePanel.GetComponentInChildren<EvidenceMenu>().evidences.Count;i++)
        {
            EvidenceBackOffBtn.enabled = true;
        }
    }

    public void OnTalk()
    {
        talkField.gameObject.SetActive(true);
        inputField.gameObject.SetActive(false);
        for (int i = 0; i < EvidencePanel.GetComponentInChildren<EvidenceMenu>().evidences.Count; i++)
        {
            EvidenceBackOffBtn.enabled = false;
        }
        EvidenceBackOffBtn.enabled = true;
        EvidencePanel.SetActive(false);
    }

    public void ChooseEvidence(Evidence e)
    {
        if (evidenceData != null)
        {
            if (evidenceData.id == e.GetEvidenceData().id)
            {
                evidenceData = null;
            }
            else
            {
                evidenceData = e.GetEvidenceData();
            }
        }
        else
        {

            evidenceData = e.GetEvidenceData();
        }
    }
}
