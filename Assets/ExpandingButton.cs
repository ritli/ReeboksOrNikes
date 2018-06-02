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

    public Sprite chapterSprite;
    public string sceneName = "Chapter";
    public int sceneIndexToLoad = 1;
    TMPro.TextMeshProUGUI text;
    Image chapterImage;

    Vector2 localOffset;

    public void OnPointerClick(PointerEventData eventData)
    {
        expand = true;

        localOffset = transform.localPosition;

        print(localOffset);


        transform.parent = transform.parent.parent;
        //transform.parent = FindObjectOfType<ChapterHandler>().transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    void Start () {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = sceneName;
        rect = GetComponent<RectTransform>();
        chapterImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();

        localOffset = transform.localPosition;
    }
#if UNITY_EDITOR
    void OnValidate()
    {

        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = chapterSprite;
    }
#endif
    void Update () {

        if (hover && !expand)
        {
            text.transform.localPosition = Vector3.Lerp(text.transform.localPosition, Vector2.down * 105, Time.deltaTime * 5);

            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            alpha = Mathf.Clamp01(alpha + Time.deltaTime * 2);

            chapterImage.color = Color.Lerp(chapterImage.color, Color.white, Time.deltaTime * 5);

        }
        else
        {
            text.transform.localPosition = Vector3.Lerp(text.transform.localPosition, Vector2.down * 95, Time.deltaTime * 10);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            alpha = Mathf.Clamp01(alpha - Time.deltaTime * 4);

            if (expand)
            {
                chapterImage.color = Color.Lerp(chapterImage.color, Color.white, Time.deltaTime * 10);
            }

            else
            {
                chapterImage.color = Color.Lerp(chapterImage.color, Color.white * 0.4f, Time.deltaTime * 5);
            }
        }

        if (expand)
        {
            
            if (time > 0.5)
            {
                Vector2 from = rect.rect.size;

                from = Vector2.Lerp(from, new Vector2(Screen.width, Screen.height), Time.deltaTime * 6);

                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector2.zero, 1f);

                rect.ForceUpdateRectTransforms();

                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, from.x);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, from.y);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector2.zero, time / 0.5f);
            }


            if (time > 1.2)
            {
                SceneManager.LoadScene(sceneIndexToLoad);
            }

            time += Time.deltaTime;
        }

    }
}
