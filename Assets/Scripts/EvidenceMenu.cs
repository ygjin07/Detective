using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EvidenceMenu : MonoBehaviour
{
    Image[] evidence_imgs;
    public List<EvidenceData> evidences = new List<EvidenceData>();
    GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        Panel = GameObject.Find("Áõ°Å ÆÇ³Ú");
        evidence_imgs = GetComponentsInChildren<Image>();

        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetEvidence(Evidence e)
    {
        if(!evidences.Any(p => p.id == e.GetEvidenceData().id))
        {
            evidences.Add(e.GetEvidenceData());
            evidence_imgs[evidences.Count].sprite = e.GetEvidenceData().sprite;
        }
    }
}
