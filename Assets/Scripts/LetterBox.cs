using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterBox : MonoBehaviour
{
    public Image background;
    public Color incorrectColor = Color.gray;
    public Color correctLetterColor = Color.yellow;
    public Color correctPositionColor = Color.green;
    public TMP_Text letterText;

    public string letter{
        get{return letterText.text;}
        set{letterText.SetText(value);}
    }

    public enum Correctness{
        Incorrect,
        CorrectLetter,
        CorrectPosition
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCorrectness(Correctness c){
        switch(c){
            case Correctness.Incorrect:
                background.color = incorrectColor;
                break;
            case Correctness.CorrectLetter:
                background.color = correctLetterColor;
                break;
            case Correctness.CorrectPosition:
                background.color = correctPositionColor;
                break;
        }
    }

    public void clear(){
        letter = "";
        setCorrectness(Correctness.Incorrect);
    }
}
