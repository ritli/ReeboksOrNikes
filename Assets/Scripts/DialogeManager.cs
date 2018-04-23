using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogeManager : MonoBehaviour {

    //public Text npcName;
    public Text dialogeText;
    public Image npcFace;
    public Canvas dialogCanvas;
    public bool isActive;

    private GameObject player;
    private GameObject[] Storys;

    private Queue<string> sentences;

	void Start () {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void startDialoge(Dialoge dialoge)
    {
        isActive = true;
        //Debug.Log("yes_1");

        player.GetComponent<Player>().movementDisabled = true;

        dialogCanvas.gameObject.SetActive(true);
        npcFace.sprite = dialoge.sprite;
                
        foreach(string sentence in dialoge.sentences)
        {
           // Debug.Log("yes_2");
            sentences.Enqueue(sentence);
        }

        int i = DisplayNextDialoge();
    }

    public int DisplayNextDialoge()
    {
        Debug.Log("Sencenses count  " + sentences.Count);

        if(sentences.Count == 0)
        {
            endDialoge();
            return sentences.Count;
        }

        dialogeText.text = sentences.Dequeue();

        return sentences.Count + 1;

        

        //Debug.Log(sentences.Dequeue());

    }

    private void endDialoge()
    {
        isActive = false;
        dialogCanvas.gameObject.SetActive(false);
        player.GetComponent<Player>().movementDisabled = false;
       

    }


}
