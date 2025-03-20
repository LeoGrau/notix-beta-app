using System.ComponentModel.DataAnnotations;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Shared.Domain.Models;

namespace Notix.Beta.API.Auth.Domain.Models;

public class User : AuditModel
{
   public int Id { get; set; }
   public string Email { get; set; } = string.Empty;
   public string HashedPassword { get; set; } = string.Empty;
   public string FirstName { get; set; } = string.Empty;
   public string LastName { get; set; } = string.Empty;
   public IList<Note> Notes { get; set; } = new List<Note>();
}