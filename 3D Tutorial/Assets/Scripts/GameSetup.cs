using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiameter;
    float ballDiameterWithBuffer;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;

    // Awake is called before the Start method is called
    private void Awake()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiameter = ballRadius * 2f;
        PlaceAllBalls();
    }

    // Start is called before the first frame update
    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiameter = ballRadius * 2f;
    }

    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();
    }

    void PlaceCueBall()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
    }

    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
    }

    void PlaceRandomBalls()
    {
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;

        void PlaceRedBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
        }

        void PlaceBlueBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemaining--;
        }

        //Outer loop is the 5 rows
        for(int i = 0; i < 5; i++)
        {
            //Inner loop are the balls in each row
            for (int j = 0; j < NumInThisRow; j++)
            {
                //Check ot see if it's the middle spot where the 8 ball goes
                if(i==2 && j==1)
                {
                    PlaceEightBall(currentPosition);
                }
                //If there are red and blue balls remaining, randomly choose one and place it
                else if (redBallsRemaining > 0 && blueBallsRemaining > 0)
                {
                    rand = Random.Range(0,2);
                    if (rand == 0)
                    {
                        PlaceRedBall(currentPosition);
                    }
                    else
                    {
                        PlaceBlueBall(currentPosition);
                    }
                }
                // If only red balls are left, place one
                else if (redBallsRemaining > 0) 
                { 
                    PlaceRedBall(currentPosition);
                }
                // Otherwise, place a blue ball
                else
                {
                    PlaceBlueBall(currentPosition);
                }

                //Move the current position for the next ball in this row to the right
                currentPosition += new Vector3(1,0,0).normalized * ballDiameter;
            }

            //Once all the balls in the row have been placed, move to the next row
            firstInRowPosition += Vector3.back * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius;
            currentPosition = firstInRowPosition;
            NumInThisRow++;
        }
    }
}
