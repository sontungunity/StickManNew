using STU;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectionController : Singleton<CollectionController> {
    [SerializeField] private RectTransform defaulTarget;
    [SerializeField] private CollectionItem m_colItemPref;
    [SerializeField] private float deltaDistance;
    [SerializeField] private float timeDelay;
    public RectTransform DefaulTarget => defaulTarget;
    public void GetItemStack(ItemStack itemStack, Vector2 startPosition, Action callback) {
        GetItemStack(itemStack, startPosition, defaulTarget.position, callback);
    }

    public void GetItemStack(ItemStack itemStack, Vector2 startPosition, Vector2 endPosition, Action callback) {
        int amount = itemStack.Amount;
        if(amount == 1) {
            CollectionItem colItem = m_colItemPref.Spawn();
            colItem.GetComponent<RectTransform>().SetParent(transform);
            colItem.Show(itemStack.ItemID.GetDataByID().Icon);
            colItem.Move(startPosition, endPosition, callback);
        } else {
            if(amount > 5) {
                amount = 5;
            }
            float x = 0;
            float y = 0;
            for(int i = 0; i < amount; i++) {
                CollectionItem colItem = m_colItemPref.Spawn();
                colItem.GetComponent<RectTransform>().SetParent(transform);
                colItem.Show(itemStack.ItemID.GetDataByID().Icon);
                x = Random.Range(-deltaDistance, deltaDistance);
                y = Random.Range(-deltaDistance, deltaDistance);
                Vector2 randomPosition = startPosition + new Vector2(x,y);
                if(i == amount - 1) {
                    colItem.Move(startPosition, randomPosition, endPosition, callback, i * timeDelay);
                } else {
                    colItem.Move(startPosition, randomPosition, endPosition, null, i * timeDelay);
                }
            }
        }
    }

    public void GetItemStack(List<ItemStack> lstItemStack, Vector2 startPosition, Action callback) {
        if(lstItemStack == null || lstItemStack.Count == 0) {
            return;
        }
        int amount = 0;
        List<ItemStack> newItemStack = new List<ItemStack>();
        foreach(var itemstack in lstItemStack) {
            amount = itemstack.Amount;
            for(int i = 0; i < amount && i < 6; i++) {
                newItemStack.Add(new ItemStack(itemstack.ItemID, 1));
            }
        }

        float x = 0;
        float y = 0;
        amount = newItemStack.Count;
        for(int i = 0; i < amount; i++) {
            CollectionItem colItem = m_colItemPref.Spawn();
            colItem.GetComponent<RectTransform>().SetParent(transform);
            colItem.Show(newItemStack[i].ItemID.GetDataByID().Icon);
            x = Random.Range(-deltaDistance, deltaDistance);
            y = Random.Range(-deltaDistance, deltaDistance);
            Vector2 randomPosition = startPosition + new Vector2(x,y);
            if(i == amount - 1) {
                colItem.Move(startPosition, randomPosition, defaulTarget.position, callback, i * timeDelay);
            } else {
                colItem.Move(startPosition, randomPosition, defaulTarget.position, null, i * timeDelay);
            }
        }
    }
}
