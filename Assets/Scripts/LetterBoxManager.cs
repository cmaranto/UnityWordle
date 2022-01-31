using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBoxManager : MonoBehaviour
{
    public GameObject gridGo;
    public GameObject letterBoxPrefab;
    public int wordCount = 5;
    public int letterCount = 5;
    LetterBox[,] grid;
    private List<string> words;

    private int currentWord = 0;
    private int currentLetter = 0;
    private string targetWord;
    public Button newGameButton;

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(newGame);
        loadWords();
        createBoard();
        newGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void loadWords()
    {
        words = new List<string>(System.IO.File.ReadAllLines("Assets/Data/5letterwords.txt"));
    }

    void getTargetWord()
    {
        targetWord = words[Random.Range(0, words.Count - 1)];
        Debug.Log(targetWord);
    }

    void clear(){
        currentWord = 0;
        currentLetter = 0;
        foreach(LetterBox lb in grid){
            lb.clear();
        }
    }

    void newGame(){
        clear();
        getTargetWord();
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown)
        {
            if (e.keyCode >= KeyCode.A && e.keyCode <= KeyCode.Z)
            {
                //set current letter and go to next
                if (currentLetter < letterCount) grid[currentWord, currentLetter].letter = e.keyCode.ToString();
                if (++currentLetter > letterCount) currentLetter = letterCount;
            }
            else if (e.keyCode == KeyCode.Backspace)
            {
                //clear current letter
                if (currentLetter == letterCount) --currentLetter;
                grid[currentWord, currentLetter].letter = "";
                if (--currentLetter == -1) currentLetter = 0;
            }
            else if (e.keyCode == KeyCode.Return)
            {
                if (currentLetter != letterCount) return;
                //set correctness
                string word = "";
                for (int i = 0; i < letterCount; ++i)
                {
                    word += grid[currentWord, i].letter;
                }
                word = word.ToLower();
                if (words.Contains(word))
                {
                    //if correct, win
                    if (word == targetWord)
                    {
                        Debug.Log("YOU WIN");
                        for(int i = 0; i < letterCount; ++i){
                            grid[currentWord,i].setCorrectness(LetterBox.Correctness.CorrectPosition);
                        }
                        return;
                    }
                    else if (currentWord == wordCount - 1)
                    {
                        Debug.Log("YOU LOSE");
                        return;
                    }
                    else
                    {
                        //set correctness
                        for(int i = 0; i < letterCount; ++i){
                            if(word[i] == targetWord[i]){
                                grid[currentWord,i].setCorrectness(LetterBox.Correctness.CorrectPosition);
                            }else if(targetWord.Contains(word[i].ToString())){
                                grid[currentWord,i].setCorrectness(LetterBox.Correctness.CorrectLetter);
                            }else{
                                grid[currentWord,i].setCorrectness(LetterBox.Correctness.Incorrect);
                            }
                        }

                        //else go to next word or loss if at last
                        ++currentWord;
                        currentLetter = 0;
                    }


                }
                else
                {
                    Debug.Log("word not found");
                    return;
                }


            }
        }
    }


    void createBoard()
    {
        GridLayoutGroup gl = gridGo.GetComponent<GridLayoutGroup>();
        gl.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gl.constraintCount = wordCount;
        grid = new LetterBox[wordCount, letterCount];
        for (int row = 0; row < wordCount; ++row)
        {
            for (int col = 0; col < letterCount; ++col)
            {
                GameObject go = GameObject.Instantiate<GameObject>(letterBoxPrefab, Vector3.zero, Quaternion.identity);
                go.transform.SetParent(gridGo.transform, false);
                grid[row, col] = go.GetComponent<LetterBox>();
            }
        }
    }
}
