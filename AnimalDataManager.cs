using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Animal { DOG, CAT }
public struct AnimalData
{
	public string binary;
	public Animal type;
}
public class AnimalDataManager
{
	static string GetFileBinary(string inputFilename)
	{
		byte[] fileBytes = File.ReadAllBytes(inputFilename);
		StringBuilder sb = new StringBuilder();

		foreach (byte b in fileBytes)
		{
			sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
		}
		return sb.ToString();
	}


	static System.Random rand = new Random();
	string catPath;
	string dogPath;
	List<string> filesCat;
	List<string> filesDog;
	public AnimalDataManager()
	{

		var currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
		catPath = currentPath + "Resources\\Images\\Training\\Cats\\";
		dogPath = currentPath + "Resources\\Images\\Training\\Dogs\\";

		filesCat = Directory.GetFiles(catPath).ToList();
		filesDog = Directory.GetFiles(dogPath).ToList();
	}
	public AnimalData GetRandomCat()
	{
		var catFileName = filesCat[rand.Next(0, filesCat.Count)];
		//Console.WriteLine("" + catFileName + " " + fileSize);
		return new AnimalData() { binary = GetFileBinary(catFileName), type = Animal.CAT };
	}

	public AnimalData GetRandomDog()
	{
		var dogFileName = filesDog[rand.Next(0, filesDog.Count)];
		//Console.WriteLine("" + catFileName + " " + fileSize);
		return new AnimalData() { binary = GetFileBinary(dogFileName), type = Animal.DOG };
	}
}
