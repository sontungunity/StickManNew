using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DailySave {
    public List<int> lstIndexReceived;
    public string str_TimeStart;
    public DateTime dateStart => DateTime.Parse(str_TimeStart);

    public DailySave() {
        lstIndexReceived = new List<int>();
        str_TimeStart = DateTime.Today.ToString();
    }

    public int GetCurIndex() {
        TimeSpan time = DateTime.Today - dateStart;
        return (int)time.TotalDays;
    }

    public bool CheckReceived(int index) {
        return lstIndexReceived.Contains(index);
    }

    public void AddIndexDay(int index) {
        lstIndexReceived.Add(index);
        if(index == 6) {
            if(lstIndexReceived.Count >= 7) {
                //EventDispatcher.Dispatch<EventKey.QuestEvent>(new EventKey.QuestEvent(QuestID.CLAIM_DAILY,1));
            }
        }
    }

    public void GetUpDate() {
        int index = GetCurIndex();
        if(index > 6) {
            lstIndexReceived = new List<int>();
            str_TimeStart = DateTime.Today.ToString();
        }
    }
}
