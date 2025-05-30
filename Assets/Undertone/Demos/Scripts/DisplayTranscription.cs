using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Linq;
using System.Text;

public class DisplayTranscription : MonoBehaviour
{
    GameObject obj;
    GameObject promptObj;
    public TMP_Text transcriptText;
    string t;
    string actual;
    List<string[]> scoremat;
    int numTrans;
    int tInd;
    int numPrompts;
    int pInd;

    public string ScoreStr = "0";
    public int Scores = 0;
    int numTestt;
    List<int> scores = new List<int>();
    float avgs;
    bool added2;
    int allScores = 0;
    
    

    //create dictionary to map the numbers to words
    Dictionary<string, string> dict = new Dictionary<string, string>
    {
        {"1", "One"},
        {"2", "Two"},
        {"3", "Three"},
        {"4", "Four"},
        {"5", "Five"},
        {"6","Six"},
        {"7", "Seven"},
        {"8", "Eight"},
        {"9", "Nine"},
        {"10", "Ten"},
        {"11", "Eleven"},
        {"12", "Twelve"},
        {"13", "Thirteen"},
        {"14", "Fourteen"},
        {"15", "Fifteen"},
        {"16", "Sixteen"},
        {"17", "Seventeen"},
        {"18", "Eighteen"},
        {"19", "Nineteen"},
        {"20", "Twenty"},
        {"30", "Thirty"},
        {"40", "Forty"},
        {"50", "Fifty"},
        {"60", "Sixty"},
        {"70", "Seventy"},
        {"80", "Eighty"},
        {"90", "Ninety"},
        {"100", "Hundred"}
        
    };

    //strip all non-numerical characters from a string
    private static string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }

    static List<string[]> importCSV() {
        List<string[]> rows = new List<string[]>();
        string filePath = "Assets/vocab_words/updated3.csv";

        try 
        {
            // Read all lines from csv
            string[] lines = File.ReadAllLines(filePath);

            // Process each line and split by the comma to get individual values
            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                // Add the values to the list of rows
                rows.Add(values);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return rows;
    }

    //remove if more than two letters in transcribed word 
    string removedLetters(string transcribed)
    {
        Dictionary<char,int> letters = new Dictionary<char,int>();
        string after = "";
        foreach (char c in transcribed)
        {
            //count the nunber each letter in string
            if(letters.ContainsKey(c))
            {
                letters[c]++;
            }
            else{
                letters[c] = 1;
            }
            //if we have seen the letter twice or less, add it to the string 
            if (letters[c] < 3)
            {
                after = after + c; 
            }
        }
        return after;
    }

    void Awake() {
        obj = GameObject.FindGameObjectWithTag("Buttony");
        promptObj = GameObject.FindGameObjectWithTag("Prompty");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //transcriptText = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().transcriptionText;
        scoremat = importCSV();
        t = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().word;
        //scores.Add(0);
        added2 = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().added;
        
    }

    //bruh i was trying to get average score but eh
    /*float AvgScore(List<int>list, int nt)
    {
        float avgScore = 0.0f;
        for(int i = 0; i < nt; i++)
        {
            avgScore = avgScore + (float)list[i];
        }
        if(nt == 0)
        return avgScore;
        else{
            avgScore = avgScore / (float)nt;
            return avgScore;
        }
        
    }*/

    // Update is called once per frame
    void Update()
    {
        numTestt = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().numTest;
        actual = promptObj.GetComponent<PromptController>().dispPrompt;
        added2 = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().added;

        t = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().word;

        //checks if transcribed word is number and deletes non-digit char
        for(int i = 0; i < dict.Count; i++)
        {
            if(t.Contains(dict.ElementAt(i).Key))
            {
                t = new string(t.Where(Char.IsDigit).ToArray());
                break;
            } 
        }
        //assigns number digits to number in words using dictionary 
        for(int i = 0; i < dict.Count; i++)
        {
            if(t == dict.ElementAt(i).Key)
            {
                t = dict.ElementAt(i).Value;
                break;
            } 
        }
        
        
        t = new String(t.Where(Char.IsLetter).ToArray()); // only include alphabetic characters
        t = t.ToLower();
        t = removedLetters(t);

        

        numTrans = scoremat.Count; // number of words in transcribed library
        numPrompts = scoremat[0].Length; // number of words prompted

        for (int i = 1; i < numPrompts; i++) {
            //makes two-word prompts into one by deleting space so it can be compared to transcribed word
            actual = new string(actual.Where(char.IsLetter).ToArray());
            actual = actual.ToLower();
            
            if (actual == scoremat[0][i]) {
                pInd = i;
                break;
            }
            else { pInd = -1; }
        }
        
        for (int i = 1; i < numTrans; i++) {
            if (t == scoremat[i][0].ToLower()) {
                tInd = i;
                break;
            }
            else { tInd = -1; }
        }

        //t = t + "\n" + actual; // now have access to prompt + transcribed
        //t = t + "\n" + tInd.ToString();
        if (pInd > 1 && tInd > 1) {
            t = t + "\nScore: " + (string)scoremat[tInd][pInd];
            ScoreStr = (string)scoremat[tInd][pInd];
            /*ScoreStr = GetNumbers(ScoreStr);
            bool b;
            b = int.TryParse(ScoreStr, out Scores);
            if (b)
            {
                Scores = Int32.Parse(ScoreStr);
                //allScores = allScores + Scores;
                
            }*/
            

        }
        else if (t == actual.ToLower() ) {
            t = t + "\nScore: 100";
            Scores = 100;
            //allScores = allScores + Scores;
            
        
        }
        else {t = t + "\nScore: " + "n/a"; }

        
        /*if(added2)
        {
            if(scores.Count + 1 == numTestt)
            {
                scores.Add(Scores);
                allScores = allScores + Scores;

            }
        }*/

        transcriptText.text = t; // + " s:" + Scores.ToString() + " nt:" + numTestt.ToString() + " all:" + allScores.ToString();
        
    }

}
