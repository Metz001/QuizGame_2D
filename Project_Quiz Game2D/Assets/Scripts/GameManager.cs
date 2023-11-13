using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance { get; private set; }
    [SerializeField] private Loader loader;
    private float totalPoints;
    public TMP_Text pointsTxT;
    public TextMeshProUGUI finalPoints;
    public float TotalPoints { get { return totalPoints; } }

    public List<Question> questions;
    public GameObject[] options;
    public int currentQuestion;
    public TMP_Text questionText;

    public bool secondChance = false;

    public void Start()
    {
        if (loader == null)
        {
            loader = GameObject.Find("Loader").GetComponent<Loader>();
        }
        print("start");
        string currentSceneName = SceneManager.GetActiveScene().name;



        if (currentSceneName == "MainMenu")
        {
            // Coloca aquí las acciones que deseas realizar si estás en la escena específica
            Debug.Log("Loaded 'Main Menu'");
        }
        else
        {
            // Coloca aquí las acciones que deseas realizar si no estás en la escena específica
            pointsTxT.text = "000";
            Debug.Log("Loadad " + currentSceneName);
            NextQuestion();
        }

    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "EndGame")
        {

            pointsTxT = GameObject.Find("puntosFinales").GetComponent<TMP_Text>();
            pointsTxT.text = totalPoints.ToString();
        }

    }
    public void AnswerInteraction(bool interaction)
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = interaction;
        }
    }
    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questions[currentQuestion].answers[i];
           
            if(questions[currentQuestion].correctAnswer == i + 1) 
            {
                options[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }
    public void NextQuestion()
    {
        if(questions.Count > 0)
        {
           currentQuestion = Random.Range(0, questions.Count);
           questionText.text = questions[currentQuestion].question;
           secondChance = false;
           SetAnswer();
        }
        else
        {
            loader.LoadSelectedScene(2);
        }   
    }
    
    public void AnswerState(bool answer)
    {
        if (answer)
        {
            if (secondChance)
            {
                Correct(25);
            } 
            else
            {
                Correct(100);
            }
        }
        else
        {            
            if (secondChance)
            {
                Incorrect(50);
            }
            secondChance = true;

        }
       
    }
    public void Incorrect(int points)
    {
        totalPoints -= points;
        pointsTxT.text = totalPoints.ToString();
        Debug.Log(TotalPoints);
    }
    public void Correct(int points)
    {  
        totalPoints += points;
        pointsTxT.text = totalPoints.ToString();
        questions.RemoveAt(currentQuestion);
        NextQuestion();
        Debug.Log(TotalPoints);
    }
}
