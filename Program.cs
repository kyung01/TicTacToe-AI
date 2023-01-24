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


	public static void Maissn()
	{
		var currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
		var catPath = currentPath + "Resources\\Images\\Training\\Cats\\";
		var dogPath = currentPath + "Resources\\Images\\Training\\Dogs\\";
		var filesCat = Directory.GetFiles(catPath).ToList();
		var filesDog = Directory.GetFiles(dogPath).ToList();

		long filesizeMax = 0;
		foreach (var catFileName in filesCat)
		{
			var fileSize = GetFileSize(catFileName);
			filesizeMax = Math.Max(filesizeMax, fileSize);
			Console.WriteLine("" + catFileName + " " + fileSize);
		}

		foreach (var dogfile in filesDog)
		{
			var fileSize = GetFileSize(dogfile);
			filesizeMax = Math.Max(filesizeMax, fileSize);
			Console.WriteLine("" + dogfile + " " + fileSize);
		}
		Console.WriteLine("max size was " + filesizeMax); //93323
		//Console.WriteLine(currentPath);
	}

}