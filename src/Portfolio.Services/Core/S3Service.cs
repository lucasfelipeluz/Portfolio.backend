using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Portfolio.Domain.Entities;
using Portfolio.Services.Interfaces;
using Portfolio.Services.Utils;

namespace Portfolio.Services;

public class S3Service : IS3Service
{
	public async Task<S3Response> UploadFile(S3Object s3Obj)
	{
		var response = new S3Response();

		try
		{
			// If the connection to AWS is false,
			// we will send a false response and
			// dont send image to S3
			string statusConnectAWS = Environment.GetEnvironmentVariable("AWS_STATUS");

			if (statusConnectAWS == "false")
			{
				response.Success = false;
				response.Message = "Image upload is disabled!";

				return response;
			}

			var awsCredentials = new AwsCredentials()
			{
				AwsKey = Environment.GetEnvironmentVariable("AWS_ACESS_KEY"),
				AwsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY"),
			};

			var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey);

			var config = new AmazonS3Config() { RegionEndpoint = Amazon.RegionEndpoint.SAEast1, };

			var uploadRequest = new TransferUtilityUploadRequest()
			{
				InputStream = s3Obj.InputString,
				Key = $"imagens/{s3Obj.Folder}/{s3Obj.Name}",
				BucketName = s3Obj.BucketName,
				CannedACL = S3CannedACL.NoACL,
			};

			using var client = new AmazonS3Client(credentials, config);

			var transferUtility = new TransferUtility(client);

			await transferUtility.UploadAsync(uploadRequest);

			response.Success = true;
			response.Message = $"{s3Obj.Name} has been uploaded successfully!";

			return response;
		}
		catch (AmazonS3Exception s3Exception)
		{
			response.Success = false;
			response.Message = s3Exception.Message;

			return response;
		}
		catch (Exception ex)
		{
			response.Success = false;
			response.Message = ex.Message;

			return response;
		}
	}
}
