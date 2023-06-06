using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SlotButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    bool imageFollowCheck;

    static GameObject pointerEnterObject;

    PlayerControls playerControlsScript;
    Vector2 cachedImagePos;
    Vector2 clickedPos;
    Vector2 releasedPos;
    Graphic thisGraphic;

    Sprite draggedItemSprite;
    Sprite none;
    static Sprite pointerEnterSprite;

    Color fullTranparency = new Color(1f, 1f, 1f, 0f);
    Color fullOpacity = new Color(1f, 1f, 1f, 1f);

    private void Start()
    {
        thisGraphic = GetComponent<Image>();
        cachedImagePos = gameObject.transform.localPosition;
        playerControlsScript = GameObject.Find("Helmet").GetComponent<PlayerControls>();
        none = gameObject.GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if (imageFollowCheck)
        {
            gameObject.transform.position = playerControlsScript.mousePosition;
        }
    }

    public void OnItemClicked()
    {
            
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clickedPos = playerControlsScript.mousePosition;
        draggedItemSprite = gameObject.GetComponent<Image>().sprite;
        if (draggedItemSprite)
        {
            imageFollowCheck = true;
        }
        thisGraphic.raycastTarget = false;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        releasedPos = playerControlsScript.mousePosition;

        if (gameObject.GetComponent<Image>().sprite != null && clickedPos == releasedPos)
        {
            
            
            
            gameObject.GetComponent<Image>().sprite = none;
            gameObject.GetComponent<Image>().color = fullTranparency;
        }
        gameObject.transform.localPosition = cachedImagePos;

        if (pointerEnterSprite != null && imageFollowCheck)
        {
            pointerEnterObject.GetComponent<Image>().sprite = draggedItemSprite;
            gameObject.GetComponent<Image>().sprite = pointerEnterSprite;
        }
        else if (pointerEnterSprite == null && pointerEnterObject != null && imageFollowCheck)
        {
            pointerEnterObject.GetComponent<Image>().sprite = draggedItemSprite;
            pointerEnterObject.GetComponent<Image>().color = fullOpacity;
            gameObject.GetComponent<Image>().sprite = none;
            gameObject.GetComponent<Image>().color = fullTranparency;
        }

        imageFollowCheck = false;
        thisGraphic.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        pointerEnterObject = pointerEventData.pointerEnter;
        if (pointerEventData.pointerEnter.GetComponent<Image>())
        {
            if(pointerEventData.pointerEnter.GetComponent<Image>().sprite)
            {
                pointerEnterSprite = gameObject.GetComponent<Image>().sprite;
            }
            else if (!pointerEventData.pointerEnter.GetComponent<Image>().sprite)
            {
                pointerEnterSprite = none;
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        pointerEnterObject = null;
        if (pointerEnterSprite)
        {
            pointerEnterSprite = null;
        }
    }

    






}
