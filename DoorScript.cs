//Edward Cao: 100697845 Feb/01/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public Text doorsafe;
    public AudioClip doorSound;
    private bool isHot;
    private bool isNoisy;
    private bool isSafe;
    private float probabilityTotal = 0f;
    Material doorMat;
    AudioSource audioSource;

    public void setDoorValues(List<float> values) //function to set the door values based on the inputted probability table
    {
        doorMat = GetComponentInChildren<Renderer>().material;
        audioSource = GetComponent<AudioSource>();
        float ranNum = Random.Range(0f,1f); //Generate a number between 0 and 1 to be used in the probability test
        Debug.Log("Random number generated at: " + this + " is " + ranNum);

        for(int i = 0; i < values.Count; i++)
        {
            probabilityTotal = probabilityTotal + values[i]; //increase the total by the probability value stored in the list
            if(probabilityTotal > ranNum) //Check to see if the probability table has caught up to the random number
            {
              //Set the different booleans dependent on what i value we are currently at
             switch (i)
                {
                    case 0:
                        Debug.Log("0");
                        isHot = true;
                        isNoisy = true;
                        isSafe = true;
                        break;
                    case 1:
                        Debug.Log("1");
                        isHot = true;
                        isNoisy = true;
                        isSafe = false;
                        break;
                    case 2:
                        Debug.Log("2");
                        isHot = true;
                        isNoisy = false;
                        isSafe = true;
                        break;
                    case 3:
                        Debug.Log("3");
                        isHot = true;
                        isNoisy = false;
                        isSafe = false;
                        break;
                    case 4:
                        Debug.Log("4");
                        isHot = false;
                        isNoisy = true;
                        isSafe = true;
                        break;
                    case 5:
                        Debug.Log("5");
                        isHot = false;
                        isNoisy = true;
                        isSafe = false;
                        break;
                    case 6:
                        Debug.Log("6");
                        isHot = false;
                        isNoisy = false;
                        isSafe = true;
                        break;
                    case 7:
                        Debug.Log("7");
                        isHot = false;
                        isNoisy = false;
                        isSafe = false;
                        break;
                    default:
                        Debug.Log("Tf how");
                        break;
                }
                break;
            }
            else
            {

            }
        }
    }

    private void OnMouseEnter() //set visual and audio door properties when mouse is hovering over door
    {
        Debug.Log(this + "Hot: " + isHot + " | Noisy: " + isNoisy + " | Safe: " + isSafe);
       if(isHot)
        {
            doorMat.color = Color.red; //change door colour to red if the door is hot
        }
        else
        {
            doorMat.color = Color.blue; //change door colour to blue if door is not hot
        }


       if(isNoisy)
        {
            audioSource.PlayOneShot(doorSound); //if the door is noisy, play the sound
        }

    }
    private void OnMouseExit() //reset door properties when the mouse leaves
    {
        doorMat.color = Color.white;
        audioSource.Stop();
        doorsafe.text = "";
    }

    private void OnMouseDown()
    {
        if (isSafe)
        {
            doorsafe.text = "This door is Safe";
        }
        else
        {
            doorsafe.text = "This door is not Safe";
        }
    }

}
