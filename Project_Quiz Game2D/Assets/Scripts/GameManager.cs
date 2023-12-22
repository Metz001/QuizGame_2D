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



    //Posicion de 6 botones 
    private Rect _6buttonOp0 = new Rect(-130f, 134f, 1900f, 80f);
    private Rect _6buttonOp1 = new Rect(-130f, 24f, 1900f, 80f);
    private Rect _6buttonOp2 = new Rect(-130f, -86f, 1900f, 80f);

    //Posición de 3 botones
    private Rect _3buttonOp0 = new Rect(-130f, 85f, 1900f, 200f);
    private Rect _3buttonOp1 = new Rect(-130f, -135f, 1900f, 200f);
    private Rect _3buttonOp2 = new Rect(-130f, -360f, 1900f, 200f);

    //Posición de 4 botones
    private Rect _4buttonOp0 = new Rect(-130f, 110f, 1900f, 140f);
    private Rect _4buttonOp1 = new Rect(-130f, -50f, 1900f, 140f);
    private Rect _4buttonOp2 = new Rect(-130f, -210, 1900f, 140f);
    private Rect _4buttonOp3 = new Rect(-130f, -370f, 1900f, 140f);

    public void Start()
    {
     
        currentQuestion = 0;
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
        for (int i = 0; i < questions[currentQuestion].answers.Length; i++) //antesint i = 0; i < options.Length; i++
        {
            print(questions[currentQuestion].answers.Length);
            switch (currentQuestion)
            {
                case 6: //6 opiones --> Pregunta #2
                    print("Active extra options");
                    options[5].gameObject.SetActive(true);
                    options[4].gameObject.SetActive(true);
                    options[3].gameObject.SetActive(true);
                    changeAnswerSize(2);
                    break;
                case 4: //4 opciones --> Pregunta #10
                    print("Active one option extra");
                    options[5].gameObject.SetActive(true);
                    options[4].gameObject.SetActive(false);
                    options[3].gameObject.SetActive(false);
                    changeAnswerSize(10);
                    break;
                case 11:
                    print("Input field");
                    //respuesta requiere ser gaurdada y revisada manualmente
                    for (int j = 0; j > options.Length; j++)
                        options[j].SetActive(false);
                    //método que active input field y reciba la respuesta. luego enciende los botones 0,1,2
                    break;

                case 13:
                    print("Selección de imágen correcta");
                    //Resize y activa imágen
                    changeAnswerSize(13);
                    break;
                case 14:
                    print("Imágenes");
                    //Resize y activa imágen
                    changeAnswerSize(14);
                    break;
                default:
                    print("Inactive Options");
                    options[3].gameObject.SetActive(false);
                    options[4].gameObject.SetActive(false);
                    options[5].gameObject.SetActive(false);
                    changeAnswerSize(0);
                    break;

            }
            options[i].GetComponent<Answer>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questions[currentQuestion].answers[i];

            if (questions[currentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }
    public void NextQuestion()
    {
        if (questions.Count > 0) //si áún hay preguntas, continuemos
        {
            //currentQuestion = Random.Range(0, questions.Count); //siguiente pregunta en aleatorio
            if (currentQuestion > 1)
                currentQuestion++;
            /*
            Switch case curretnQuestion
                Case 2: activar botones extra
                case 10: activar un botones extra
                Case 11: activar input field
                Case 13: activar imágen o distribución difertente 
                Case 14: Activar imágen para cada opcion
            
             */
            questionText.text = questions[currentQuestion].question; //Texto de la pregunta = la pregunta actual
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
        if (totalPoints > 0)
        {
            totalPoints -= points;
            pointsTxT.text = totalPoints.ToString();
        }

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

    private void changeAnswerSize(int i)
    {
        if (i == 0) //Default, 3 botones
        {
            //Op0
            options[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(_3buttonOp0.x, _3buttonOp0.y);
            options[0].GetComponent<RectTransform>().sizeDelta = new Vector2(_3buttonOp0.width, _3buttonOp0.height);
            //Op1
            options[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(_3buttonOp1.x, _3buttonOp1.y);
            options[1].GetComponent<RectTransform>().sizeDelta = new Vector2(_3buttonOp1.width, _3buttonOp1.height);
            //Op2
            options[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(_3buttonOp2.x, _3buttonOp2.y);
            options[2].GetComponent<RectTransform>().sizeDelta = new Vector2(_3buttonOp2.width, _3buttonOp2.height);
        }
        if (i == 2) //6 Op
        {
            //Op0
            options[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(_6buttonOp0.x, _6buttonOp0.y);
            options[0].GetComponent<RectTransform>().sizeDelta = new Vector2(_6buttonOp0.width, _6buttonOp0.height);
            //Op1
            options[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(_6buttonOp1.x, _6buttonOp1.y);
            options[1].GetComponent<RectTransform>().sizeDelta = new Vector2(_6buttonOp1.width, _6buttonOp1.height);
            //Op2
            options[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(_6buttonOp2.x, _6buttonOp2.y);
            options[2].GetComponent<RectTransform>().sizeDelta = new Vector2(_6buttonOp2.width, _6buttonOp2.height);
        }
        if (i == 10 || i == 13 || i == 14)
        {
            //Op0
            options[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(_4buttonOp0.x, _4buttonOp0.y);
            options[0].GetComponent<RectTransform>().sizeDelta = new Vector2(_4buttonOp0.width, _4buttonOp0.height);
            //Op1
            options[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(_4buttonOp1.x, _4buttonOp1.y);
            options[1].GetComponent<RectTransform>().sizeDelta = new Vector2(_4buttonOp1.width, _4buttonOp1.height);
            //Op2
            options[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(_4buttonOp2.x, _4buttonOp2.y);
            options[2].GetComponent<RectTransform>().sizeDelta = new Vector2(_4buttonOp2.width, _4buttonOp2.height);
            //Op3
            options[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(_4buttonOp3.x, _4buttonOp3.y);
            options[3].GetComponent<RectTransform>().sizeDelta = new Vector2(_4buttonOp3.width, _4buttonOp3.height);

        }


    }
}
