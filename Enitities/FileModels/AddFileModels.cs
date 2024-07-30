using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Enitities.FileModels;

public class AddFileModels
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Field { get; set; }

    public UserFileHelper UserFileHelper { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public List<IFormFile> Files { get; set; }
}
