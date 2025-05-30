using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace LeastSquares.Undertone
{
    /// <summary>
    /// This class represents the record button in the UI and handles the recording and transcribing of audio.
    /// </summary>
    public class RecordButtonUndertone : MonoBehaviour
    {
        public PushToTranscribe _transcriber;
        public RotateBackgroundUndertone _rotate;
        private bool _isRecording;
        public bool added;
        public TMP_Text transcriptionText;
        public string word;
        public int numTest = 0;



        /// <summary>
        /// Adds a listener to the button to call the OnClicked function when clicked.
        /// </summary>
        private void Start()
        {
            var recordButton = GetComponent<Button>();
            recordButton.onClick.AddListener(OnClicked);
        }

        /// <summary>
        /// Handles the logic for when the record button is clicked.
        /// </summary>
        private async void OnClicked() // changed private to public
        {
            var buttonText = GetComponentInChildren<TMP_Text>();
            // If the speech recognition engine is not loaded, do nothing.
          
            
            if (!_transcriber.Engine.Loaded) return;

            if (!_isRecording)
            {
                numTest = numTest + 1;
                added = true;
                // Start recording and change the button text to "Stop".
                buttonText.text = "Stop".ToUpperInvariant() + " " + numTest.ToString();//allScores.ToString();
                _transcriber.StartRecording();
                _isRecording = true;
                _rotate.speed = 10;
            }
            else
            {
                added = false;
                // Stop recording and transcribe the audio.
                buttonText.text = "Transcribing...".ToUpperInvariant();
                GetComponent<Button>().interactable = false;
                _rotate.speed = 0;
                string transcription = await _transcriber.StopRecording();
                transcriptionText.text = transcription;
                word = transcription;
                buttonText.text = "Record".ToUpperInvariant();
                GetComponent<Button>().interactable = true;
                _isRecording = false;
            }
        }
    }
}