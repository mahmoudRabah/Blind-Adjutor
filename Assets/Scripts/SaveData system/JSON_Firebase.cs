using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON_Firebase : MonoBehaviour
{
    string userDataKey;
    string json;
    Query query;
    List<string> userKey;
    List<string> userValue;

    public List<string> UserKey { get => userKey; set => userKey = value; }
    public List<string> UserValue { get => userValue; set => userValue = value; }
    private void Start ()
    {
        userKey = new List<string> ();
        userValue = new List<string> ();
    }
    public void WriteUserData (IUserData userData)
    {
        json = JsonUtility.ToJson (userData);
        userDataKey = userData.Key;
        RealtimeDBFirebase.Instance.Reference.Child (RealtimeDBFirebase.Instance.TableName).Child (userDataKey).SetRawJsonValueAsync (json);
    }
    public async void GetAllUsersData ()
    {
        UserKey.Clear ();
        UserValue.Clear ();
        query = FirebaseDatabase.DefaultInstance.GetReference (RealtimeDBFirebase.Instance.TableName);
        RealtimeDBFirebase.Instance.Data = await query.GetValueAsync ();
        foreach ( var item in RealtimeDBFirebase.Instance.Data.Children )
        {
            UserKey.Add (item.Key);
            var val = item.Child (RealtimeDBFirebase.Instance.RecordKeyName).GetRawJsonValue ();
            val = val.Substring (1 , val.Length - 2);
            UserValue.Add (val);
        }
        RealtimeDBFirebase.Instance.OnGetFromFirebaseSuccess?.Invoke ();
    }
}
