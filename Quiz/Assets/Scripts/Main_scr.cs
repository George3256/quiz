using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_scr : MonoBehaviour {
    
    // для контейнера тестов
    public GameObject ContainerTests;

    public Text TextQuestion;

    public Button[] ButtonAnswer;

    int CurrentQuestion;

    string Tests = @"хуй или пизда1.дайте два.хуй.пизда
хуй или пизда2.дайте два.хуй.пизда";
    bool IsClick;

    List<HZ> listHZ;


    //Для контейнера меню
    public GameObject ContainerMenu;

    public Button ButtonStart;


    void Start ()
    {
        InitializationTest();
        ContainerTests.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickButtonStart()
    {
        ContainerTests.SetActive(true);
        ContainerMenu.SetActive(false);
        CurrentQuestion = UnityEngine.Random.Range(0, listHZ.Count - 1);
        ViewQuestion(CurrentQuestion);
        IsClick = true;
    }
    void InitializationTest()
    {
        listHZ = new List<HZ>();
        string[] str = Tests.Split('\n');
        for (int i = 0; i < str.Length; i++)
        {
            listHZ.Add(new HZ(str[i]));
        }  
    }
    void ViewQuestion(int i)
    { 
        TextQuestion.text = listHZ[i].Question;
        int r = UnityEngine.Random.Range(0, listHZ[i].Answers.Count - 1);
        ButtonAnswer[0].GetComponentInChildren<Text>().text = listHZ[i].Answers[r];
        listHZ[i].Answers.RemoveAt(r);
        r = UnityEngine.Random.Range(0, listHZ[i].Answers.Count - 1);
        ButtonAnswer[1].GetComponentInChildren<Text>().text = listHZ[i].Answers[r];
        listHZ[i].Answers.RemoveAt(r);
        r = 0;
        ButtonAnswer[2].GetComponentInChildren<Text>().text = listHZ[i].Answers[r];
    }
    public void ButtonOnClick(int index)
    {
        if (IsClick)
        {
            if (ButtonAnswer[index].GetComponentInChildren<Text>().text == listHZ[CurrentQuestion].CorrectAnswer)
            {
                ButtonAnswer[index].image.color = Color.green;
                IsClick = false;
            }
            else
            {
                ButtonAnswer[index].image.color = Color.red;
                IsClick = false;
            }
        }
        else NextQuestion();
    }

    public void NextQuestion()
    {
        if (listHZ.Count > 1)
        {
            listHZ.RemoveAt(CurrentQuestion);
            ButtonAnswer[2].image.color = Color.white;
            ButtonAnswer[1].image.color = Color.white;
            ButtonAnswer[0].image.color = Color.white;
            CurrentQuestion = UnityEngine.Random.Range(0, listHZ.Count - 1);
            ViewQuestion(CurrentQuestion);
            IsClick = true;
        }
        else
        {
            ContainerTests.SetActive(false);
            ContainerMenu.SetActive(true);
        }
    }
    public void OnClickAll()
    {
        Application.OpenURL("market://details?id=com.company.game");
    }
    public void OnClickEstimate()
    {
        Application.OpenURL("market://details?id=com.company.game");
    }
}
