using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AddIdea_UIController : MonoBehaviour
{

    string storage_path = "";

    GameObject mainpanel;

    Button AddIdea_Button;
    InputField IdeaName_InputField;
    Slider IdeaDiff_Slider;
    InputField IdeaDesc_InputField;

    Toggle CustomFileLoc_Toggle;
    InputField CustomFileLoc_InputField;

    string ideaname;

    float ideadiff;

    string ideadesc;
    /*
    public class Ideas
    {
        public string[] names;
        public float[] diffs;
        public string[] descs;
    };

    Ideas ideas = null;
    */
    string[] allFGtext = { "Title", "IdeaName_Text", "IdeaDiff_Text", "IdeaDesc_Text", "CustomFileLoc_Label", "Saved_Text" };
    //string[] allBG = { "Main" };


    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;

        if (Application.platform == RuntimePlatform.Android)
        {
            GameObject.Find("Platform_Text").GetComponent<Text>().text = "Platform - Android";
        }
        else GameObject.Find("Platform_Text").GetComponent<Text>().text = "Platform - Windows";

        mainpanel = GameObject.Find("Main");

        IdeaName_InputField = GameObject.Find("IdeaName_InputField").GetComponent<InputField>();
        IdeaDiff_Slider = GameObject.Find("IdeaDiff_Slider").GetComponent<Slider>();
        IdeaDesc_InputField = GameObject.Find("IdeaDesc_InputField").GetComponent<InputField>();

        CustomFileLoc_Toggle = GameObject.Find("CustomFileLoc_Toggle").GetComponent<Toggle>();
        CustomFileLoc_InputField = GameObject.Find("CustomFileLoc_InputField").GetComponent<InputField>();

        AddIdea_Button = GameObject.Find("AddIdea_Button").GetComponent<Button>();
        AddIdea_Button.onClick.AddListener(AddIdea);

        GameObject.Find("Black_White").GetComponent<Button>().onClick.AddListener(Black_White);
        GameObject.Find("Black_Cyan").GetComponent<Button>().onClick.AddListener(Black_Cyan);
        GameObject.Find("White_Black").GetComponent<Button>().onClick.AddListener(White_Black);
        GameObject.Find("White_Red").GetComponent<Button>().onClick.AddListener(White_Red);
        GameObject.Find("ImageBG").GetComponent<Button>().onClick.AddListener(ImageBG);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Black_White()
    {
        ChangeColors(Color.black, Color.white);
    }
    void Black_Cyan()
    {
        ChangeColors(Color.black, Color.cyan);
    }
    void White_Black()
    {
        ChangeColors(Color.white, Color.black);
    }
    void White_Red()
    {
        ChangeColors(Color.white, Color.red);
    }
    void ImageBG()
    {
        Color tempc = Color.white;
        tempc.a = 0.5f;
        mainpanel.GetComponent<Image>().color = tempc;
        foreach (string f in allFGtext)
        {
            GameObject.Find(f).GetComponent<Text>().color = Color.blue;
        }
    }

    void ChangeColors(Color bg, Color fg)
    {
        foreach (string f in allFGtext)
        {
            GameObject.Find(f).GetComponent<Text>().color = fg;
        }
        bg.a = 1f;
        mainpanel.GetComponent<Image>().color = bg;
    }

    void AddIdea()
    {
        Debug.Log("Adding Idea...");

        ideaname = IdeaName_InputField.text;
        ideadiff = IdeaDiff_Slider.value;
        ideadesc = IdeaDesc_InputField.text;

        Debug.Log("IdeaName: " + ideaname);
        Debug.Log("IdeaDiff: " + ideadiff);
        Debug.Log("IdeaDesc: " + ideadesc);

        string[] contents = new string[3];
        contents[0] = "Idea Name       : " + ideaname;
        contents[1] = "Idea Difficulty : " + ideadiff;
        contents[2] = "Idea Desc       : " + ideadesc;

        Debug.Log("---------Contents----------");
        foreach (string s in contents)
        {
            Debug.Log(s);
        }
        Debug.Log("---------------------------");

        if (CustomFileLoc_Toggle.isOn == false)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                storage_path = Application.persistentDataPath;
            }
            else storage_path = Application.persistentDataPath;
        } 
        else storage_path = CustomFileLoc_InputField.text;

        if (!Directory.Exists(Path.Combine(storage_path, "Ideas")))
        {
            Directory.CreateDirectory(Path.Combine(storage_path, "Ideas"));
        }
        if (!File.Exists(Path.Combine(storage_path, "Ideas", ideaname + ".txt")))
        {
            FileStream file = File.Create(Path.Combine(storage_path, "Ideas", ideaname + ".txt"));
            file.Close();
        }

        File.WriteAllLines(Path.Combine(storage_path, "Ideas", ideaname + ".txt"), contents);

        GameObject.Find("Saved_Text").GetComponent<Text>().text = ideaname + " added!";

        Debug.Log("Contents written to File: " + Path.Combine(storage_path, "Ideas", ideaname + ".txt"));
    }
}
