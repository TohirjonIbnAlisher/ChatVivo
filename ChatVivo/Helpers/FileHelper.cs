using Enitities.FileModels;

namespace ChatVivo.Helpers;

public static class FileHelper
{
    public static string RandomString(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        return finalString;
    }
    public static void Addwwwroot(string fileFolder, string filename, IFormFile file)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileFolder);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, filename);
        var bytes = new byte[file.Length];
        file.OpenReadStream().Read(bytes, 0, bytes.Length);
        File.WriteAllBytes(path, bytes);
    }

    private static void RemoveFile(string path)
    {
        var folderName = string.Empty;
        var fileName = string.Empty;
        if (!string.IsNullOrEmpty(path))
        {
            var n = 1;
            foreach (var i in path.Split('/'))
            {
                if (n == 1)
                    folderName = i;
                else
                    fileName = i;
                n++;
            }
        }
        var currentpath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot", folderName, fileName);
        if (File.Exists(currentpath))
            File.Delete(currentpath);

    }

    public static string AddFiles(AddFileModels model)
    {
        try
        {

            if (model.Files == null)
                return string.Empty;
            var appealFiles = string.Empty;
            string path = null;
            var field = model.Field.ToLower();

            foreach (var i in model.Files)
            {
                Console.WriteLine($"field: {field}");
            
                //using (var image = Image.FromStream(i.OpenReadStream()))
                //{
                //    var width = image.Width;
                //    var height = image.Height;
                //}
                long timespan = DateTime.Now.Ticks;

                //var dt = new DateTime();
                //var dt = dt.Add(new TimeSpan(timespan));

                var filename = RandomString(5) + timespan.ToString() + "__" + i.FileName.Replace("%", "_");
                Addwwwroot(field, filename, i);

                path = ";" + field + "/" + filename;

                appealFiles += path;
                Console.WriteLine($"path: {field}");
            }
            //; birinchi belgini o`chirib qoyish
            appealFiles = appealFiles.Substring(1, appealFiles.Length - 1);

            return appealFiles;
        }
        catch (Exception ext)
        {
            Console.WriteLine($"Add File Exception: {ext.Message}");
            return ext.Message;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string RemoveFile(DeleteFileModel model)
    {
        try
        {
            string files = "";

            if (string.IsNullOrEmpty(model.DeleteFile))
                return model.Files;

            foreach (var i in model.Files.Split('|'))
            {
                if (string.IsNullOrEmpty(i))
                    continue;

                if (i == model.DeleteFile)
                    continue;

                if (!string.IsNullOrEmpty(i))
                    files += ";" + i;
            }

            RemoveFile(model.DeleteFile);
            return files;
        }
        catch (Exception ext)
        {
            return model.Files;
        }
    }

   
}
