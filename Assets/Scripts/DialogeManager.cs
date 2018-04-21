using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogeManager : MonoBehaviour {

    //public Text npcName;
    public Text dialogeText;
    public Image npcFace;
    public Canvas dialogCanvas;

    private Queue<string> sentences;

	void Start () {
        sentences = new Queue<string>();
	}

    public void startDialoge(Dialoge dialoge)
    {

        dialogCanvas.gameObject.SetActive(true);
        npcFace.sprite = dialoge.sprite;
        //npcName.text = dialoge.name;
        
        foreach(string sentence in dialoge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextDialoge();
        

        //Debug.Log("Pratar med  " + dialoge.name);
    }

    public void DisplayNextDialoge()
    {
        Debug.Log("Sencenses count  " + sentences.Count);

        if(sentences.Count < 1)
        {
            endDialoge();
        }

        //Debug.Log(sentences.Dequeue());
        dialogeText.text = sentences.Dequeue();
        //string sentence = sentences.Dequeue();
    }

    private void endDialoge()
    {
        dialogCanvas.enabled = false;
        Debug.Log("End Dialoge");
    }

}
