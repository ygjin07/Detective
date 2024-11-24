using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evidence : MonoBehaviour
{
    private EvidenceData data;
    [SerializeField]
    private string description;
    [SerializeField]
    private int id;


    // Start is called before the first frame update
    void Start()
    {
        data = new EvidenceData(GetComponent<Image>().sprite, description, id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EvidenceData GetEvidenceData()
    {
        return data;
    }
}

public class EvidenceData
{
    public Sprite sprite;
    public string description;
    public int id;

    public EvidenceData(Sprite s, string d, int id)
    {
        sprite = s;
        description = d;
        this.id = id;
    }
}