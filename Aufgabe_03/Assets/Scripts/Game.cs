using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    // The gamescore
    private int score = 0;

    // Questions and answers  
    private AnsweredQuestion[] answeredQuestion = new AnsweredQuestion[] {
        new AnsweredQuestion("What is the\ndiameter of\nthe earth?", "40.000 km", "6.350 km", "12.700 km", "20.000 m", "C"),
        new AnsweredQuestion("What is the\ncapital\nof Canada?", "Toronto", "Ottawa", "Montreal", "Quebec", "B"),
        new AnsweredQuestion("What is\ncos(π)?", "0", "-1", "+1", "1/2", "B"),
        new AnsweredQuestion("What is the\ncapital\nof Australia?", "Perth", "Melbourne", "Sydney", "Canberra", "D"),
        new AnsweredQuestion("Northern\nIreland\nbelongs to...?", "England", "Great Britain", "the United\nKingdom", "none of these", "C"),
        new AnsweredQuestion("... is not a\nneighbouring\ncountry of\nGermany.", "Poland", "Slovakia", "Czechia", "Luxembourg", "B"),
    };

	public GameObject thumb;

	public GameObject plant;
	private float initialPlantYRotation;

    // Question section
    public GameObject question;
    // Answer sections
    public GameObject answerBoardA;
    public GameObject answerBoardB;
    public GameObject answerBoardC;
    public GameObject answerBoardD;
    // TextMesh for displaying the score
    public TextMesh scoreText;
    // Sound for correct answer
    public AudioClip successSound;
    // Sound for wrong answer
    public AudioClip failureSound;

    // Index of currently asked question
    private int num = -1;

    // AudioSource in the scene
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {        
        audioSource = GetComponent<AudioSource>();
		thumb.SetActive(false);		// Hide thumb on start 
		initialPlantYRotation =  plant.transform.eulerAngles.y;
        SwitchAnswer();
    }

    // Display the next QA tuple
    public void SwitchAnswer() {
        num = (num + 1) % answeredQuestion.Length;
        question.GetComponent<TextMesh>().text = answeredQuestion[num].Question;
        answerBoardA.transform.GetChild(1).GetComponent<TextMesh>().text = answeredQuestion[num].AnswerA;
        answerBoardB.transform.GetChild(1).GetComponent<TextMesh>().text = answeredQuestion[num].AnswerB;
        answerBoardC.transform.GetChild(1).GetComponent<TextMesh>().text = answeredQuestion[num].AnswerC;
        answerBoardD.transform.GetChild(1).GetComponent<TextMesh>().text = answeredQuestion[num].AnswerD;
    }

    // The next variables control timings:
    // gaze timer: how long has the reticle to rest on a answer to be selected
    // answer timer: how long is the correct/wrong answer highlighted
    private bool runGazeTimer = false;
    private float gazingTimeElapsed = 0f;
    private bool runAnswerTimer = false;
    private float answerTimeElapsed = 0f;

    // what was selected by gaze
    private string selection;
    // the board section of the selection
    private GameObject selectedBoard;

    // different answer boards are handled by different event methods (PointerEnter)
    public void GazingAtAnswerA() 
    {
        selection = "A";
        if (!runAnswerTimer) {
            selectedBoard = answerBoardA;
        }
        GazingAtAnswer();
    }

   public void GazingAtAnswerB() {
        selection = "B";
        if (!runAnswerTimer) {
            selectedBoard = answerBoardB;
        }
        GazingAtAnswer();
    }

   public void GazingAtAnswerC() {
        selection = "C";
        if (!runAnswerTimer) {
            selectedBoard = answerBoardC;
        }
        GazingAtAnswer();
    }

   public void GazingAtAnswerD() {
        selection = "D";
        if (!runAnswerTimer) {
            selectedBoard = answerBoardD;
        }
        GazingAtAnswer();
    }

    public void GazingAtAnswer() {
        if (! runAnswerTimer) {
            runGazeTimer = true;
        }
    }

    // PointerExit
    public void NoLongerGazingAtAnswer() {
        runGazeTimer = false;
        gazingTimeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (runGazeTimer) {
            // User gazes on answer
            gazingTimeElapsed += Time.deltaTime;
        }
        if (gazingTimeElapsed > 2.0f) {
            // User chose answer by looking more than 2 secs on it
            runGazeTimer = false;
            gazingTimeElapsed = 0f;
            Highlight();
            runAnswerTimer = true;
        }
        if (runAnswerTimer) {
            // Animate right/wrong answer
            answerTimeElapsed += Time.deltaTime;
            if (correctBoard != null) {
                correctBoard.transform.position = new Vector3(correctBoard.transform.position.x, 
                correctBoard.transform.position.y, correctBoard.transform.position.z - 0.01f);
            }
        }
        if (answerTimeElapsed > 2.0f) {
            // reset everything, go to next question
            runAnswerTimer = false;
            answerTimeElapsed = 0f;
            SwitchAnswer();
            RestoreBoard();
        }
    }


    // Variables for highlighting
    private Material previousMaterial;
    public Material highlightCorrect;
    public Material highlightFalse;

    // Correct answer is also moved in direction of viewer, store former position
    private Vector3 previousPositionCorrectAnswer;
    // Board with correct answer
    private GameObject correctBoard;
    
    // Change highlight of chosen answer
    // Change score
    // Play sounds

    public void Highlight() {
        previousMaterial = selectedBoard.GetComponent<Renderer>().material;
        if (selection.Equals(answeredQuestion[num].Correct)) {
			if(plant.transform.eulerAngles.y != initialPlantYRotation) {
				plant.transform.eulerAngles = new Vector3(transform.eulerAngles.x, plant.transform.eulerAngles.y - 10f, transform.eulerAngles.z);
			}
            selectedBoard.GetComponent<Renderer>().material = highlightCorrect;
            score += 10;
            scoreText.text = "" + score;
            audioSource.PlayOneShot(successSound, 0.7f);
        }
        else {
			thumb.transform.rotation = Quaternion.Euler(180,0,0);		// Set thumb to "wrong" rotation
			plant.transform.eulerAngles = new Vector3(transform.eulerAngles.x, plant.transform.eulerAngles.y + 10f, transform.eulerAngles.z);

            selectedBoard.GetComponent<Renderer>().material = highlightFalse; 
            score -= 10;
            scoreText.text = "" + score;
            audioSource.PlayOneShot(failureSound, 0.7f);
        }
		thumb.SetActive(true);
        switch (answeredQuestion[num].Correct) {
            case "A" : correctBoard = answerBoardA; break;
            case "B" : correctBoard = answerBoardB; break;
            case "C" : correctBoard = answerBoardC; break;
            case "D" : correctBoard = answerBoardD; break;
        }
        previousPositionCorrectAnswer = correctBoard.transform.position;
    }

    // Reset highlight and position of board with correct answer
    public void RestoreBoard() {
        selectedBoard.GetComponent<Renderer>().material = previousMaterial;
        correctBoard.transform.position = previousPositionCorrectAnswer;
		thumb.SetActive(false); // Hide Thumb again
		thumb.transform.rotation = Quaternion.Euler(0,0,0);	// Reset thumb rotation
    }
}
