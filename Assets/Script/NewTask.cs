using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class Task
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public Task(int id, string name, string description)
    {
        this._id = id;
        this._name = name;
        this._description = description;
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}";
    }
}

public class NewTask : MonoBehaviour
{
    private string _folderPath;

    // Start is called before the first frame update
    void Start()
    {
        _folderPath = Path.Combine(Application.persistentDataPath, "UserData");

        // TODO (Dav): The creation of the folder should be done in the application startup. Here if the directory doesn't exist we should throw an exception
        if (!Directory.Exists(_folderPath))
        {
            Debug.Log("Creating Folder");
            Directory.CreateDirectory(_folderPath);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnNewTask()
    {
        int dummyId = 1;
        SaveNewTask();
        Task task = LoadTask(dummyId);
    }

    void SaveNewTask()
    {
        Task task = new Task(1, "Grocery", "Apple: 1\r\nRice: 2kg");
        string jsonTask = JsonUtility.ToJson(task);

        string path = Path.Combine(_folderPath, "task" + task.Id + ".json");
        
        File.WriteAllText(path, jsonTask);
    }

    Task LoadTask(int taskId)
    {
        Task task = null;
        string path = Path.Combine(_folderPath, "task" + taskId + ".json");
        if (File.Exists(path))
        {
            string jsonTask = File.ReadAllText(path);
            task = JsonUtility.FromJson<Task>(jsonTask);
        }
        else
        {
            Debug.Log("No file found at path" + path);
        }
        return task;
    }
}
