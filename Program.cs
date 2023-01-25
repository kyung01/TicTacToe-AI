using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


public class Program
{
	static void Masin(string[] args)
	{
		//BrainRunnerThread instance = new BrainRunnerThread();
		//instance.Init();
		//instance.Run();
	}

	static long GetFileSize(string FilePath)
	{
		//if you don't have permission to the folder, Directory.Exists will return False
		if (!Directory.Exists(Path.GetDirectoryName(FilePath)) )
		{
			//if you land here, it means you don't have permission to the folder
			Console.WriteLine("Permission denied");
			return -1;
		}
		else if (File.Exists(FilePath))
		{
			return new FileInfo(FilePath).Length;
		}
		return 0;
	}

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
	static AnimalDataManager animalManager;
	public static void Main2()
	{
		animalManager = new AnimalDataManager();

		var currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
		var catPath = currentPath + "Resources\\Images\\Training\\Cats\\";
		var dogPath = currentPath + "Resources\\Images\\Training\\Dogs\\";
		var filesCat = Directory.GetFiles(catPath).ToList();
		var filesDog = Directory.GetFiles(dogPath).ToList();


		List<AnimalData> animalData = new List<AnimalData>();
		long filesizeMax = 0;
		foreach (var catFileName in filesCat)
		{
			var fileSize = GetFileSize(catFileName);
			filesizeMax = Math.Max(filesizeMax, fileSize);
			//Console.WriteLine("" + catFileName + " " + fileSize);
			animalData.Add(new AnimalData() { binary = GetFileBinary(catFileName), type = Animal.CAT });
		}

		foreach (var dogfile in filesDog)
		{
			var fileSize = GetFileSize(dogfile);
			filesizeMax = Math.Max(filesizeMax, fileSize);
			//Console.WriteLine("" + dogfile + " " + fileSize);
			animalData.Add(new AnimalData() { binary = GetFileBinary(dogfile), type = Animal.DOG });
		}
		Console.WriteLine("max size was " + filesizeMax); //93323
		//Console.WriteLine(currentPath);
	}

	private static void List<T>()
	{
		throw new NotImplementedException();
	}
}