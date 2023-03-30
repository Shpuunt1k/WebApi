using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class NewCategoryViewModel
{
	public int? Id { get; set; }
	public string Title { get; set; }
	public List<NewSectionViewModel> Sections { get; set; }
	public NewCategoryViewModel()
	{
		Sections = new List<NewSectionViewModel>();
	}
}
