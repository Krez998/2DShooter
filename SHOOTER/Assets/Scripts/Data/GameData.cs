using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour
{
    [SerializeField] private int _collectCount;

    private void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/ShooterData.dat", FileMode.Create);
        Data data = new Data();
        data.CollectCount = _collectCount;
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/ShooterData.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/ShooterData.dat", FileMode.Open);
            Data data = (Data)binaryFormatter.Deserialize(fileStream);
            _collectCount = data.CollectCount;
            fileStream.Close();
        }
    }

    public int GetData()
    {
        return _collectCount;
    }

    public void SetData(int count)
    {
        _collectCount = count;
        Save();
    }
}
