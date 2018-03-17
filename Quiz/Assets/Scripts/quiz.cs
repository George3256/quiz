using System.Collections;
using System.Collections.Generic;

public class HZ
{
	public string Question {get;set;}
	public List<string>  Answers {get;set;}
	public string CorrectAnswer {get;set;}

	public HZ(string str)
	{
        Answers = new List<string>();
		string[] s=str.Split('.');
		Question=s[0]+"?";
		CorrectAnswer=s[1];
		for(int i=0;i<s.Length - 1;i++)
		{
			Answers.Add(s[1+i]);
		}		
	}
}