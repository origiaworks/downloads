using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdCreate : MonoBehaviour
{
    [Tooltip("Œ…”")]
    [SerializeField]
    private int _ketasuu = 12;

    //ID‚ğæ“¾‚·‚é
    public string GetId()
    {
        string id = "";

        //36i”‚ÌID‚ğ¶¬‚·‚é
        for (int i = 0; i < _ketasuu; i++)
        {
            id += NumberExchange(Random.Range(0, 36));
        }

        Debug.Log("ID(" + id + ")‚ğæ“¾‚µ‚Ü‚µ‚½B");
        return id;
    }

    private string NumberExchange(int num)
    {
        var value = "";

        switch (num)
        {
            case 0: value = "0"; break;
            case 1: value = "1"; break;
            case 2: value = "2"; break;
            case 3: value = "3"; break;
            case 4: value = "4"; break;
            case 5: value = "5"; break;
            case 6: value = "6"; break;
            case 7: value = "7"; break;
            case 8: value = "8"; break;
            case 9: value = "9"; break;
            case 10: value = "a"; break;
            case 11: value = "b"; break;
            case 12: value = "c"; break;
            case 13: value = "d"; break;
            case 14: value = "e"; break;
            case 15: value = "f"; break;
            case 16: value = "g"; break;
            case 17: value = "h"; break;
            case 18: value = "i"; break;
            case 19: value = "j"; break;
            case 20: value = "k"; break;
            case 21: value = "l"; break;
            case 22: value = "m"; break;
            case 23: value = "n"; break;
            case 24: value = "o"; break;
            case 25: value = "p"; break;
            case 26: value = "q"; break;
            case 27: value = "r"; break;
            case 28: value = "s"; break;
            case 29: value = "t"; break;
            case 30: value = "y"; break;
            case 31: value = "v"; break;
            case 32: value = "w"; break;
            case 33: value = "x"; break;
            case 34: value = "y"; break;
            case 35: value = "z"; break;
            default: value = "0"; break;
        }
        return value;
    }
}
