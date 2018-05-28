using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExpandingButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    RectTransform rect;
    bool expand = false, hover = false;

    float time = 0, alpha;

    TMPro.TextMeshProUGUI text;

    public void Click()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        expand = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    // Use this for initialization
    void Start () {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {

        if (hover && !expand)
        {
            text.transform.localPosition = Vector3.Lerp(text.transform.localPosition, Vector2.down * 150, Time.deltaTime * 5);

            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            alpha = Mathf.Clamp01(alpha + Time.deltaTime * 2);

        }
        else
        {
            text.transform.localPosition = Vector3.Lerp(text.transform.localPosition, Vector2.down * 95, Time.deltaTime * 5);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            alpha = Mathf.Clamp01(alpha - Time.deltaTime * 2);

        }



        if (expand)
        {
            time += Time.deltaTime;
            
            //rect.rect.Set(rect.rect.x, rect.rect.y, from.x, from.y);

            if (time > 0.5)
            {
                Vector2 from = rect.rect.size;

                from = Vector2.Lerp(from, new Vector2(Screen.width, Screen.height), Time.deltaTime * 6);

                transform.position = Vector3.Lerp(transform.position, new Vector3(Screen.width / 2, Screen.height / 2), 1);

                rect.ForceUpdateRectTransforms();

                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, from.x);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, from.y);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(Screen.width / 2, Screen.height / 2), time / 0.5f);
            }


            if (time > 2)
            {
                SceneManager.LoadScene(1);
            }
        }

    }
}
