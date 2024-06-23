
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;


namespace Cainos.PixelArtPlatformer_VillageProps
{   
    public class Chest : MonoBehaviour
    {
        private UnityWebRequest webRequest;
        public GameObject questionPanel;
        [FoldoutGroup("Reference")]
        public Animator animator;

        [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                animator.SetBool("IsOpened", isOpened);
            }
        }
  
        private bool hasBeenOpened = false;
        private bool isOpened;

        [FoldoutGroup("Runtime"),Button("Open"), HorizontalGroup("Runtime/Button")]
        public async void Open()
        {
            if (!hasBeenOpened)
            {   
                IsOpened = true;
                hasBeenOpened = true;
                Time.timeScale = 0;

                var question = await FetchAsync("https://opentdb.com/api.php?amount=1");
   
                question[0].incorrect_answers.Add(question[0].correct_answer);
                questionPanel.GetComponent<QuestionManager>().DisplayQuestion(question[0].question, question[0].incorrect_answers, question[0].correct_answer);

            }
        }

        [FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
        public void Close()
        {
            IsOpened = false;
        }
        public async Task<List<Result>> FetchAsync(string urlOpenTrivialDatabase)
        {
            try
            {
                using (UnityWebRequest webRequest = UnityWebRequest.Get(urlOpenTrivialDatabase))
                {
                    var asyncOperation = webRequest.SendWebRequest();
                    while (!asyncOperation.isDone)
                    {
                        await Task.Yield(); 
                    }

                    if (webRequest.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Error fetching data: {webRequest.error}");
                        return new List<Result>();
                    }

                    ApiResponse data = JsonUtility.FromJson<ApiResponse>(webRequest.downloadHandler.text);
                    return data.results;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception during fetch: {e.Message}");
                return new List<Result>();
            }
        }


    }
    [Serializable]
    public class ApiResponse
    {
        public int response_code;
        public List<Result> results;
    }

    // Define Result class to match the structure of each result in the JSON
    [Serializable]
    public class Result
    {
        public string type;
        public string difficulty;
        public string category;
        public string question;
        public string correct_answer;
        public List<string> incorrect_answers;
    }
}
