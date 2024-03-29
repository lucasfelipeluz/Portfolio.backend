﻿using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class CreateAboutMeViewModel
{
	[MinLength(3, ErrorMessage = "The about me name must have more than 3 letters")]
	[MaxLength(100, ErrorMessage = "The about me name must have less than 100 letters")]
	public string Name { get; set; }

	[MinLength(3, ErrorMessage = "The about me text must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The about me text must have less than 500 letters")]
	public string Text { get; set; }

	[MinLength(3, ErrorMessage = "The about me text in english must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The about me text in english must have less than 500 letters")]
	public string TextEnglish { get; set; }

	[MinLength(3, ErrorMessage = "The about me job title must have more than 3 letters")]
	[MaxLength(50, ErrorMessage = "The about me job title must have less than 50 letters")]
	public string JobTitle { get; set; }

	[MinLength(3, ErrorMessage = "The about me job title in english must have more than 3 letters")]
	[MaxLength(50, ErrorMessage = "The about me job title in english must have less than 50 letters")]
	public string JobTitleEnglish { get; set; }

	[MinLength(3, ErrorMessage = "The about me url telegram must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The about me url telegram must have less than 80 letters")]
	public string TelegramLink { get; set; }

	[MinLength(3, ErrorMessage = "The about me url instagram must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The about me url instagram must have less than 80 letters")]
	public string InstagramLink { get; set; }

	[MinLength(3, ErrorMessage = "The about me url linkedin must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The about me url linkedin must have less than 80 letters")]
	public string LinkedinLink { get; set; }

	[MinLength(3, ErrorMessage = "The about me url github must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The about me url github must have less than 80 letters")]
	public string GithubLink { get; set; }

	[MinLength(3, ErrorMessage = "The about me address must have more than 3 letters")]
	[MaxLength(100, ErrorMessage = "The about me address must have less than 100 letters")]
	public string Address { get; set; }

	public bool IsAvailable { get; set; }
}
