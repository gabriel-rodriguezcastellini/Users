using System.ComponentModel.DataAnnotations;

namespace Users.Api.Models;

public class QueryDto
{
    [Range(0, int.MaxValue)]
    public int? Page { get; set; }

    [Range(1, int.MaxValue)]
    public int? ItemsPerPage { get; set; }
}
