using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public sealed class Title:MonoBehaviour
{
    public int x, y;


    private Item _item;


    public Item item
    {
        get
        {


            return _item;
        }
        set
        {


            _item = value;


            Icon.sprite = _item.sprite;

        }

    }


    public Image Icon;

    public Button button;

    private ParticlePopup popup;
    public ParticlePopup Popup
    {

        get
        {
            
            popup = transform.GetChild(0).GetComponent<ParticlePopup>();

            return popup;
        
        }

        private set { }
    
    }


    public Title Left => x > 0 ? Board.Instance.tiles[x - 1, y] : null; // ��������,�������� ����� �������
    public Title Top => y > 0 ? Board.Instance.tiles[x, y - 1] : null;
    public Title Right => x < Board.Instance.With - 1 ? Board.Instance.tiles[x + 1, y] : null;
    public Title Down => y < Board.Instance.Height - 1 ? Board.Instance.tiles[x, y + 1] : null;

    public Title[] Neightbours => new[] // ������ ���� ������� ������� �����
        {

        Left,
        Top,
        Right,
        Down,

        };


    private void Awake() { 

        AddListener();
    }

    public void AddListener() => button.onClick.AddListener(() => Board.Instance.Select(this));




    public List<Title> GetConnectedTiles(List<Title> exclude = null)
    {
        var result = new List<Title> { this, };
        if (exclude == null)
        {

            exclude = new List<Title> { this, };


        }


        else { exclude.Add(this);
        }

        foreach ( var neighbour in Neightbours)
        {

            if (neighbour == null || exclude.Contains(neighbour) || neighbour._item != _item) continue;

            result.AddRange(neighbour.GetConnectedTiles(exclude));

        }

        return result; 
    
    
    }

 

}
