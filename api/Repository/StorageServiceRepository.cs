using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using api.Dtos.S3;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class StorageServiceRepository : IStorageServiceRepository
    {
        public async Task<S3ResponseDto> UploadFileAsync(S3object s3obj, AwsCredentials awsCredentials)

        {
            var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey); 

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUWest2
            };

            var response = new S3ResponseDto();

            try 
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3obj.InputStream,
                    Key = s3obj.Name,
                    BucketName = s3obj.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                using var client = new AmazonS3Client(credentials, config);

                var transferUtility = new TransferUtility(client);

                await transferUtility.UploadAsync(uploadRequest);

                response.StatusCode = 200;
                response.Message = $"{s3obj.Name} has been uploaded successfully.";
                response.Url = $"https://dlxfzwdunogf7.cloudfront.net/{s3obj.Name}";
            }
            catch(AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }   

        public async Task<S3ResponseDto> DeleteFileAsync(S3object s3obj, AwsCredentials awsCredentials)
        {
            var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey); 

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUWest2
            };

            var response = new S3ResponseDto();

            try 
            {
                var deleteRequest = new DeleteObjectRequest()
                {
                    Key = s3obj.Name,
                    BucketName = s3obj.BucketName,
                };

                using var client = new AmazonS3Client(credentials, config);

                await client.DeleteObjectAsync(deleteRequest);

                response.StatusCode = 200;
                response.Message = $"{s3obj.Name} has been deleted successfully.";
            }
            catch(AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}