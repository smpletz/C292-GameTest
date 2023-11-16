using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Goal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI restartText;
    bool restartable;

    // Start is called before the first frame update
    void Start()
    {
        restartable = false;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.R) && restartable)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            messageText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            restartable = true;
        }
    }
}
