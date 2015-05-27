


public static void SaveToFile(string fileName,string fileContent) 
{
	File.WriteAllBytes (filePath+"/"+fileName , System.Text.Encoding.UTF8.GetBytes(fileContent));
}

public static string ReadFromFile(string fileName) 
{
	string fileContent = "";
	if (File.Exists (filePath + "/" + fileName))
		fileContent = System.BitConverter.ToString(File.ReadAllBytes (filePath + "/" + fileName));
	return fileContent;
}

