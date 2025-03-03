﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.TestFramework;
using Azure.Storage.Files.DataLake.Models;
using Azure.Storage.Test.Shared;

namespace Azure.Storage.Files.DataLake.Tests
{
    [DataLakeClientTestFixture]
    public class DataLakeFileClientTransferValidationTests : TransferValidationTestBase<
        DataLakeServiceClient,
        DataLakeFileSystemClient,
        DataLakeFileClient,
        DataLakeClientOptions,
        DataLakeTestEnvironment>
    {
        private const string _fileResourcePrefix = "test-file-";

        public DataLakeFileClientTransferValidationTests(bool async, DataLakeClientOptions.ServiceVersion serviceVersion)
            : base(async, _fileResourcePrefix, null /* RecordedTestMode.Record /* to re-record */)
        {
            ClientBuilder = ClientBuilderExtensions.GetNewDataLakeClientBuilder(Tenants, serviceVersion);
        }

        protected override async Task<IDisposingContainer<DataLakeFileSystemClient>> GetDisposingContainerAsync(
            DataLakeServiceClient service = null,
            string containerName = null,
            UploadTransferValidationOptions uploadTransferValidationOptions = default,
            DownloadTransferValidationOptions downloadTransferValidationOptions = default)
        {
            var disposingFileSystem = await ClientBuilder.GetNewFileSystem(service: service, fileSystemName: containerName);

            disposingFileSystem.FileSystem.ClientConfiguration.UploadTransferValidationOptions = uploadTransferValidationOptions;
            disposingFileSystem.FileSystem.ClientConfiguration.DownloadTransferValidationOptions = downloadTransferValidationOptions;

            return disposingFileSystem;
        }

        protected override async Task<DataLakeFileClient> GetResourceClientAsync(
            DataLakeFileSystemClient container,
            int resourceLength = default,
            bool createResource = default,
            string resourceName = null,
            UploadTransferValidationOptions uploadTransferValidationOptions = default,
            DownloadTransferValidationOptions downloadTransferValidationOptions = default,
            DataLakeClientOptions options = null)
        {
            options ??= ClientBuilder.GetOptions();
            options.UploadTransferValidationOptions = uploadTransferValidationOptions;
            options.DownloadTransferValidationOptions = downloadTransferValidationOptions;

            container = InstrumentClient(new DataLakeFileSystemClient(container.Uri, Tenants.GetNewHnsSharedKeyCredentials(), options));
            var file = InstrumentClient(container.GetRootDirectoryClient().GetFileClient(resourceName ?? GetNewResourceName()));
            if (createResource)
            {
                await file.CreateAsync();
            }
            return file;
        }

        protected override async Task<Response> UploadPartitionAsync(DataLakeFileClient client, Stream source, UploadTransferValidationOptions hashingOptions)
        {
            return await client.AppendAsync(source, 0, new DataLakeFileAppendOptions
            {
                TransferValidationOptions = hashingOptions
            });
        }

        protected override async Task<Response> DownloadPartitionAsync(DataLakeFileClient client, Stream destination, DownloadTransferValidationOptions hashingOptions, HttpRange range = default)
        {
            var response = await client.ReadAsync(new DataLakeFileReadOptions
            {
                TransferValidationOptions = hashingOptions,
                Range = range
            });

            await response.Value.Content.CopyToAsync(destination);
            return response.GetRawResponse();
        }

        protected override async Task ParallelUploadAsync(DataLakeFileClient client, Stream source, UploadTransferValidationOptions hashingOptions, StorageTransferOptions transferOptions)
        {
            await client.UploadAsync(source, new DataLakeFileUploadOptions
            {
                TransferValidationOptions = hashingOptions,
                TransferOptions = transferOptions
            });
        }

        protected override async Task ParallelDownloadAsync(DataLakeFileClient client, Stream destination, DownloadTransferValidationOptions hashingOptions, StorageTransferOptions transferOptions)
        {
            await client.ReadToAsync(destination, new DataLakeFileReadToOptions
            {
                TransferValidationOptions = hashingOptions,
                TransferOptions = transferOptions
            });
        }

        protected override async Task<Stream> OpenWriteAsync(DataLakeFileClient client, UploadTransferValidationOptions hashingOptions, int internalBufferSize)
        {
            return await client.OpenWriteAsync(true, new DataLakeFileOpenWriteOptions
            {
                TransferValidationOptions = hashingOptions,
                BufferSize = internalBufferSize
            });
        }

        protected override async Task<Stream> OpenReadAsync(DataLakeFileClient client, DownloadTransferValidationOptions hashingOptions, int internalBufferSize)
        {
            return await client.OpenReadAsync(new DataLakeOpenReadOptions(false)
            {
                TransferValidationOptions = hashingOptions,
                BufferSize = internalBufferSize
            });
        }

        protected override async Task SetupDataAsync(DataLakeFileClient client, Stream data)
        {
            await client.AppendAsync(data, 0);
            await client.FlushAsync(data.Length);
        }

        protected override bool ParallelUploadIsChecksumExpected(Request request)
        {
            return request.Uri.Query.Contains("action=append");
        }
    }
}
