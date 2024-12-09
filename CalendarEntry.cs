using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Attributes;
using Microsoft.VisualBasic;

class CalendarEntry
{
    public DateTime Start {get;set;}
    public DateTime End {get;set;}
    public string Id {get;set;} = Guid.NewGuid().ToString();
    public string Place {get;set;}="";
    public List<string> People {get;set;}=new();
    public string Title {get;set;}="";

    public void Save (){

        //serialize the calendar entry object to a string and save the string to a file
        File.WriteAllText($"{Id}.CalendarEntry", JsonSerializer.Serialize(this));

    }
    
    public static CalendarEntry Load(string loadId)
    {
        
        // define a path that points to ID + ".calendarEntry" 
        string path = $"{loadId}.CalendarEntry";
        
        // Use the path to load the file from disk as a string
        string caljson = File.ReadAllText(path);
        
        // use the JsonSerializer to deserialize a CalendarEntry object from the string
        CalendarEntry thiscal = JsonSerializer.Deserialize<CalendarEntry>(caljson);
        // return the CalendarEntry object 
        return thiscal;

    }
    public static List<CalendarEntry> LoadAll()
    {

        string[] files = System.IO.Directory.GetFiles(Environment.CurrentDirectory);
        List<CalendarEntry> cals = new(); 

        foreach(string file in files.Where(e => e.EndsWith(".CalendarEntry")))
        {
            string fp = file.Split("\\")[4];
            string thisFile = file.Replace(".CalendarEntry",""); 
            CalendarEntry cal = CalendarEntry.Load(thisFile);
            cals.Add(cal);

        }
        // for each file ending with .CalendarEntry in the current folder
            // look at the filename and extract the id from it 
            // Call CalendarEntry.Load(id) and add the result to a List<CalendarEntry>

        // return the list  
        return cals;



    }

}   



 


