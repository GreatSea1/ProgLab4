using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProgrLab4
{
  public interface IOriginator
  {
    object GetMemento();
    void SetMemento(object Memento);
  }

  class FileMemento
  {
    public string FirstName;
    public string LastName;
  }

  [Serializable]
  public class Patient : IOriginator
  {
    public string _FirstName { get; set; }
    public string _LastName { get; set; }

    public Patient(string FirstName, string LastName)
    {
      _FirstName = FirstName;
      _LastName = LastName;
    }

    public object GetMemento()
    {
      return new FileMemento
      {
        FirstName = this._FirstName,
        LastName = this._LastName,
      };
    }

    public void SetMemento(object Memento)
    {
      if (Memento is FileMemento)
      {
        var Mem = Memento as FileMemento;
        _FirstName = Mem.FirstName;
        _LastName = Mem.LastName;
      }
    }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
      Patient pt = new Patient("John", "Doen");

      FileStream stream = File.Create(@"C:\Users\pawsw\source\repos\ProgLab4\ProgrLab4\Files\Patient.dat");
      BinaryFormatter formatter = new BinaryFormatter();

      //Сериализация
      formatter.Serialize(stream, pt);
      stream.Close();

      //Десериализация
      stream = File.OpenRead(@"C:\Users\pawsw\source\repos\ProgLab4\ProgrLab4\Files\Patient.dat");

      pt = formatter.Deserialize(stream) as Patient;

      Console.WriteLine("Имя    : " + pt._FirstName);
      Console.WriteLine("Фамилия: " + pt._LastName);
      stream.Close();
      Console.ReadKey();

      //Поиск файла
      SearchingFiles Search = new();
      Search.Search();

      Console.Write("Введите текст: ");
      string Searchable = Console.ReadLine();

      Patient FirstFile = new Patient("Matt", "Smith");
      Patient SecondFile = new Patient("Peter", "Capaldi");
      Patient ThirdFile = new Patient("Jodie", "Whittaker");

      List<Patient> ListOfFiles = new List<Patient>
            {
                FirstFile, SecondFile, ThirdFile
            };

      foreach (var File in ListOfFiles)
      {
        if (File._FirstName.Contains(Searchable))
        {
          Console.WriteLine($"{Searchable} in {File._LastName}");
        }
        else
        {
          Console.WriteLine($"{File._LastName} не содержит {Searchable}");
        }
      }
    }
  }
}
