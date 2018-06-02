using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterHandler : MonoBehaviour {

    ExpandingButton[] chapters;
    public Transform origin;
    bool buttonsOpen;

	// Use this for initialization
	void Start () {
        chapters = new ExpandingButton[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            chapters[i] = transform.GetChild(i).GetComponent<ExpandingButton>();
        }


	}
	
    public void ToggleChapters()
    {
        buttonsOpen = !buttonsOpen;
    }

	void Update () {

        UpdateChaptersPosition();
	}

    void UpdateChaptersPosition()
    {
        float closed = Screen.height * 1.25f, open = 250, current;

        if (buttonsOpen)
        {
            current = open;
        }
        else
        {
            current = closed;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            chapters[i].transform.position = new Vector2(chapters[i].transform.position.x, Mathf.Lerp(chapters[i].transform.position.y, origin.transform.position.y - current * (i + 1), Time.deltaTime * 5));
        }
    }
}
