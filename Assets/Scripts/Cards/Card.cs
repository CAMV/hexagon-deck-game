using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Card", menuName = "Card Game")]
public class Card : ScriptableObject
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

    protected virtual bool CheckCondition()
    {
        return true;
    }

    protected virtual void ExecuteAction()
    {
        if (!CheckCondition())
            return;

    }
}
