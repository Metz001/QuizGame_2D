using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Answer : MonoBehaviour
{
    public bool isCorrect = false;
    public GameManager gameManager;
    private Button buttonAnswer;

   

    public void Verify_Answer()
    {
        buttonAnswer = gameObject.GetComponent<Button>();
        StartCoroutine(reactionButton(isCorrect));

    }

    IEnumerator reactionButton(bool active)
    {

        if (active)
        {
            print("active Green");
            buttonAnswer.GetComponent<Image>().color = Color.green;
            gameManager.AnswerInteraction(false);
            yield return new WaitForSeconds(2);
            gameManager.AnswerInteraction(true);
            buttonAnswer.GetComponent<Image>().color = Color.white;
            gameManager.AnswerState(true);
            //gameManager.Correct(100);
        }
        else
        {
            print("active Red");
            buttonAnswer.GetComponent<Image>().color = Color.red;
            gameManager.AnswerInteraction(false);
            yield return new WaitForSeconds(2);
            gameManager.AnswerInteraction(true);
            buttonAnswer.GetComponent<Image>().color = Color.white;
            gameManager.AnswerState(false);
        }


    }


}
