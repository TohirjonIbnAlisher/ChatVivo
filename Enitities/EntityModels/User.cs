using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enitities.EntityModels;

[Table("user",Schema = "auth")]

public class User : Person
{
}
