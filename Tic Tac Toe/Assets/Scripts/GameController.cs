using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonText;
    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount;

    private int[,] winningPoisitions = new int[8, 3]
    {
        {0,1,2}, {3,4,5}, {6,7,8},
        {0,3,6}, {1,4,7}, {2,5,8},
        {0,4,8}, {2,4,6}
    };

    public void Awake()
    {
        gameOverPanel.SetActive(false);
        setGameControllerOnButtons();
        playerSide = "X";
        moveCount = 0;
    }

    public void setGameControllerOnButtons()
    {
        for(int i=0; i<buttonText.Length; i++)
        {
            buttonText[i].GetComponentInParent<GridSpace>().setGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        for (int i = 0; i < winningPoisitions.GetLength(0); i++)
        {
            if (buttonText[winningPoisitions[i, 0]].text == playerSide &&
                buttonText[winningPoisitions[i,1]].text == playerSide &&
                buttonText[winningPoisitions[i,2]].text == playerSide)
            {
                GameOver();
            }
        }
        if(moveCount >= 9)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "It's a draw!";
        }
        ChangeSides();
    }

    private void GameOver()
    {
        for(int i=0; i<buttonText.Length; i++)
        {
            buttonText[i].GetComponentInParent<Button>().interactable = false;
        }
        gameOverPanel.SetActive(true);
        gameOverText.text = playerSide + " Wins!";
    }

    private void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }
}
