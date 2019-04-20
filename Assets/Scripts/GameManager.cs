using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public string username;
    public int maxMessages = 25;
    public GameObject chatPanel, textObject;
    public InputField chatBox;
    public Color playerMessage, info;
    [SerializeField] List<Message> messageList = new List<Message>();
    private bool chatFocused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !chatFocused)
        {
            chatFocused = true;
            chatBox.ActivateInputField();
        } else if (Input.GetKeyDown(KeyCode.Return) && chatFocused)
        {
            if (chatBox.text != "")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //create message to chatbox
                    SendMessageToChat(username + ": " + chatBox.text, Message.MessageType.playerMessage);

                    //reset chat input to blank
                    chatBox.text = "";
                }
            }
            chatFocused = false;
            chatBox.OnDeselect(new BaseEventData(EventSystem.current));
        }

        /*
        if(chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //create message to chatbox
                SendMessageToChat(username + ": " + chatBox.text, Message.MessageType.playerMessage );

                //reset chat input to blank
                chatBox.text = "";
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                //enable typing to chat input
                chatBox.ActivateInputField();
            }
        }

        if(!chatBox.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                chatBox.DeactivateInputField();
            }
        }
        */
    }

    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        //new message object
        Message newMessage = new Message
        {
            //set text to input text
            text = text
        };

        //create text formatting
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageTypeColor(messageType);
        messageList.Add(newMessage);
    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
        }
        return color;
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
    public MessageType messageType;

    public enum MessageType
    {
        playerMessage,
        info
    }
}