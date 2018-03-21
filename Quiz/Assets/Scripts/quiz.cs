using System.Collections;
using System.Collections.Generic;

public class TestCase
{
    static private int[] TYPE_COUNTERS = new int[] { 0, 0, 0, 0 };
    public string Question {get;set;}
	public List<string>  Answers {get;set;}
	public string CorrectAnswer {get;set;}

    /// <summary>
    /// Метод генерирует массив вопросов с ответами на основе ключевой строки
    /// </summary>
    /// <param name="keyString"> ключевая строка </param>
    /// <returns></returns>
    public static TestCase[] generateTestCases(string keyString)
    {
        string[] stringTestCases = keyString.Split(new string[] { "\n", "\r" }, System.StringSplitOptions.RemoveEmptyEntries);
        TestCase[] result = new TestCase[stringTestCases.Length];
        for(int i=0; i<stringTestCases.Length;i++)
        {
            result[i] = new TestCase(stringTestCases[i]);
        }
        return result;
    }

    public static int getType()
    {
        int maxVal = TYPE_COUNTERS[0];
        int maxI = 0;
        for(int i=1; i< TYPE_COUNTERS.Length; i++)
        {
            if(TYPE_COUNTERS[i] > maxVal)
            {
                maxVal = TYPE_COUNTERS[i];
                maxI = i;
            }
        }
        return maxI;
    }

    /// <summary>
    /// Возвращает массив ответов на вопрос в случайном порядке
    /// </summary>
    /// <returns></returns>
    public string[] getAnswersRandom()
    {
        List<string> answersList = new List<string>(Answers);
        int count = answersList.Capacity;
        //random replaces:
        for (int i=0; i<count; i++)
        {
            int index1 = (int)UnityEngine.Random.Range(0, count);
            int index2 = (int)UnityEngine.Random.Range(0, count);
            string bucket = answersList[index1];
            answersList[index1] = answersList[index2];
            answersList[index2] = bucket;
        }
        return answersList.ToArray();
    }

    public void checkAnswer(string answer)
    {
        for(int i = 0; i < Answers.Capacity; i++)
        {
            if (answer.Equals(Answers[i]))
            {
                TYPE_COUNTERS[i]++;
                return;
            }
        }
    }

    public static void clearResult()
    {
        TYPE_COUNTERS = new int[] { 0, 0, 0, 0 };
    }
    
	public TestCase(string str)
	{
        Answers = new List<string>();
		string[] s=str.Split(new char[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);
		Question=s[0];
		CorrectAnswer=s[1];
		for(int i=0;i<s.Length - 1;i++)
		{
			Answers.Add(s[1+i]);
		}		
	}
}