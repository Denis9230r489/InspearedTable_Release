using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public abstract  class Content: InputContent
{
 

    public  Text Info { get; set; }

    public string[] contents;
  
    public abstract TextAsset File { get; set; }
    public abstract int Price { get; set; }

    
    public abstract void ShowContent();

    public Image content;

} 
public class Motivation : Content//2

{
    private static int NumberCont = 15;
    public override int Price { get; set; } = 1500;
    public override TextAsset File { get; set; }
    public Motivation()
    {

        File = (TextAsset)Resources.Load(path: "Content/Motivation"); //�������� ������ �� ����� 

        Info = GameObject.Find("Content").GetComponent<Text>(); // ���������� ��������� Text �� �����

        string file = File.text.ToString(); // �������������� ������ � �����

        contents = file.Split("*"); // ������ ���� ���������
        NumberCont = contents.Length;

    }
    public override void ShowContent()
    {
       
        if (NumberCont > 0)
        {
            NumberCont -= 1;
            int index = NumberCont;
            MoveY(0);

            Info.text = contents[index]; // ���������� ��� ��������� �������

            contents[index].Remove(index); // �������� ����������� ��������� �� ������ ������� 
        }

        else
        {
            Info.text = "����� ��������� ����������!";

        }
    }
}




public class Joke : Content//1
{
    private static int NumberCont = 12;
    public override TextAsset File { get; set; }
 
    public override int Price { get; set; } = 2000;

    public  Joke()
    {
        File = (TextAsset)Resources.Load(path: "Content/Jokes");

        Info = GameObject.Find("Content").GetComponent<Text>();

        string file = File.text.ToString();



        contents = file.Split("*"); // ������ ���������

        NumberCont = contents.Length;

    }
    public override void ShowContent()
    {
        
        if (NumberCont > 0)
        {
            MoveY(0);
            NumberCont -= 1;
            int index = NumberCont;
            Info.text = contents[index];   
        }
        else
        {
            Info.text = "����� �������� ����������!";
        
        }
    }
}


 
public class Story : Content //0
{
    private static int NumberCont = 0;
    public override int Price { get; set; } = 3000;
    public override TextAsset File { get; set; }


    public Story()
    {
        File = (TextAsset)Resources.Load(path: "Content/Story");

        Info = GameObject.Find("Content").GetComponent<Text>();

        string file = File.text.ToString();

        contents = file.Split("*"); // ������ �������

        NumberCont = contents.Length;
    }
    public override void ShowContent()
    {
       

        if (NumberCont > 0)
        {
            MoveY(0);
            int index = NumberCont-1;

            NumberCont -= 1;

            

            Info.text = contents[index];

            contents[index].Remove(index);
        }

        else
        { Info.text = "����� ������ ����� ����������!"; }
    }

    
}


public class InputContent : MonoBehaviour
{
    [SerializeField] private int Pos =0;

    [SerializeField] private AudioClip MaryCrist;

    [SerializeField]  private AudioClip ClickButton;
    public GameObject BoardcContent;

    public Button ButtonBoardContent;

    private Board board;

    private void Awake()
    {
        InitializeContent();
    }
    private Content content = null;
    public  async void InitializeContent()
    {
        content = null;

        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();

        Events.MusicClick.Invoke(MaryCrist);

        BoardcContent.SetActive(true); // �����



       short index = await board.GetCotentType();

        switch (index)
        {


            case 0:
                content = new Story();
                break;
            case 1:
                content = new Joke();
                break;
            case 2:
                content = new Motivation();
                break;
        }

    }

    public void ShowContent()
    {
        
        if (content.Price > 0)
        {
            
            MoveY(0);
           
        
            content.ShowContent();

            StartCoroutine(ScoreCaracter.Instance.ChangeScore(-(content.Price), 50));// ������ �� �������
        }
    }

    protected async void MoveY(float Y) { await BoardcContent.transform.DOLocalMoveY(Y, 1f).Play().AsyncWaitForCompletion(); }
    public void ExitFromBoardContent()
    {

        Events.MusicClick.Invoke(ClickButton);

        if (ScoreCaracter.Instance.Score < content.Price)

            ButtonBoardContent.interactable = false;

        else
        {

            ButtonBoardContent.interactable = true;

        }
        MoveY(5000);



    }


 }
 


