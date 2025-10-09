using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using TMPro;

// Response structure for deserialization
[System.Serializable]
public class DeepSeekResponse
{
    public string model;
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public int index;
    public Message message;
}

[System.Serializable]
public class Message
{
    public string role;
    public string content;
}

[System.Serializable]
public class RequestWrapper
{
    public string model;
    public bool stream;
    public List<Message> messages;
}

public class UnityAndDeepSeek : MonoBehaviour
{
    // URL of the DeepSeek API
    private string url = "https://api.deepseek.com/chat/completions";
    public string apiKey; 

    public string systemInstructions = "You are an assistant";
    public TMP_Text text;


    // List to store messages
    private List<Message> messages = new List<Message>();

    void Start()
    {   
        messages.Add(new Message { role = "system", content = systemInstructions });
    }

    public void ClickOn()
    {
        string userMessage = text.text; 
        ChatWithDeepSeek(userMessage); 
    }

    public void ChatWithDeepSeek(string message)
    {
        messages.Add(new Message { role = "user", content = message });
        StartCoroutine(SendDeepSeekRequest());
    }

    IEnumerator SendDeepSeekRequest()
    {
        // Construct the JSON payload dynamically using the RequestWrapper class
        string jsonPayload = CreateJsonPayload();

        // Create a UnityWebRequest for the POST request
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        
        // Convert the JSON payload to a byte array and set it as the upload data
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        
        // Set the content type to application/json
        request.SetRequestHeader("Content-Type", "application/json");
        
        // Set the Authorization header with the Bearer token (your API key)
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
        
        // Set the download handler to capture the response
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        // Check for errors in the request
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // If the request is successful, parse the response JSON
            string responseContent = request.downloadHandler.text;
            DeepSeekResponse response = JsonUtility.FromJson<DeepSeekResponse>(responseContent);

            // Extract the content of the message from the response
            string assistantMessage = response.choices[0].message.content;

            // Log the assistant's message content
            Debug.Log("Assistant's Message: " + assistantMessage);

            // Append the assistant's response to the messages
            messages.Add(new Message { role = "assistant", content = assistantMessage });
        }
    }

    // Helper method to create the JSON payload from the messages list using the RequestWrapper class
    private string CreateJsonPayload()
    {
        var request = new RequestWrapper
        {
            model = "deepseek-chat",
            messages = messages,
            stream = false
        };

        // Serialize the entire request using built-in Unity JsonUtility
        return JsonUtility.ToJson(request);
    }
}
