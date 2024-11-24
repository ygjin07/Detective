using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject EvidenceMenu;
    public GameObject TalkPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Startalk()
    {
        Menu.SetActive(false);
        TalkPanel.SetActive(true);
    }

    public void Finishalk()
    {
        Menu.SetActive(true);
        TalkPanel.SetActive(false);
    }
}
