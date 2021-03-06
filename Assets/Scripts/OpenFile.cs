﻿using UnityEngine;
using System.IO;
using SFB;
using System.Collections;
using UnityEngine.UI;
public class OpenFile : MonoBehaviour
{
    public Text info;
    public Text info_Number;
    public Text info2;
    public Text info3;
    public Text info4;
    public Text info5;
    public Text info6;
    public Text info7;
    public Text info8;

    string[] dataAsArray;
    private string _path;

    public void LoadFile(int option, string file)
    {
        Debug.Log(_path);
        string filePath = Path.Combine(_path + file);

        Debug.Log(filePath);
        if (!string.IsNullOrEmpty(filePath) && filePath.Length != 0)
        {
            string fileContent = File.ReadAllText(filePath);
            Debug.Log(fileContent);
            LoadOptions options = new LoadOptions(option);

            dataAsArray = options.GetData(fileContent);
            Debug.Log(dataAsArray);

            string[] message = options.GetCustomMessage();
            info_Number.text = message[0];
            info.text = message[1];
            info2.text = dataAsArray[0].ToString();
            info3.text = dataAsArray[1].ToString();
            info4.text = dataAsArray[2].ToString();
            info5.text = dataAsArray[3].ToString();
            info6.text = dataAsArray[4].ToString();
            info7.text = dataAsArray[5].ToString();
            info8.text = dataAsArray[6].ToString();
        }
        else
        {
            dataAsArray = new string[1];
            dataAsArray[0] = "File not found! Please navigate to the right directory.";
            Debug.LogError(dataAsArray);
        }
    }

    void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginVertical();

        if (GUILayout.Button("Close this application (Q)") || Input.GetKeyUp("q"))
        {
            ExecuteFunctions(0);
        }

        if (GUILayout.Button("Open Folder (O)"))
        {
            ExecuteFunctions(1);
        }

        if (!string.IsNullOrEmpty(_path) && GUILayout.Button("Ads Contact List (A)"))
        {
            ExecuteFunctions(2);
        }

        if (!string.IsNullOrEmpty(_path) && GUILayout.Button("Ads Interests (I)") || Input.GetKeyUp("i"))
        {
            ExecuteFunctions(3);
        }

        GUILayout.EndVertical();
        GUILayout.Space(20);
        GUILayout.Label(_path);
        GUILayout.EndHorizontal();
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(_path) && Input.GetKeyUp(KeyCode.Q))
        {
            ExecuteFunctions(0);
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            ExecuteFunctions(1);
        }

        if (!string.IsNullOrEmpty(_path) && Input.GetKeyUp(KeyCode.A))
        {
            ExecuteFunctions(2);
        }

        if (!string.IsNullOrEmpty(_path) && Input.GetKeyUp(KeyCode.I))
        {
            ExecuteFunctions(3);
        }
    }

    private void ExecuteFunctions(int function)
    {
        switch (function)
        {
            case 0:
                Application.Quit();
                break;
            case 1:
                string[] paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", false);
                if (paths.Length <= 0 || string.IsNullOrEmpty(paths[0]))
                {
                    return;
                }
                _path = paths[0];
                LoadFile(LoadOptions.ADS_CONTACT_LIST, "/ads/advertisers_who_uploaded_a_contact_list_with_your_information.json");
                break;
            case 2:
                LoadFile(LoadOptions.ADS_CONTACT_LIST, "/ads/advertisers_who_uploaded_a_contact_list_with_your_information.json");
                break;
            case 3:
                LoadFile(LoadOptions.ADS_INTERESTS, "/ads/ads_interests.json");
                break;
            default:
                break;
        }
    }
}
