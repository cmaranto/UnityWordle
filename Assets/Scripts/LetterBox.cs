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
    public Color selectedColor = Color.green;
    public Color unselectedColor = Color.black;
    public TMP_Text letterText;

    public string letter{
        get{return letterText.text;}
        set{letterText.SetText(value);}
    }

    private bool m_selected = false;
    public bool selected{
        get{return m_selected;}
        set{
            m_selected = value;
            background.GetComponent<Outline>().effectColor = value ? selectedColor : unselectedColor;
        }
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
