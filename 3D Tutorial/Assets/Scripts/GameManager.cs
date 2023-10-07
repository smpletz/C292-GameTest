using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum CurrentPlayer
    {
        Player1,
        Player2
    }

    CurrentPlayer currentPlayer;
    bool isWinningShotForPlayer1 = false;
    bool isWinningShotForPlayer2 = false;
    int player1BallsRemaining = 7;
    int player2BallsRemaining = 7;

    [SerializeField] TextMeshProGui player1BallsRemaining;
    [SerializeField] TextMeshProGui player2BallsRemaining;
    [SerializeField] TextMeshProGui currentTurnText;
    [SerializeField] TextMeshProGui messageText;

    [SerializeField] GameObject restartButton;

    [SerializeField] Transform headPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = CurrentPlayer.Player1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Scratch()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            if(isWinningShotForPlayer1)
            {
                ScratchOnWinningShot("Player 1");
                return true;
            }
        }
        else
        {
            if (isWinningShotForPlayer2)
            {
                ScratchOnWinningShot("Player 2");
                return true;
            }
        }
        NextPlayerTurn();
        return false;
    }

    void EarlyEightBall()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            Lose("Player 1 Hit in the Eight Ball Too Early and Has Lost!")
        }
        else
        {
            Lose("Player 2 Hit in the Eight Ball Too Early and Has Lost!")
        }
    }
    
    void ScratchOnWinningShot(string player)
    {
        Lose(player + " Scratched on Their Final Shot and Has Lost!")
    }

    void NoMoreBalls(CurrentPlayer player)
    {
        if (player == CurrentPlayer.Player1)
        {
            isWinningShotForPlayer1 = true;
        } 
        else
        {
            isWinningShotForPlayer2 = true;
        }
    }

    bool CheckBall(Ball ball)
    {
        if(ball.IsCueBall())
        {
            if (Scratch())
            {
                return true;
            }
            else{
                return false;
            }
        }
        else if (ball.IsEightBall())
        {
            if (currentPlayer == CurrentPlayer.Player1)
            {
                if (isWinningShotForPlayer1)
                {
                    Win("Player 1");
                    return true;
                }
            }
            else
            {
                if(isWinningShotForPlayer2)
                {
                    Win("Player 2");
                    return true;
                }
            }
            EarlyEightBall();
        }
        else
        {
            //All other logic when not eight or cue ball
            if (ball.IsBallRed())
            {
                player1BallsRemaining--;
                if(player1BallsRemaining <= 0)
                {
                    isWinningShotForPlayer1 = true;
                }
                if(currentPlayer != CurrentPlayer.Player1)
                {
                    NextPlayerTurn();
                }
            }
            else
            {
                player2BallsRemaining--;
                if(player2BallsRemaining <= 0)
                {
                    isWinningShotForPlayer2 = true;
                }
                if(currentPlayer != CurrentPlayer.Player2)
                {
                    NextPlayerTurn();
                }
            }
        }
        return true;
    }

    //potential error, G in GameObject not capitalized in video
    void Lose(string message)
    {
        messageText.GameObject.SetActive(true);
        messageText.text = message;
        restartButton.SetActive(true);
    }

    void Win(string player)
    {
        messageText.GameObject.SetActive(true);
        messageText.text = message + "Has Won!";
        restartButton.SetActive(true);
    }

    void NextPlayerTurn()
    {
        if (CurrentPlayer == CurrentPlayer.Player1)
        {
            currentPlayer = CurrentPlayer.Player2;
            currentTurnText.text = "Current Turn: Player 2";
        }
        else
        {
            currentPlayer = CurrentPlayer.Player1;
            currentTurnText.text = "Current Turn: Player 1";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GameObject.tag == "Ball")
        {
            if(CheckBall(other.GameObject.GetComponent<Ball>()))
            {
                Destroy(other.GameObject)
            }
            else{
                other.GameObject.Transform.position = headPosition.position;
                other.GameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
    }
}
