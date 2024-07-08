using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using api.Dtos.S3;
using api.Models;

namespace api.Interfaces
{
    public interface IStorageServiceRepository
    {
        Task<S3ResponseDto> UploadFileAsync(S3object s3obj, AwsCredentials awsCredentials);
        Task<S3ResponseDto> DeleteFileAsync(S3object s3obj, AwsCredentials awsCredentials);
    }
}