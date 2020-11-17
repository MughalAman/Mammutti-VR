using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizGameManager : MonoBehaviour
{
    // All variables
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    
    [SerializeField]
    private float timeBetweenQuestions = 1f;

    [SerializeField]
    private TextMeshProUGUI questionText = null;
    
    [SerializeField]
    private Button button_A = null;
    
    [SerializeField]
    private Button button_B = null;

    [SerializeField]
    private Button button_C = null;

    [SerializeField]
    private Button button_D = null;

    [SerializeField]
    private TextMeshProUGUI buttonAText = null;
    
    [SerializeField]
    private TextMeshProUGUI buttonBText = null;
    
    [SerializeField]
    private TextMeshProUGUI buttonCText = null;

    [SerializeField]
    private TextMeshProUGUI buttonDText = null;

    private Question currentQuestion;

    [SerializeField]
    private GameObject buttonsGameObject = null;

    [SerializeField]
    private GameObject quizStartUI = null;

    [SerializeField]
    private GameObject quizGameUI = null;

    [SerializeField]
    private string endText = null;

    private int score = 0;

    //Check if unasweredQuestions is null or there is 0 unaswered questions if this returns true then get questions from questions (public Question[] questions;) then select question randomly
    public void StartGame()
    {
        quizStartUI.SetActive(false);
        quizGameUI.SetActive(true);

        if(unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetRandomQuestion();
    }

    //Select & display question randomly from unaswered questions
    void SetRandomQuestion()
    {
        if(unansweredQuestions.Count != 0)
        {         
            int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
            currentQuestion = unansweredQuestions[randomQuestionIndex];

            questionText.text = currentQuestion.question;
            buttonAText.text = "A. " + currentQuestion.button_A_Text;
            buttonBText.text = "B. " + currentQuestion.button_B_Text;
            buttonCText.text = "C. " + currentQuestion.button_C_Text;
            buttonDText.text = "D. " + currentQuestion.button_D_Text;

            button_A.image.color = Color.white;
            button_B.image.color = Color.white;
            button_C.image.color = Color.white;
            button_D.image.color = Color.white;

            buttonAText.color = new Color32(50, 50, 50, 255);
            buttonBText.color = new Color32(50, 50, 50, 255);
            buttonCText.color = new Color32(50, 50, 50, 255);
            buttonDText.color = new Color32(50, 50, 50, 255);
        }
        else
        {
            Debug.Log("No questions left!, Total score: " + score);
            buttonsGameObject.SetActive(false);
            questionText.text = endText + " " + score;
            StartCoroutine(ResetGame());
        }

    }

    //Increase score, remove current question from unasweredQuestion then wait and call SetRandomQuestion function
    IEnumerator ShowNextQuestion()
    {
         score++;
         unansweredQuestions.Remove(currentQuestion);

         yield return new WaitForSeconds(timeBetweenQuestions);
         SetRandomQuestion();
    }

    //Wait for 2 seconds and then reload scene (restart)
    IEnumerator ResetGame()
    {
         yield return new WaitForSeconds(2f);
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
                                                                            //endText + " " + score
    //Wait for 1 second and then disable buttons and show end screen e.g. "Your total score was: 10" after that start ResetGame coroutine
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        buttonsGameObject.SetActive(false);
        questionText.text = endText + " " + score;
        StartCoroutine(ResetGame());
    }

    //Check if user selected the right answer e.g. int pressedButton = 0 = buttonA... if true change colors and start ShowNextQuestion coroutine, else change colors and start EndGame coroutine
    public void UserSelectAnswer(int pressedButton)
    {
        if(currentQuestion.isA && pressedButton == 0)
        {
            Debug.Log("Correct!");
            button_A.image.color = Color.green;
            buttonAText.color = Color.white;
            StartCoroutine(ShowNextQuestion());
        }
        else if (currentQuestion.isB && pressedButton == 1)
        {
            Debug.Log("Correct!");
            button_B.image.color = Color.green;
            buttonBText.color = Color.white;
            StartCoroutine(ShowNextQuestion());
        }
        else if (currentQuestion.isC && pressedButton == 2)
        {
            Debug.Log("Correct!");
            button_C.image.color = Color.green;
            buttonCText.color = Color.white;
            StartCoroutine(ShowNextQuestion());
        }
        else if (currentQuestion.isD && pressedButton == 3)
        {
            Debug.Log("Correct!");
            button_D.image.color = Color.green;
            buttonDText.color = Color.white;
            StartCoroutine(ShowNextQuestion());
        }
        else
        {
            //if pressedButton is e.g. 0 (Button A) then change that buttons colors
            switch (pressedButton)
            {
                case 0:
                    button_A.image.color = Color.red;
                    buttonAText.color = Color.white;        
                break;

                case 1:
                    button_B.image.color = Color.red;
                    buttonBText.color = Color.white;        
                break;

                case 2:
                    button_C.image.color = Color.red;
                    buttonCText.color = Color.white;        
                break;

                case 3:
                    button_D.image.color = Color.red;
                    buttonDText.color = Color.white;        
                break;

                default:

                break;
            }
            Debug.Log("Wrong!, Total score: " + score);
            StartCoroutine(EndGame());
        }
    }

}
