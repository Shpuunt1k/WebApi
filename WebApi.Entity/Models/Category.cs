using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class Category
{
    public int? Id { get; set; }
    public string Title { get; set; }
	public List<Section> Sections { get; set; }
	public Category()
	{
		Sections = new List<Section>();
	}
}
