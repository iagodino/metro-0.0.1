using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory inventory;
    public List<Weapon> weapons;
    public List<Key> keys;
    public List<ConsumableItem> items;

    // Start is called before the first frame update
    void Awake()
    {
        
        if(inventory == null)
        {
            inventory = this;
        }
        else if(inventory != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void AddKey(Key key)
    {
        keys.Add(key);
    }

    public bool CheckKey(Key key)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] == key)
            {
                return true;
            }
        }
        return false;
    }

    public void AddItem(ConsumableItem item)
    {
        items.Add(item);
    }

    public  void RemoveItem(ConsumableItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i] == item)
            {
                items.RemoveAt(i);
                break;
            }
        }
    }
}
