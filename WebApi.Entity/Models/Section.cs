using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class Section
{
    public int Id { get; set; }
    public string Title{ get; set; }
    public string Description { get; set; }
	public List<Theme> Themes { get; set; }
	public Category? Category { get; set; }
	public int? CategoryId { get; set; }
	public Section()
	{
		Themes = new List<Theme>();
	}
}
