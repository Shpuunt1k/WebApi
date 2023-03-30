using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Viewmodels;

namespace WebApi.Entity.Models;

public class GetSectionViewModel
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public List<GetThemeViewModel> Themes { get; set; }
	public int? CategoryId { get; set; }

	public GetSectionViewModel()
	{
		Themes = new List<GetThemeViewModel>();
	}
	public int CountThemes { get; set; }
}
