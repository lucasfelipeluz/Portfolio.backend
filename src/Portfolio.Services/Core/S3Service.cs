using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Portfolio.Domain.Entities;
using Portfolio.Services.Interfaces;
using Portfolio.Services.Utils;

namespace Portfolio.Services
{
  public class S3Service : IS3Service
  {
    public async Task<S3Response> UploadFileAsync(S3Object s3Obj)
    {
      var response = new S3Response();

      try
      {
        var awsCredentials = new AwsCredentials()
        {
          AwsKey = Environment.GetEnvironmentVariable("AWS_ACESS_KEY"),
          AwsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY"),
        };

        var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey);

        var config = new AmazonS3Config()
        {
          RegionEndpoint = Amazon.RegionEndpoint.SAEast1,
        };

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

        response.StatusCode = 200;
        response.Message = $"{s3Obj.Name} has been uploaded successfully!";

        return response;
      }
      catch (AmazonS3Exception s3Exception)
      {
        response.StatusCode = (int)s3Exception.StatusCode;
        response.Message = s3Exception.Message;

        return response;
      }
      catch (Exception ex)
      {
        response.StatusCode = 500;
        response.Message = ex.Message;

        return response;
      }
    }
  }
}
