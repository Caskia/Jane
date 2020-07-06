using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jane.AWS.S3
{
    public class Test
    {
        public async Task DoAsync()
        {
            var s3Client = new AmazonS3Client("AKIAUJHU7YCPJYDIMQV4", "VTRAmGdfdizhCCX5D1YypbALrnn6laSAIFFJxebJ", RegionEndpoint.GetBySystemName("ap-northeast-1"));

            var accessKey = "VTRAmGdfdizhCCX5D1YypbALrnn6laSAIFFJxebJ";
            var stringToSign = "ew0KICAgICJleHBpcmF0aW9uIjogIjIwMjAtMDctMDdUMDA6MDA6MDBaIiwNCiAgICAiY29uZGl0aW9ucyI6IFsNCiAgICAgICAgew0KICAgICAgICAgICAgImJ1Y2tldCI6ICJoaWNvaW4iDQogICAgICAgIH0sDQogICAgICAgIHsNCiAgICAgICAgICAgICJrZXkiOiAiMTIyMzIxMy8yLnR4dCINCiAgICAgICAgfSwNCiAgICAgICAgew0KICAgICAgICAgICAgImFjbCI6ICJwdWJsaWMtcmVhZCINCiAgICAgICAgfSwNCiAgICAgICAgew0KICAgICAgICAgICAgIkNvbnRlbnQtVHlwZSI6ICJ0ZXh0L3BsYWluIg0KICAgICAgICB9LA0KICAgICAgICB7DQogICAgICAgICAgICAieC1hbXotYWxnb3JpdGhtIjogIkFXUzQtSE1BQy1TSEEyNTYiDQogICAgICAgIH0sDQogICAgICAgIHsNCiAgICAgICAgICAgICJ4LWFtei1jcmVkZW50aWFsIjogIkFLSUFVSkhVN1lDUEpZRElNUVY0LzIwMjAwNzA3L2FwLW5vcnRoZWFzdC0xL3MzL2F3czRfcmVxdWVzdCINCiAgICAgICAgfSwNCiAgICAgICAgew0KICAgICAgICAgICAgIngtYW16LWRhdGUiOiAiMjAyMDA3MDdUMDAwMDAwWiINCiAgICAgICAgfQ0KICAgIF0NCn0=";
            var region = "ap-northeast-1";
            var date = "20200707";

            //accessKey = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";
            //stringToSign = "eyAiZXhwaXJhdGlvbiI6ICIyMDE1LTEyLTMwVDEyOjAwOjAwLjAwMFoiLA0KICAiY29uZGl0aW9ucyI6IFsNCiAgICB7ImJ1Y2tldCI6ICJzaWd2NGV4YW1wbGVidWNrZXQifSwNCiAgICBbInN0YXJ0cy13aXRoIiwgIiRrZXkiLCAidXNlci91c2VyMS8iXSwNCiAgICB7ImFjbCI6ICJwdWJsaWMtcmVhZCJ9LA0KICAgIHsic3VjY2Vzc19hY3Rpb25fcmVkaXJlY3QiOiAiaHR0cDovL3NpZ3Y0ZXhhbXBsZWJ1Y2tldC5zMy5hbWF6b25hd3MuY29tL3N1Y2Nlc3NmdWxfdXBsb2FkLmh0bWwifSwNCiAgICBbInN0YXJ0cy13aXRoIiwgIiRDb250ZW50LVR5cGUiLCAiaW1hZ2UvIl0sDQogICAgeyJ4LWFtei1tZXRhLXV1aWQiOiAiMTQzNjUxMjM2NTEyNzQifSwNCiAgICB7IngtYW16LXNlcnZlci1zaWRlLWVuY3J5cHRpb24iOiAiQUVTMjU2In0sDQogICAgWyJzdGFydHMtd2l0aCIsICIkeC1hbXotbWV0YS10YWciLCAiIl0sDQoNCiAgICB7IngtYW16LWNyZWRlbnRpYWwiOiAiQUtJQUlPU0ZPRE5ON0VYQU1QTEUvMjAxNTEyMjkvdXMtZWFzdC0xL3MzL2F3czRfcmVxdWVzdCJ9LA0KICAgIHsieC1hbXotYWxnb3JpdGhtIjogIkFXUzQtSE1BQy1TSEEyNTYifSwNCiAgICB7IngtYW16LWRhdGUiOiAiMjAxNTEyMjlUMDAwMDAwWiIgfQ0KICBdDQp9";
            //region = "us-east-1";
            //date = "20151229";

            var dateKey = new HMACSHA256(Encoding.UTF8.GetBytes($"AWS4{accessKey}")).ComputeHash(Encoding.UTF8.GetBytes(date));
            var dateRegionKey = new HMACSHA256(dateKey).ComputeHash(Encoding.UTF8.GetBytes(region));
            var dateRegionServiceKey = new HMACSHA256(dateRegionKey).ComputeHash(Encoding.UTF8.GetBytes("s3"));
            var signingKey = new HMACSHA256(dateRegionServiceKey).ComputeHash(Encoding.UTF8.GetBytes("aws4_request"));

            var signatureBuffer = new HMACSHA256(signingKey).ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
            var base64Signature = Convert.ToBase64String(signatureBuffer);
            var signature = string.Concat(signatureBuffer.Select(k => k.ToString("x2")));

            var url = s3Client.GetPreSignedURL(new Amazon.S3.Model.GetPreSignedUrlRequest()
            {
                BucketName = "hicoin",
                Expires = new DateTime(2020, 07, 08),
                Key = "1223213/2.txt",
            });
            var t = await s3Client.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
            {
                CannedACL = "public-read",
                BucketName = "hicoin",
                FilePath = @"C:\Users\Caskia\Desktop\2.txt",
                Key = "1223213/2.txt"
            });
        }

        //        {
        //    "expiration": "2020-07-07T00:00:00Z",
        //    "conditions": [
        //        {
        //            "bucket": "hicoin"
        //        },
        //        {
        //            "key": "1223213/2.txt"
        //        },
        //        {
        //    "acl": "public-read"
        //        },
        //        {
        //    "Content-Type": "text/plain"
        //        },
        //        {
        //    "x-amz-algorithm": "AWS4-HMAC-SHA256"
        //        },
        //        {
        //    "x-amz-credential": "AKIAUJHU7YCPJYDIMQV4/20200707/ap-northeast-1/s3/aws4_request"
        //        },
        //        {
        //    "x-amz-date": "20200707T000000Z"
        //        }
        //    ]
        //}
    }
}