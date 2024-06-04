using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class JZDatabaseAccess : MonoBehaviour
{
    private readonly MongoClient client = new MongoClient("mongodb+srv://justindraw:justindraw@cluster0.fulmstt.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    public SpriteRenderer[] spr;

    // Start is called before the first frame update
    private void Start()
    {
        database = client.GetDatabase("draw");
        collection = database.GetCollection<BsonDocument>("spriteSheet");

        //Invoke("DownloadDB", 3f);
    }

    private async void DownloadDatabase()
    {
        Task<List<DrawData>> task = GetFromDatabase();
        var result = await task;
        string output = string.Empty;

        foreach (DrawData drawing in result)
        {
            output += drawing.Name;
            Debug.Log(output);
        }

        CreateSpriteFromURI(result[0].DataURI);
    }

    public async Task<List<DrawData>> GetFromDatabase()
    {
        var allSprites = collection.FindAsync(new BsonDocument());
        var spritesAwaited = await allSprites;

        List<DrawData> drawData = new List<DrawData>();

        foreach (var item in spritesAwaited.ToList())
        {
            drawData.Add(Deserialize(item.ToString()));
        }

        return drawData;
    }

    private DrawData Deserialize(string rawJson)
    {
        DrawData drawing = new DrawData();

        string startDataURI = rawJson.Substring(rawJson.IndexOf("iVBORw"));
        string cleanDataURI = startDataURI.Substring(0, startDataURI.Length - 3);

        drawing.DataURI = cleanDataURI;
        return drawing;
    }

    public void CreateSpriteFromURI(string URIstring)
    {
        byte[] imageBytes = Convert.FromBase64String(URIstring);

        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(imageBytes);

        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);        
        spr[0].sprite = sprite;
        spr[1].sprite = sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DownloadDatabase();
        }
    }
}

public class DrawData
{
    public string Name { get; set; }
    public string Sprite { get; set; }
    public string DataURI { get; set; }
}