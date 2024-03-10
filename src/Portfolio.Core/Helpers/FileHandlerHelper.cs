namespace Portfolio.Core.Helpers;

public class FileHandlerHelper
{
	public static void ConvertBase64ToSavedFile(string base64, string fileName)
	{
		Byte[] bytes = Convert.FromBase64String(base64);
		File.WriteAllBytes($"temp/{fileName}", bytes);
	}

	public static byte[] GetFileToBytesArray(string fileName)
	{
		return File.ReadAllBytes($"temp/{fileName}");
	}

	public static void DeleteFile(string fileName)
	{
		File.Delete($"temp/{fileName}");
	}
}
