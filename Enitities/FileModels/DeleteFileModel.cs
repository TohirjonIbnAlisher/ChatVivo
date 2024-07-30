using System.ComponentModel.DataAnnotations;

namespace Enitities.FileModels;

public class DeleteFileModel
{
    /// <summary>
    /// folder1/a2.doc
    /// </summary>
    [Required]
    public string DeleteFile { get; set; }

    /// <summary>
    /// folder1/a1.doc;folder1/a2.doc;folder1/a3.doc
    /// </summary>
    [Required]
    public string Files { get; set; }
}
