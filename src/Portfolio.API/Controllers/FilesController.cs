﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Utils;
using Portfolio.API.ViewModels;
using Portfolio.Core.Helpers;
using Portfolio.Domain.Entities;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.API.Controllers;

[ApiController]
public class FilesController : ControllerBase
{
	private readonly IS3Service _s3Service;
	private readonly IImageService _imageService;

	public FilesController(IS3Service s3Service, IImageService imageService)
	{
		_s3Service = s3Service;
		_imageService = imageService;
	}

	[HttpPost]
	[Route("api/images")]
	[Authorize]
	public async Task<IActionResult> UploadImage([FromBody] UploadImageViewModel uploadImageViewModel)
	{
		if (uploadImageViewModel.SkillId == 0 && uploadImageViewModel.ProjectId == 0)
			return BadRequest(Responses.InternalServerErrorMessage("You must provide a project or a skill id"));

		// Creating the file info
		string folderName = uploadImageViewModel.ProjectId != 0 ? "project" : "skill";
		int? entityId =
			uploadImageViewModel.ProjectId != 0 ? uploadImageViewModel.ProjectId : uploadImageViewModel.SkillId;
		string fileName = $"{folderName}_{entityId}{Guid.NewGuid()}.jpg";

		// Upload to AWS S3
		FileHandlerHelper.ConvertBase64ToSavedFile(uploadImageViewModel.Base64, fileName);
		var fileInfo = FileHandlerHelper.GetFileToBytesArray(fileName);
		var stream = new MemoryStream(fileInfo);

		var s3Obj = new S3Object()
		{
			BucketName = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME"),
			InputString = stream,
			Name = fileName,
			Folder = folderName,
		};

		var result = await _s3Service.UploadFile(s3Obj);

		// Saving records to the database
		var createImageDto = new CreateImageDto
		{
			Name = fileName,
			Folder = folderName,
			ProjectId = uploadImageViewModel.ProjectId,
			SkillId = uploadImageViewModel.SkillId,
		};

		if (result.Success)
			await _imageService.Create(createImageDto);

		FileHandlerHelper.DeleteFile(fileName);

		if (!result.Success)
			return StatusCode(
				StatusCodes.Status503ServiceUnavailable,
				Responses.ServiceUnavailableErrorMessage(result.Message)
			);

		return Ok(Responses.SuccessMessage(result.Message));
	}
}
