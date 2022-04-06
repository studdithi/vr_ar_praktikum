public class AnsweredQuestion {
    private string _question;

    public string Question {
        get { return _question; }
        set { _question = value; }
    }

    private string _answerA;

    public string AnswerA {
        get { return _answerA; }
        set { _answerA = value; }
    }

   private string _answerB;

    public string AnswerB {
        get { return _answerB; }
        set { _answerC = value; }
    }

    private string _answerC;

    public string AnswerC {
        get { return _answerC; }
        set { _answerC = value; }
    }

   private string _answerD;

    public string AnswerD {
        get { return _answerD; }
        set { _answerD = value; }
    }

 
    private string _correct;

    public string Correct {
        get { return _correct; }
        set { _correct = value; }
    }

    public AnsweredQuestion(string question, string answera, string answerb, string answerc, string answerd, string correct) {
        this._question = question;
        this._answerA = answera;
        this._answerB = answerb;
        this._answerC = answerc;
        this._answerD = answerd;
        this._correct = correct;
    }
}