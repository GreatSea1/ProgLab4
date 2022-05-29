using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrLab4
{
  public class SearchingFiles
  {
    string FileName;
    string CatalogFromSearch = @"C:\Users\pawsw\source\repos\ProgLab4\ProgrLab4\Files";
    public void Search()
    {
      Console.WriteLine("Введите название файла: ");
      FileName = Console.ReadLine();
      FileName = FileName + ".txt";
      foreach (string FoundedFile in Directory.EnumerateFiles(CatalogFromSearch, FileName, SearchOption.AllDirectories))
      {
        FileInfo FI;
        try
        {
          //Создаётся объект класса, который содержит в себе ссылку на найденный файл
          FI = new FileInfo(FoundedFile);
          Console.WriteLine(FI.Name + " " + FI.FullName + " " + FI.Length + " байт " + " Создан " + FI.CreationTime);
        }
        catch
        {
          continue;
        }
      }
    }
  }
}