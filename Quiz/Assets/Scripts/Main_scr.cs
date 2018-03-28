using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_scr : MonoBehaviour {

    private bool inMainMenu;
    // для контейнера тестов
    public GameObject ContainerTests;
    public Text TextQuestion;
    public Button[] ButtonAnswer;
    int CurrentQuestion;
    string Tests = @"Вас может соблазнить такая присущая ему (ей) черта, как.провокативность.сдержанность.загадочность.нежность.
Для вас любовь втроем.это фантазия, которую можно воплотить в жизнь.шокирующая идея.заманчивый опыт (или уже испытанный).настоящий секс – дело двоих.
О своей сексуальной жизни вы могли бы сказать, что она главным образом состоит из.подвигов.упоительных мгновений.обещаний.нежных ласк.
Вашим секс-гаджетом мог бы стать (или является).набор игрушек, купленных в секс-шопе.презерватив.зеркало на потолке.шейный платок.
Какова ваша первая ассоциация со словом «сексуальность».Два тела, слившихся воедино.Бутылка шампанского.Слова любви, выраженные языком тела.Райский сад, полный чудесных красок и ароматов.
Каков ваш любимый момент во время близости.Момент оргазма.Я не выделяю какого-то конкретного момента.Предварительные ласки.Состояние нежной расслабленности после оргазма.
Увидев на экране порноактера с рельефной мускулатурой, вы бы сказали.Вот это тело!.Какой ужас!.Они ничего не понимают в настоящей сексуальности.Это не для меня.
Ваш лучший друг (подруга) признается вам, что хотел(а) бы иметь гомосексуальный опыт.Вас очень возбуждает это сообщение.Вам неловко обсуждать с ним (с ней) такую тему.Вы считаете, что ему (ей) нужна помощь психотерапевта.Вы разговариваете с ним (ней) об этом желании.
Ваше сексуальное кредо.Камасутра.Ничего определенного.Главное – мои самые смелые фантазии.Желания любимого(ой) – закон.
Сексуальный партнер в первую очередь должен быть.дерзким и пылким.сдержанным и вселяющим уверенность.страстным и изощренным.нежным и влюбленным.
Помимо вашего партнера вы могли бы заняться сексом с.любовником(цей) старшего возраста.ни с кем.незнакомым человеком.бывшим возлюбленным(ой).
Если бы секс воплотился в ткани, он стал бы.кожей красного цвета.белым хлопком.пурпурным бархатом.шелком пастельных тонов.
Выразить партнеру свои пожелания в сексе – для вас это.азы сексуального общения.немыслимо.язык намеков.непросто, так как вы смущаетесь.
Двое детей играют «в доктора» в стоге сена, cлыша их смех, вы думаете.Все-таки это нездоровые игры.А у них большое будущее!.Чудесная пора невинности!.Какая непосредственность!.
Помните ли вы ту нежность, с которой ваши родители относились друг к другу.Нет, к ним это не относится.Такое было лишь по особым случаям.Такое бывало после каждой их ссоры.Да, с ностальгией вспоминаю об этом.
Какая из частей тела способна вас взволновать больше всего.Половой орган.Кисти рук.Рот.Ягодицы.
Какое из этих мест для вас наиболее эротично.Подъезд дома.Моя спальня.Пляж.Ванная.
Бывают ли у вас эротические сны, от которых вы испытываете чувственное наслаждение.Часто.Никогда.С тех пор как миновал подростковый возраст – нет.Иногда.
Мастурбация.полезна и расслабляет.это постыдное занятие.к ней прибегают за неимением партнера.это удовольствие, которое мы с партнером доставляем друг другу.";
    bool IsClick;
    List<TestCase> listTest;
    List<TestCase> listTestTmp;

    //Для контейнера меню
    public GameObject ContainerMenu;
    public Button ButtonStart;

    //Для контейнера результата:
    public GameObject ContainerResult;

    void Start ()
    {
        listTest = new List<TestCase>(TestCase.generateTestCases(Tests));
        toMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inMainMenu)
            {
                Application.OpenURL("https://play.google.com/store/apps/dev?id=8151310720269665513");
            }
            else toMainMenu();
        }
    }

    public void OnClickButtonStart()
    {
        listTestTmp = new List<TestCase>(listTest);
        ContainerTests.SetActive(true);
        ContainerMenu.SetActive(false);
        CurrentQuestion = UnityEngine.Random.Range(0, listTestTmp.Count - 1);
        ViewQuestion(CurrentQuestion);
        IsClick = true;
        inMainMenu = false;
    }
    void ViewQuestion(int i)
    {
        //текст вопроса:
        int curNumber = listTest.Count - listTestTmp.Count + 1;
        int maxNumber = listTest.Count;
        string curNumberStr = "( " + curNumber + " / " + maxNumber + " ) ";
        TextQuestion.text = curNumberStr + listTestTmp[i].Question;
        //текст ответов:
        string[] answersRandom = listTestTmp[i].getAnswersRandom();
        for(int j=0; j<ButtonAnswer.Length; j++)
        {
           ButtonAnswer[j].GetComponentInChildren<Text>().text = answersRandom[j];
        }
    }
    public void ButtonOnClick(int index)
    {
        string choosenAnswer = ButtonAnswer[index].GetComponentInChildren<Text>().text;
        listTestTmp[CurrentQuestion].checkAnswer(choosenAnswer);
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (listTestTmp.Count > 1)
        {
            listTestTmp.RemoveAt(CurrentQuestion);
            CurrentQuestion = UnityEngine.Random.Range(0, listTestTmp.Count - 1);
            ViewQuestion(CurrentQuestion);
            IsClick = true;
        }
        else
        {
            toResults();
        }
    }

    public void toMainMenu()
    {
        ContainerResult.SetActive(false);
        ContainerTests.SetActive(false);
        ContainerMenu.SetActive(true);
        TestCase.clearResult();
        inMainMenu = true;
    }

    private void toResults()
    {
        //set active:
        ContainerResult.SetActive(true);
        ContainerTests.SetActive(false);
        ContainerMenu.SetActive(false);
        //fill text:
        int type = TestCase.getType();
        Results.Result r = Results.getResult(type);
        string title = r.title;
        string[] description = r.description.Split(new string[] { "Советы: " }, System.StringSplitOptions.None);
        string desc = description[0];
        string adv = description[1];
        UnityEngine.Object o = GameObject.Find("TextTitle");
        o = GameObject.Find("TextTitle").GetComponent<Text>();
        GameObject.Find("TextTitle").GetComponent<Text>().text = "Ваш результат: " + title;
        GameObject.Find("TextDescription").GetComponent<Text>().text = desc;
        GameObject.Find("TextAdvise").GetComponent<Text>().text = "Советы: " + adv;
        //clear counters:
        TestCase.clearResult();
        //
        inMainMenu = false;
    }

    public void testFill(int number)
    {
        ContainerResult.SetActive(true);
        ContainerTests.SetActive(false);
        ContainerMenu.SetActive(false);
        //fill text:
        Results.Result r = Results.getResult(number);
        string title = r.title;
        string[] description = r.description.Split(new string[] { "Советы: " }, System.StringSplitOptions.None);
        string desc = description[0];
        string adv = description[1];
        UnityEngine.Object o = GameObject.Find("TextTitle");
        o = GameObject.Find("TextTitle").GetComponent<Text>();
        GameObject.Find("TextTitle").GetComponent<Text>().text = "Ваш результат: " + title;
        GameObject.Find("TextDescription").GetComponent<Text>().text = desc;
        GameObject.Find("TextAdvise").GetComponent<Text>().text = "Советы: " + adv;
    }

    public void test1()
    {
        testFill(0);
    }
    public void test2()
    {
        testFill(1);
    }
    public void test3()
    {
        testFill(2);
    }
    public void test4()
    {
        testFill(3);
    }

    public void OnClickAll()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8151310720269665513");
    }
    public void OnClickEstimate()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
