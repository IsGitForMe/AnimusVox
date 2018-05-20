using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserActions : MonoBehaviour
{
    [SerializeField]
    Text textLogin;

    [SerializeField]
    Text textPassword;

    [SerializeField]
    Text textAlarm;

    private string path = @"D:\programUN\AnimusVoxCL\Assets\External\Users.txt";

    private bool SymbolExistance(string pass,int start,int end)
    {
        for (int i = 0; i < pass.Length; i++)
        {
            if (pass[i] < end && pass[i] > start)
            {
                return true;
            }
        }

        return false;
    }

	// Use this for initialization
	void Start ()
    {
        if (File.Exists(path))
        {
            Debug.Log("Users folder exist");
        }
        else
        {
            Debug.Log("Users folder not exist");
        }
	}

    private bool UserExists(string login)
    {
        string[] sArr = File.ReadAllLines(path);

        for (int i = 0; i < sArr.Length; i++)
        {
            string[] user = sArr[i].Split(' ');

            if (user[0] == login)
            {                
                return true;
            }
        }

        return false;
    }

    public void Register()
    {
        string txtLogin = textLogin.text;
        string txtPass = textPassword.text;

        var userExists = UserExists(txtLogin);
        
        if (userExists)
        {
            textAlarm.text = "      This one had already registred";
        }
        else
        {
            if (!PasswordCorrect(txtPass))
            {
                textAlarm.text = "Password might have both lower AND upper letters, any symbol and number";
                return;
            }

            textAlarm.text = " User registred you can enter the game";
            File.AppendAllText(path, "\r\n"+txtLogin +" "+txtPass);                        
        }
    }

    public bool PasswordCorrect(string pass)                    //needs to continued
    {
        bool upperExists = false;
        bool numbsExists = false;
        bool symbolExists = false;
        bool lowerExists = false;

        upperExists = SymbolExistance(pass, 64, 91);
        numbsExists = SymbolExistance(pass, 47, 58);
        symbolExists = SymbolExistance(pass, 32, 48);
        lowerExists = SymbolExistance(pass, 96, 123);
        
        if ((upperExists) && (numbsExists) && (symbolExists) && (lowerExists))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void LogIn()
    {
        string txtLogin = textLogin.text;
        string txtPass = textPassword.text;

        string[] sArr = File.ReadAllLines(path);

        for (int i = 0;i < sArr.Length; i++)
        {
            if (sArr[i] == textLogin.text + " " + textPassword.text)
            {
                Debug.Log("User found");
                Application.LoadLevel("Scenestart");                
                break;
            }
            else
            {
                textAlarm.text = "User not found or uncorrect password\n        Please register or try again";
            }
        }
        
        Debug.Log(textLogin.text + " " + textPassword.text);
        //string login = ;
    }
}
//for (int i = 0; i <= 255; i++)
//        {
//            File.AppendAllText(path, "\r\n"+i+" = "+(char)i);
//        }