using Portfolio.Domain.Entities;
using Portfolio.Services.Utils;

namespace Portfolio.Services.Interfaces;

public interface IS3Service
{
	Task<S3Response> UploadFile(S3Object s3Obj);
}
