using UnityEngine;
using TMPro;
using System.IO;


public class NotebookManager : MonoBehaviour
{
    [SerializeField] private string inputText;

    public void ReadStringInput(string input)
    {
        inputText = input;
        Debug.Log(input);
    }
}