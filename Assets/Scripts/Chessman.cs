using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;
    public int health; 
    public int attack;

    public Sprite crni_vojnik, crni_kralj, crni_strelac;
    public Sprite beli_vojnik, beli_kralj, beli_strelac;


    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Take the instantiated location and adjust transform
        SetCoords();

        //Choose correct sprite based on piece's name
        switch (this.name)
        {
            case "crni_vojnik": this.GetComponent<SpriteRenderer>().sprite = crni_vojnik; player = "black"; break;
            case "crni_kralj": this.GetComponent<SpriteRenderer>().sprite = crni_kralj; player = "black"; break;
            case "crni_strelac": this.GetComponent<SpriteRenderer>().sprite = crni_strelac; player = "black"; break;

            case "beli_vojnik": this.GetComponent<SpriteRenderer>().sprite = beli_vojnik; player = "white"; break;
            case "beli_kralj": this.GetComponent<SpriteRenderer>().sprite = beli_kralj; player = "white"; break;
            case "beli_strelac": this.GetComponent<SpriteRenderer>().sprite = beli_strelac; player = "white"; break;

        }
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 1.343f;
        y *= 1.322f;

        //Add constants (pos 0,0)
        x += -4.0f;
        y += -4.0f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -2.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    public void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player) {

        
        DestroyMovePlates();

        InitiateMovePlates();
        }

    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    private void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "crni_strelac":
            case "beli_strelac":
                LMovePlate();
                break;
            case "crni_kralj":
            case "beli_kralj":
                SurroundMovePlate();
                break;
            case "beli_vojnik":
                WhiteSoldierMovePlate();
                break;
            case "crni_vojnik":
                BlackSoldierMovePlate();
                break;


        }
    }

    public void WhiteSoldierMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
    }

    public void BlackSoldierMovePlate()
    {
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);

    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if(sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if(cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void MovePlateSpawn (int matrixX, int matrixY)
    {
        float x  = matrixX;
        float y = matrixY;

        x *= 1.343f;
        y *= 1.322f;

        x += -4.0f;
        y += -4.0f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.343f;
        y *= 1.322f;

        x += -4.0f;
        y += -4.0f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attacked = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

     public void SetHealth(int value)
    {
        health = value;
    }

    // Get the health of the chess piece
    public int GetHealth()
    {
        return health;
    }

    // Set the attack of the chess piece
    public void SetAttack(int value)
    {
        attack = value;
    }

    // Get the attack of the chess piece
    public int GetAttack()
    {
        return attack;
    }


}