using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;

public class RealtimeDBFirebase : MonoBehaviour
{
    DatabaseReference reference;
    [Header ("Firebse realtime database URL")]
    [Space (10)]
    [Tooltip ("For Example : https://project-name.firebaseio.com/")]
    [TextArea] [SerializeField] string dB_URL;
    [Header ("Firebase Data")]
    [Space (10)]
    [SerializeField] string tableName;
    [Space (10)]
    [SerializeField] string recordKeyName;
    [Space (10)]
    [Header ("Firebse realtime database Events")]
    [Space (10)]
    [SerializeField] UnityEvent saveOnFirebase;
    [SerializeField] UnityEvent getFromFirebase;
    [SerializeField] UnityEvent onGetFromFirebaseSuccess;
    [SerializeField] UnityEvent onGetFromFirebaseFailed;
    DataSnapshot data;
    Task<DataSnapshot> task;
    static RealtimeDBFirebase instance;

    public static RealtimeDBFirebase Instance { get => instance; set => instance = value; }
    public Task<DataSnapshot> Task { get => task; set => task = value; }
    public DataSnapshot Data { get => data; set => data = value; }
    public DatabaseReference Reference { get => reference; set => reference = value; }
    public string TableName { get => tableName; set => tableName = value; }
    public UnityEvent OnGetFromFirebaseSuccess { get => onGetFromFirebaseSuccess; set => onGetFromFirebaseSuccess = value; }
    //The key of record that we want to get ex: username => the value on database is "rabah"
    public string RecordKeyName { get => recordKeyName; set => recordKeyName = value; }

    private void Awake ()
    {
        if ( Instance == null )
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start ()
    {
        Reference = FirebaseDatabase.DefaultInstance.GetReferenceFromUrl (dB_URL);
    }
    public void PostToDatabase ()
    {
        saveOnFirebase?.Invoke ();
    }
    public void GetFromDatabase ()
    {
        getFromFirebase?.Invoke ();
        StartCoroutine (RetrieveFromDatabase ());
    }
    private IEnumerator RetrieveFromDatabase ()
    {
        if ( Application.internetReachability == NetworkReachability.NotReachable )
        {
            if ( Task != null )
            {
                yield return new WaitUntil (() => Task.IsCompleted || Task.IsFaulted);
                if ( Task.IsFaulted )
                {
                    // Handle the error...
                    Debug.LogWarning ("Getting data is Failed");
                    //Hint
                    onGetFromFirebaseFailed?.Invoke ();
                }
                else if ( Task.IsCompleted )
                {
                    Data = Task.Result;
                    Debug.LogWarning ("Getting data is Success");
                    OnGetFromFirebaseSuccess?.Invoke ();
                }
            }
            else
            {
                Debug.LogError ("Task Is Null!!!");
            }
        }
        else
        {
            Debug.LogError ("Check the Network");
        }
    }
}
