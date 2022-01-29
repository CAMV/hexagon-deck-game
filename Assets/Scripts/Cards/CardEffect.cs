using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Game/Card Effect")]
public class CardEffect : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    [SerializeField]
    private Sprite _image;

    [SerializeField]
    private UnityEvent _actions;

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
    }

    public Sprite Image
    {
        get
        {
            return _image;
        }
    }

    public virtual bool CheckCondition()
    {
        return true;
    }

    public virtual void Use()
    {
        if (!CheckCondition())
            return;

    }
}
