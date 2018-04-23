using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogeManager : MonoBehaviour {

    //public Text npcName;
    public Text dialogeText;
    public Image npcFace;
    public Canvas dialogCanvas;

    private GameObject player;

    private Queue<string> sentences;

	void Start () {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void startDialoge(Dialoge dialoge)
    {   
        player.GetComponent<Player>().movementDisabled = true;

        dialogCanvas.gameObject.SetActive(true);
        npcFace.sprite = dialoge.sprite;
                
        foreach(string sentence in dialoge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextDialoge();
    }

    public void DisplayNextDialoge()
    {
        Debug.Log("Sencenses count  " + sentences.Count);

        if(sentences.Count == 0)
        {
            endDialoge();
            return;
        }

        //Debug.Log(sentences.Dequeue());
        dialogeText.text = sentences.Dequeue();
    }

    private void endDialoge()
    {
        dialogCanvas.gameObject.SetActive(false);
        player.GetComponent<Player>().movementDisabled = false;
        //sentences.Clear();
        //Debug.Log("End Dialoge");
    }

}
