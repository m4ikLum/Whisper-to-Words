using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class PromptController : MonoBehaviour
{
    public TMP_Text promptText;
    int i = -1;
    List<string> words = new List<string>();
    GameObject obj;
    public TMP_Text transcText;
    public string dispPrompt;

    /////////////////////////////////////////
    void Awake() {
        obj = GameObject.FindGameObjectWithTag("Buttony");
    }

    // Start is called before the first frame update
    void Start() {
        ReadString();
        transcText = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().transcriptionText;
    }

    public void ReadString() {
        try 
        {
            using (StreamReader sr = new StreamReader("Assets/vocab_words/vocabList.txt"))
            {
                string word;
               // Read and display lines from the file until the end of
               // the file is reached.
               while ((word = sr.ReadLine()) != null)
               {
                   words.Add(word);
               }
            }
        }
        catch (Exception e)
        {
           // Let the user know what went wrong.
           Console.WriteLine("The file could not be read:");
           Console.WriteLine(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rect bounds = new Rect(340, 285, 310, 45);  // specific to joscie's box
        transcText = obj.GetComponent<LeastSquares.Undertone.RecordButtonUndertone>().transcriptionText;
        //Console.WriteLine(transcText.ToString());
        // Move onto next word if left-click & inside bounds of prompt box
        if (Input.GetMouseButtonDown(0) && bounds.Contains(Input.mousePosition)) {
            i = i + 1;
           if (i > words.Count - 1)
           {
               i = 0;
           }
           promptText.text = "Say " + words[i];
           dispPrompt = words[i];
           //transcText.text = transcText.ToString();
        }
    }
}
