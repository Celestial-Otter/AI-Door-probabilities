//Edward Cao: 100697845 Feb/01/22

//Documentation for location computer files: https://docs.microsoft.com/en-us/dotnet/api/system.io.file?redirectedfrom=MSDN&view=net-6.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class ProbabilityFileFinder : MonoBehaviour
{
    public string fileName;
    public string fileLocation;
    public InputField inputField1;
    public InputField inputField2;
    public Text badFileNotice;
    public List<string[]> probabilityTable = new List<string[]>();
    public List<float> probabilityValues = new List<float>();
    //public string proabilityTable;

    public void storeInfo() //Function finds ands stores probability values from inputted file
    {
        fileName = inputField1.text;
        fileLocation = inputField2.text;

        Debug.Log("File name is: " + fileName);
        Debug.Log("File location is: " + fileLocation);

        //Code to try to access the file
        string path = fileLocation + "\\" + fileName;
        //string path = @"d:\Desktop\AI Assignment 1\AI Assignment 1\Assets\probabilities.txt";
        if (!File.Exists(path)) //If file name or path is invalid
        {
            //print out error message
            badFileNotice.text = "File not found";
            Debug.Log("Bad file name/location: " + path);

        }
        else
        {
            using (StreamReader sr = File.OpenText(path)) // Read the file and print to console
            {
                string s;
                while ((s = sr.ReadLine()) != null) //loop through the provided file
                {
                    //Debug.Log(s);
                    //probabilityTable.Add(s);
                    //string currentLine = s.Replace("\t", " ");
                    string currentLine = s;
                    Debug.Log("Current line is: " + currentLine);

                    string[] subs = currentLine.Split('\t');

                    for(int i = 0; i < subs.Length; i++)
                    {
                        bool isfloat = float.TryParse(subs[i], out float n);
                        if(isfloat) //If the parsed substring can be parsed into a float value, store it in the float list of probabilities
                        {
                            probabilityValues.Add(float.Parse(subs[i]));
                        }
                    }

                    for (int i = 0; i < probabilityValues.Count; i++) //Print out probability list for debugging purposes
                    {
                        Debug.Log("ProbabilityValues at " + i + " Is: " + probabilityValues[i]);
                    }

                    //for (int i = 0; i < probabilityTable.Count; i++)
                    //{
                    //    Debug.Log(probabilityTable[i]);
                    //}
                }

                //Disable the menu stuff
                GameObject fileNameText = GameObject.Find("FileNamePrompt");
                fileNameText.SetActive(false);
                GameObject fileLocationText = GameObject.Find("FileLocationPrompt");
                fileLocationText.SetActive(false);
                GameObject submitButton = GameObject.Find("Button");
                submitButton.SetActive(false);
                GameObject fieldOne = GameObject.Find("InputField");
                fieldOne.SetActive(false);
                GameObject fieldTwo = GameObject.Find("InputField (1)");
                fieldTwo.SetActive(false);
                GameObject canvasBackground = GameObject.Find("CanvasBackground");
                canvasBackground.SetActive(false);
                badFileNotice.text = "";


                for (int i = 0; i < 20; i++) //loop through all the doors and give them the probability list to assign values to themselves
                {
                    GameObject door = GameObject.Find("Door " + "(" + i + ")");
                    DoorScript other = (DoorScript)door.GetComponent(typeof(DoorScript));
                    other.setDoorValues(probabilityValues);

                }
            }
        }
    }
}
