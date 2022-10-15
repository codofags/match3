using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodePiece : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public int value;
    public Animator anim;
    public Point index;
    public Spriteget Spriteget;
    [HideInInspector]
    public Vector2 pos;
    [HideInInspector]
    public RectTransform rect;
    public GameObject board;

    bool updating;

    Image img;
    public GameObject berry;

    public void Initialize(int v, Point p, Sprite piece)
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        value = v;
        SetIndex(p);
        img.sprite = piece;
    }

    public void SetIndex(Point p)
    {
        index = p;
        ResetPosition();
        UpdateName();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
public void Deat()
{
       berry.transform.localScale=new Vector2(1f,1f);
       ///Spriteget.Regen();
       anim.Play("as");
}
public void mode_on()
{
      Spriteget.Mode1();
      value=10;
}
public void mode_on2()
{
      Spriteget.Mode2();
      value=8;
}
public void mode_on3()
{
      Spriteget.Mode2();
      value=9;
}
public void Deat2()
{       
         board=GameObject.Find("GameBoard");
       this.transform.parent=board.transform;
       berry.transform.localScale=new Vector2(1f,1f);
       Spriteget.Regen();
       anim.Rebind();
}
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition()
    {
        pos = new Vector2(50 + (100 * index.x), -50 - (100 * index.y));
    }

    public void MovePosition(Vector2 move)
    {
        rect.anchoredPosition += move * Time.deltaTime * 16f;
    }

    public void MovePositionTo(Vector2 move)
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, move, Time.deltaTime * 16f);
     
    }

    public bool UpdatePiece()
    {
        if(Vector3.Distance(rect.anchoredPosition, pos) > 1)
        {
            //transform.localScale=new Vector2(1.2f,1.2f);
            transform.localScale=new Vector2(1.1f,1.1f);
            MovePositionTo(pos);
            updating = true;
            return true;
        }
        else
        {
            rect.anchoredPosition = pos;
            updating = false;
               
               transform.localScale=new Vector2(1.0f,1f);
            return false;
        }
    }

    void UpdateName()
    {
        transform.name = "Node [" + index.x + ", " + index.y + "]";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // If we're already grabbing a piece, don't grab another one
        if (updating) return;
        MovePieces.instance.MovePiece(this);
        transform.localScale=new Vector2(1f,1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MovePieces.instance.DropPiece();
        transform.localScale=new Vector2(1,1);
    }
}
