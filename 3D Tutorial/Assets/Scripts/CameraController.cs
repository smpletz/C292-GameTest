using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float roationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    [SerializeField] float power;
    [SerializeField] GameObject cueStick;
    private float horizontalInput;
    private bool isTakingShot = false;
    [SerializeField] float maxDrawDistance;
    private float SavedMousePosition;

    Transform cueBall;
    GameManager gameManager;
    [SerializeField] TextMeshProUGUI powerText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if(ball.GetComponent<Ball>().IsCueBall())
            {
                cueBall = ball.transform;
                break;
            }
        }

        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(cueBall != null && !isTakingShot)
        {
            horizontalInput = Input.GetAxis("Mouse x") * roationSpeed * Time.deltaTime;

            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }

        Shoot();
    }

    public void ResetCamera()
    {
        cueStick.SetActive(true);
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }

    void Shoot()
    {
        if(gameObject.GetComponent<Camera>().enabled)
        {
            if (Input.GetButtonDown("Fire 1") && !isTakingShot)
            {
                isTakingShot = true;
                SavedMousePosition = 0f;
            }
            else if (isTakingShot)
            {
                if (SavedMousePosition + Input.GetAxis("Mouse y") <= 0)
                {
                    SavedMousePosition += Input.GetAxis("Mouse y");
                    if (SavedMousePosition <= maxDrawDistance)
                    {
                        SavedMousePosition = maxDrawDistance;
                    }
                    float powerValueNumber = ((SavedMousePosition - 0) / (maxDrawDistance - 0)) * (100 - 0) + 0;
                    int powerValueInt = Mathf.RoundToInt(powerValueNumber);
                    powerText.text = "Power: " + powerValueInt + "%";
                }
                if (Input.GetButtonDown("Fire 1"))
                {
                    Vector3 hitDirection = transform.forward;
                    hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

                    cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power * Mathf.Abs(SavedMousePosition), ForceMode.Impulse);
                    cueStick.SetActive(false);
                    gameManager.SwitchCameras();
                    isTakingShot = false;
                }
            }
        }
    }
}