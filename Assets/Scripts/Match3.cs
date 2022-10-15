using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Match3 : MonoBehaviour
{
    public ArrayLayout boardLayout;

    [Header("UI Elements")]
    public Sprite[] pieces;
    public RectTransform gameBoard,gameBoard2;

    [Header("Prefabs")]
    public GameObject nodePiece,spv,sph,spp;
    public TMPro.TMP_Text myText;
        public Image fill;
public Node node2;
Point next2;
    int width = 6;
    float xy,yprev,ynext,xnext,xprev,xb,yb,xm,ym;
    int height = 6;
    public float score;
    int[] fills;
    Node[,] board;
   bool box,five;
    List<NodePiece> update;
    List<FlippedPieces> flipped;
    List<NodePiece> dead;

    System.Random random;

    // Start is called before the first frame update
    public void Start()
    {
        StartGame();
    }

    void Update()
    {
        List<NodePiece> finishedUpdating = new List<NodePiece>();
        for (int i = 0; i < update.Count; i++)
        {
            NodePiece piece = update[i];
              piece.Deat2();
            if (!piece.UpdatePiece()) finishedUpdating.Add(piece);
        }
        for (int i = 0; i < finishedUpdating.Count; i++)
        {
            NodePiece piece = finishedUpdating[i];
            FlippedPieces flip = getFlipped(piece);
            NodePiece flippedPiece = null;

            int x = (int)piece.index.x;
            fills[x] = Mathf.Clamp(fills[x]-1, 0, width);

            List<Point> connected = isConnected(piece.index, true);
            bool wasFlipped = (flip != null);

            // If we flipped to make this update
            if (wasFlipped)
            {
                flippedPiece = flip.getOtherPiece(piece);
                AddPoints(ref connected, isConnected(flippedPiece.index, true));
            }               

            // If we didn't make a match
            if(connected.Count == 0)
            {
                // If we flipped
                if(wasFlipped)
                {
                    // Flip back
                    FlipPieces(piece.index, flippedPiece.index, false);
                }
            }

            // If we made a match
            else
            {
                // Remove the node pieces connected
                foreach(Point pnt in connected)
                {
                    Node node = getNodeAtPoint(pnt);
                    NodePiece nodePiece = node.getPiece();
                    if(nodePiece != null)
                    {
                        //nodePiece.gameObject.SetActive(false);
                        nodePiece.Deat();
                        xy=xy+1;
                if(box==true)
                {
                    nodePiece.mode_on();
                    box=false;
                }
if(xm==4)
{
     nodePiece.mode_on2();
     xm=0;
     
}
if(ym>=4)
{
     nodePiece.mode_on3();
     ym=0;
}
nodePiece.transform.parent=gameBoard2;
                //myText.text=score.ToString();
                fill.fillAmount=score/20;

                       nodePiece.name=xy.ToString();
                       if(xy==1)
                       {
                            yprev=nodePiece.transform.position.y;
                            xnext=nodePiece.transform.position.x;
                       }
                       if(xy==2)
                       {
                           ynext=nodePiece.transform.position.y;
                           xprev=nodePiece.transform.position.x;
                           
                       }
                       if(ynext==yprev)
                       {
                             if(xy==2)
                             {
                            spp=Instantiate(sph, new Vector2((xprev+xnext)/2,nodePiece.transform.position.y), Quaternion.identity);
                            spp.transform.parent=gameBoard2;
                      
                             }
                             xm=xm+1;
                             Debug.Log(xm);

                       }
                       else
                       {
                           Debug.Log(ym);
                           ym=ym+1;
                           
                           if(xy==2)
                           {
                            spp=Instantiate(spv, nodePiece.transform.position, Quaternion.identity);
                            spp.transform.parent=gameBoard2;
                                     Debug.Log("|");
                           }
                       }
                        dead.Add(nodePiece);
                    }                 
                        
                    node.SetPiece(null);
                }
               Invoke("Regen",1f);
               Invoke("Regen2",2f);
                //ApplyGravityToBoard();
            }
            // Remove the flip after update
            flipped.Remove(flip);
            update.Remove(piece);
      
         
        }
    }

   
public void Regen()
{
  
//nodePiece.gameObject.SetActive(false); 
}
public void Regen2()
{
   
ApplyGravityToBoard();
}

    void ApplyGravityToBoard()
    {
           score=score+xy;
        xy=0;
        xm=0;
        ym=0;


        for(int x = 0; x<width; x++)
        {
            for(int y = (height-1); y >= 0; y--)
            {
                Point p = new Point(x, y);
                Node node = getNodeAtPoint(p);
                int val = getValueAtPoint(p);

                // if it's not a hole, do nothing
                if (val != 0) continue;

                for(int ny = (y - 1); ny >= -1; ny--)
                {
                    Point next = new Point(x, ny);
                    int nextVal = getValueAtPoint(next);
                    if (nextVal == 0)
                        continue;
                    // if we did not hit an end, but it's not 0 then use this to fill current hole
                    if (nextVal != -1)
                    {
                         node2=node;
                         next2=next;
              
                        Node got = getNodeAtPoint(next);
                        NodePiece piece = got.getPiece();

                        // Set the hole
                        node.SetPiece(piece);
                        
                        update.Add(piece);

                        // Replace the hole
                        got.SetPiece(null);
                    }
                    else // If we hit an end
                    {
                        // Fill the hole with one of the dead pieces
                        int newVal = fillPiece();
                        NodePiece piece;
                        Point fallPnt = new Point(x, (-1 - fills[x]));

                        if (dead.Count > 0)     
                   
                        {  NodePiece revived = dead[0];
                            piece = revived;
                             
                           
                            revived.gameObject.SetActive(true);

                            // Set initial position so the piece will spawn at the top and fall down
                            revived.rect.anchoredPosition = getPositionFromPoint(fallPnt);
                           
                            
                            dead.RemoveAt(0);
                            /**/
                        }
                        else
                        {
                            GameObject obj = Instantiate(nodePiece, gameBoard);
                            NodePiece n = obj.GetComponent<NodePiece>();
                            RectTransform rect = obj.GetComponent<RectTransform>();
                            rect.anchoredPosition = getPositionFromPoint(fallPnt);
                            piece = n;
                        }

                        piece.Initialize(newVal, p, pieces[newVal - 1]);

                        Node hole = getNodeAtPoint(p);
                        hole.SetPiece(piece);
                        ResetPiece(piece);
                        fills[x]++;
                    }                 
                    break;
                }
            }
        }
    }

    FlippedPieces getFlipped(NodePiece p)
    {
        FlippedPieces flip = null;
        for(int i = 0; i < flipped.Count; i++)
        {
            if (flipped[i].getOtherPiece(p) != null)
            {
                flip = flipped[i];
                break;
            }
                
        }
        return flip;
    }

    void StartGame()
    {
        fills = new int[width];
        string seed = getRandomSeed();
        random = new System.Random(seed.GetHashCode());
        update = new List<NodePiece>();
        flipped = new List<FlippedPieces>();
        dead = new List<NodePiece>();

        InitializeBoard();
        VerifyBoard();
        InstantiateBoard();
    }

    void InitializeBoard()
    {
        board = new Node[width, height];
        for(int y=0; y<height; y++)
        {
            for(int x=0; x<width; x++)
            {
                board[x, y] = new Node((boardLayout.rows[y].row[x]) ? -1 : fillPiece(), new Point(x, y));
            }
        }
    }

    void VerifyBoard()
    {
        List<int> remove;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Point p = new Point(x,y);
                int val = getValueAtPoint(p);
                if (val <= 0) continue;

                remove = new List<int>();

                // If we have a match
                while (isConnected(p, true).Count > 0)
                {
                    val = getValueAtPoint(p);
                    if (!remove.Contains(val))
                    {
                        remove.Add(val);
                    }
                    setValueAtPoint(p, newValue(ref remove));
                }
            }
        }
    }

    void InstantiateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = getNodeAtPoint(new Point(x, y));
                int val = node.value;
                if (val <= 0) continue;
                GameObject p = Instantiate(nodePiece, gameBoard);
                NodePiece piece = p.GetComponent<NodePiece>();
                RectTransform rect = p.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(50 + (100 * x), -50 - (100 * y));
                piece.Initialize(val, new Point(x, y), pieces[val - 1]);
                node.SetPiece(piece);
            }
        }
    }

    public void ResetPiece(NodePiece piece)
    {
        piece.ResetPosition();
        
        update.Add(piece);
    }

    public void FlipPieces(Point one, Point two, bool main)
    {
        // If it's a hole, do nothing
        if (getValueAtPoint(one) < 0) return;

        Node nodeOne = getNodeAtPoint(one);
        NodePiece pieceOne = nodeOne.getPiece();
        if (getValueAtPoint(two) > 0)
        {
            Node nodeTwo = getNodeAtPoint(two);
            NodePiece pieceTwo = nodeTwo.getPiece();

            // Flip the pieces
            nodeOne.SetPiece(pieceTwo);
            nodeTwo.SetPiece(pieceOne);

            if(main)
                flipped.Add(new FlippedPieces(pieceOne, pieceTwo));

            update.Add(pieceOne);
            update.Add(pieceTwo);
        }
        else
            ResetPiece(pieceOne);
    }

    List<Point> isConnected(Point p, bool main)
    {
        List<Point> connected = new List<Point>();
        int val = getValueAtPoint(p);

        // Maintain this order for directions to keep loops working
        Point[] directions =
        {
            Point.up,
            Point.right,
            Point.down,
            Point.left
        };

        // Check if there are 2 or more identical shapes
        foreach(Point dir in directions)
        {
            
            List<Point> line = new List<Point>();

            int same = 0;
            
            for(int i=1; i < 3; i++)
            {
            
                Point check = Point.add(p, Point.mult(dir, i));
             
                if(getValueAtPoint(check) == val)
                {
   
                    line.Add(check);
                    same++;
                }
            }

            // If there's more than 1 of the same shape in the direction, we know it's a match
            if(same > 1)
            {
                // Add these points to the connected list
                AddPoints(ref connected, line);
            }
        }

        // Check if we are in the "middle" of 2 identical shapes
        for (int i = 0; i < 2; i++)
        {
            List<Point> line = new List<Point>();
            int same = 0;

            Point[] check = { Point.add(p, directions[i]), Point.add(p, directions[i + 2]) };

            // Check both sides of the piece & if they are the same value, add them to the list
            foreach(Point next in check) {
                if (getValueAtPoint(next) == val)
                {
                    line.Add(next);
                    same++;
                }
            }

            if(same > 1)
            {
                AddPoints(ref connected, line);
            }
        }

        // Check for 2x2
        for (int i=0; i < 4; i++)
        {
            List<Point> square = new List<Point>();

            int same = 0;
            int next = i + 1;
            if(next >= 4)
            {
                next -= 4;
                
            }

            Point[] check = { Point.add(p, directions[i]), Point.add(p, directions[next]), Point.add(p, Point.add(directions[i], directions[next])) };

            // Check all sides of the piece & if they are the same value add to the list
            foreach (Point pnt in check)
            {
                if (getValueAtPoint(pnt) == val)
                {
                    square.Add(pnt);
                    //Debug.Log(square.Count);
                    if(square.Count==2)  
                    {
                      
                       
                    }
                    same++;
                    
                   
                }
            }

            if(same > 2)
            {  box=true;  
            Debug.Log(box);
                AddPoints(ref connected, square);
                
             
            }

        }

        // Check for other matches on current match
        if (main)
        {
            for(int i=0; i < connected.Count; i++)           
                AddPoints(ref connected, isConnected(connected[i], false));            
        }

        return connected;
    }
    
    void AddPoints(ref List<Point> points, List<Point> add)
    {
        foreach(Point p in add)
        {
            bool doAdd = true;
            for(int i = 0; i < points.Count; i++)
            {
                // Don't add points if they're already there
                if (points[i].Equals(p))
                {
                    doAdd = false;
                    break;
                }
            }
            if (doAdd)
            {
                points.Add(p);
            }
        }
    }

    // Return a cube, sphere, cylinder or diamond
    int fillPiece()
    {
        int val = 1;
        val = (random.Next(0, 100) / (100 / pieces.Length))+1;
        return val;
    }

    int getValueAtPoint(Point p)
    {
        // If out of the array, return a hole just in case
        if(p.x < 0 || p.x >= width || p.y < 0 || p.y >= height)
        {
            return -1;
        }
        return board[p.x, p.y].value;
    }

    void setValueAtPoint(Point p, int v)
    {
        board[p.x, p.y].value = v;
    }

    Node getNodeAtPoint(Point p)
    {
        return board[p.x, p.y];
    }

    int newValue(ref List<int> remove)
    {
        List<int> available = new List<int>();
        for(int i =0; i < pieces.Length; i++)
        {
            available.Add(i + 1);
        }
        foreach(int i in remove)
        {
            available.Remove(i);
        }
        if (available.Count <= 0) return 0;
        return available[random.Next(0, available.Count)];
    }

    // Build a 20-char random seed
    string getRandomSeed()
    {
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890@#$%^&*()";

        for(int i=0; i<20; i++)
        {
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }

        return seed;
    }

    public Vector2 getPositionFromPoint(Point p)
    {
        return new Vector2(50+ (100 * p.x), -50 - (100 * p.y));
    }
}

[System.Serializable]

public class Node
{
    
    public int value;
    public Point index;
    NodePiece piece;

    public Node(int v, Point i)
    {
        value = v;
        index = i;
    }

    public void SetPiece(NodePiece p)
    {
        piece = p;
        value = (piece == null) ? 0 : piece.value;
        if (piece == null) return;
        piece.SetIndex(index);
    }
    public NodePiece getPiece()
    {
        return piece;
    }
}

[System.Serializable]
public class FlippedPieces
{
    public NodePiece one;
    public NodePiece two;

    public FlippedPieces(NodePiece o, NodePiece t)
    {
        one = o;
        two = t;
    }

    public NodePiece getOtherPiece(NodePiece p)
    {
        if (p == one)
            return two;
        else if (p == two)
            return one;
        else
            return null;
    }
}