﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.DocumentAnalysis
{
    /// <summary>
    /// The client to use to connect with the Form Recognizer Azure Cognitive Service to build models from
    /// custom documents. It also supports listing, copying, and deleting models, creating composed models, and accessing account
    /// properties.
    /// </summary>
    /// <remarks>
    /// This client only works with <see cref="DocumentAnalysisClientOptions.ServiceVersion.V2022_06_30_Preview"/> and up.
    /// If you want to use a lower version, please use the <see cref="Training.FormTrainingClient"/>.
    /// </remarks>
    public class DocumentModelAdministrationClient
    {
        /// <summary>Provides communication with the Form Recognizer Azure Cognitive Service through its REST API.</summary>
        internal readonly DocumentAnalysisRestClient ServiceClient;

        /// <summary>Provides tools for exception creation in case of failure.</summary>
        internal readonly ClientDiagnostics Diagnostics;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelAdministrationClient"/> class.
        /// </summary>
        protected DocumentModelAdministrationClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelAdministrationClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint to use for connecting to the Form Recognizer Azure Cognitive Service.</param>
        /// <param name="credential">A credential used to authenticate to an Azure Service.</param>
        /// <remarks>
        /// Both the <paramref name="endpoint"/> URI string and the <paramref name="credential"/> <c>string</c> key
        /// can be found in the Azure Portal.
        /// For more information see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/formrecognizer/Azure.AI.FormRecognizer/README.md#authenticate-the-client"> here</see>.
        /// </remarks>
        public DocumentModelAdministrationClient(Uri endpoint, AzureKeyCredential credential)
            : this(endpoint, credential, new DocumentAnalysisClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelAdministrationClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint to use for connecting to the Form Recognizer Azure Cognitive Service.</param>
        /// <param name="credential">A credential used to authenticate to an Azure Service.</param>
        /// <param name="options">A set of options to apply when configuring the client.</param>
        /// <remarks>
        /// Both the <paramref name="endpoint"/> URI string and the <paramref name="credential"/> <c>string</c> key
        /// can be found in the Azure Portal.
        /// For more information see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/formrecognizer/Azure.AI.FormRecognizer/README.md#authenticate-the-client"> here</see>.
        /// </remarks>
        public DocumentModelAdministrationClient(Uri endpoint, AzureKeyCredential credential, DocumentAnalysisClientOptions options)
        {
            Argument.AssertNotNull(endpoint, nameof(endpoint));
            Argument.AssertNotNull(credential, nameof(credential));

            options ??= new DocumentAnalysisClientOptions();

            Diagnostics = new ClientDiagnostics(options);
            HttpPipeline pipeline = HttpPipelineBuilder.Build(options, new AzureKeyCredentialPolicy(credential, Constants.AuthorizationHeader));
            ServiceClient = new DocumentAnalysisRestClient(Diagnostics, pipeline, endpoint.AbsoluteUri);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelAdministrationClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint to use for connecting to the Form Recognizer Azure Cognitive Service.</param>
        /// <param name="credential">A credential used to authenticate to an Azure Service.</param>
        /// <remarks>
        /// The <paramref name="endpoint"/> URI string can be found in the Azure Portal.
        /// For more information see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/formrecognizer/Azure.AI.FormRecognizer/README.md#authenticate-the-client"> here</see>.
        /// </remarks>
        public DocumentModelAdministrationClient(Uri endpoint, TokenCredential credential)
            : this(endpoint, credential, new DocumentAnalysisClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelAdministrationClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint to use for connecting to the Form Recognizer Azure Cognitive Service.</param>
        /// <param name="credential">A credential used to authenticate to an Azure Service.</param>
        /// <param name="options">A set of options to apply when configuring the client.</param>
        /// <remarks>
        /// The <paramref name="endpoint"/> URI string can be found in the Azure Portal.
        /// For more information see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/formrecognizer/Azure.AI.FormRecognizer/README.md#authenticate-the-client"> here</see>.
        /// </remarks>
        public DocumentModelAdministrationClient(Uri endpoint, TokenCredential credential, DocumentAnalysisClientOptions options)
        {
            Argument.AssertNotNull(endpoint, nameof(endpoint));
            Argument.AssertNotNull(credential, nameof(credential));

            options ??= new DocumentAnalysisClientOptions();
            string defaultScope = $"{(string.IsNullOrEmpty(options.Audience?.ToString()) ? DocumentAnalysisAudience.AzurePublicCloud : options.Audience)}/.default";

            Diagnostics = new ClientDiagnostics(options);
            var pipeline = HttpPipelineBuilder.Build(options, new BearerTokenAuthenticationPolicy(credential, defaultScope));
            ServiceClient = new DocumentAnalysisRestClient(Diagnostics, pipeline, endpoint.AbsoluteUri);
        }

        #region Build
        /// <summary>
        /// Build a custom document analysis model from a collection of documents in an Azure Blob Storage container.
        /// </summary>
        /// <param name="waitUntil">
        /// <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service;
        /// <see cref="WaitUntil.Started"/> if it should return after starting the operation.
        /// </param>
        /// <param name="trainingFilesUri">
        /// An externally accessible Azure Blob Storage container URI pointing to the container that has your training files.
        /// Note that a container URI without SAS is accepted only when the container is public or has a managed identity
        /// configured.
        /// For more information on setting up a training data set, see <see href="https://aka.ms/azsdk/formrecognizer/buildcustommodel">this article</see>.
        /// </param>
        /// <param name="buildMode">
        /// The technique to use to build the model. Use:
        /// <list type="bullet">
        ///   <item>
        ///     <term><see cref="DocumentBuildMode.Template"/></term>
        ///     <description>
        ///       When the custom documents all have the same layout. Fields are expected to be in the same place
        ///       across documents. Build time tends to be considerably shorter than <see cref="DocumentBuildMode.Neural"/>
        ///       mode.
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="DocumentBuildMode.Neural"/></term>
        ///     <description>
        ///       Recommended mode when custom documents have different layouts. Fields are expected to be the same but
        ///       they can be placed in different positions across documents.
        ///     </description>
        ///   </item>
        /// </list>
        /// For more information see <see href="https://aka.ms/azsdk/formrecognizer/buildmode">here</see>.
        /// </param>
        /// <param name="modelId">A unique ID for your model. If not specified, a model ID will be created for you.</param>
        /// <param name="options">A set of options available for configuring the request. For example, set a model description or set a filter to apply
        /// to the documents in the source path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>
        /// A <see cref="BuildModelOperation"/> to wait on this long-running operation. Its Value upon successful
        /// completion will contain meta-data about the created custom model.
        /// </returns>
        public virtual BuildModelOperation BuildModel(WaitUntil waitUntil, Uri trainingFilesUri, DocumentBuildMode buildMode, string modelId = default, BuildModelOptions options = default, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(trainingFilesUri, nameof(trainingFilesUri));

            options ??= new BuildModelOptions();

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(BuildModel)}");
            scope.Start();

            try
            {
                var source = new AzureBlobContentSource(trainingFilesUri.AbsoluteUri);
                if (!string.IsNullOrEmpty(options.Prefix))
                {
                    source.Prefix = options.Prefix;
                }

                modelId ??= Guid.NewGuid().ToString();
                var request = new BuildDocumentModelRequest(modelId, buildMode)
                {
                    AzureBlobSource = source,
                    Description = options.Description
                };

                foreach (var tag in options.Tags)
                {
                    request.Tags.Add(tag);
                }

                var response = ServiceClient.BuildDocumentModel(request, cancellationToken);
                var operation = new BuildModelOperation(response.Headers.OperationLocation, response.GetRawResponse(), ServiceClient, Diagnostics);

                if (waitUntil == WaitUntil.Completed)
                {
                    operation.WaitForCompletion(cancellationToken);
                }

                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Build a custom model from a collection of documents in an Azure Blob Storage container.
        /// </summary>
        /// <param name="waitUntil">
        /// <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service;
        /// <see cref="WaitUntil.Started"/> if it should return after starting the operation.
        /// </param>
        /// <param name="trainingFilesUri">
        /// An externally accessible Azure Blob Storage container URI pointing to the container that has your training files.
        /// Note that a container URI without SAS is accepted only when the container is public or has a managed identity
        /// configured.
        /// For more information on setting up a training data set, see <see href="https://aka.ms/azsdk/formrecognizer/buildcustommodel">this article</see>.
        /// </param>
        /// <param name="buildMode">
        /// The technique to use to build the model. Use:
        /// <list type="bullet">
        ///   <item>
        ///     <term><see cref="DocumentBuildMode.Template"/></term>
        ///     <description>
        ///       When the custom documents all have the same layout. Fields are expected to be in the same place
        ///       across documents. Build time tends to be considerably shorter than <see cref="DocumentBuildMode.Neural"/>
        ///       mode.
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="DocumentBuildMode.Neural"/></term>
        ///     <description>
        ///       Recommended mode when custom documents have different layouts. Fields are expected to be the same but
        ///       they can be placed in different positions across documents.
        ///     </description>
        ///   </item>
        /// </list>
        /// For more information see <see href="https://aka.ms/azsdk/formrecognizer/buildmode">here</see>.
        /// </param>
        /// <param name="modelId">A unique ID for your model. If not specified, a model ID will be created for you.</param>
        /// <param name="options">A set of options available for configuring the request. For example, set a model description or set a filter to apply
        /// to the documents in the source path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>
        /// A <see cref="BuildModelOperation"/> to wait on this long-running operation. Its Value upon successful
        /// completion will contain meta-data about the created custom model.
        /// </returns>
        public virtual async Task<BuildModelOperation> BuildModelAsync(WaitUntil waitUntil, Uri trainingFilesUri, DocumentBuildMode buildMode, string modelId = default, BuildModelOptions options = default, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(trainingFilesUri, nameof(trainingFilesUri));
            options ??= new BuildModelOptions();

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(BuildModel)}");
            scope.Start();

            try
            {
                var source = new AzureBlobContentSource(trainingFilesUri.AbsoluteUri);
                if (!string.IsNullOrEmpty(options.Prefix))
                {
                    source.Prefix = options.Prefix;
                }

                modelId ??= Guid.NewGuid().ToString();
                var request = new BuildDocumentModelRequest(modelId, buildMode)
                {
                    AzureBlobSource = source,
                    Description = options.Description
                };

                foreach (var tag in options.Tags)
                {
                    request.Tags.Add(tag);
                }

                var response = await ServiceClient.BuildDocumentModelAsync(request, cancellationToken).ConfigureAwait(false);
                var operation = new BuildModelOperation(response.Headers.OperationLocation, response.GetRawResponse(), ServiceClient, Diagnostics);

                if (waitUntil == WaitUntil.Completed)
                {
                    await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false);
                }

                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        #endregion Build

        #region Management Ops

        /// <summary>
        /// Gets information about a model, including the types of documents it can recognize and the fields it will extract for each document type.
        /// </summary>
        /// <param name="modelId">The ID of the model to retrieve.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to a <see cref="DocumentModel"/> containing
        /// information about the requested model.</returns>
        public virtual Response<DocumentModel> GetModel(string modelId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(modelId, nameof(modelId));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetModel)}");
            scope.Start();

            try
            {
                Response<DocumentModel> response = ServiceClient.GetModel(modelId, cancellationToken);
                return Response.FromValue(response.Value, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Gets information about a model, including the types of documents it can recognize and the fields it will extract for each document type.
        /// </summary>
        /// <param name="modelId">The ID of the model to retrieve.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to a <see cref="DocumentModel"/> containing
        /// information about the requested model.</returns>
        public virtual async Task<Response<DocumentModel>> GetModelAsync(string modelId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(modelId, nameof(modelId));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetModel)}");
            scope.Start();

            try
            {
                Response<DocumentModel> response = await ServiceClient.GetModelAsync(modelId, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(response.Value, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Deletes the model with the specified model ID.
        /// </summary>
        /// <param name="modelId">The ID of the model to delete.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response"/> representing the result of the operation.</returns>
        public virtual Response DeleteModel(string modelId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(modelId, nameof(modelId));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(DeleteModel)}");
            scope.Start();

            try
            {
                return ServiceClient.DeleteModel(modelId, cancellationToken);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Deletes the model with the specified model ID.
        /// </summary>
        /// <param name="modelId">The ID of the model to delete.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response"/> representing the result of the operation.</returns>
        public virtual async Task<Response> DeleteModelAsync(string modelId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(modelId, nameof(modelId));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(DeleteModel)}");
            scope.Start();

            try
            {
                return await ServiceClient.DeleteModelAsync(modelId, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Gets a collection of items describing the models available on this Cognitive Services Account.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A collection of <see cref="DocumentModelSummary"/> items.</returns>
        public virtual Pageable<DocumentModelSummary> GetModels(CancellationToken cancellationToken = default)
        {
            Page<DocumentModelSummary> FirstPageFunc(int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetModels)}");
                scope.Start();

                try
                {
                    Response<GetModelsResponse> response = ServiceClient.GetModels(cancellationToken);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            Page<DocumentModelSummary> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetModels)}");
                scope.Start();

                try
                {
                    Response<GetModelsResponse> response = ServiceClient.GetModelsNextPage(nextLink, cancellationToken);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Gets a collection of items describing the models available on this Cognitive Services Account.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A collection of <see cref="DocumentModelSummary"/> items.</returns>
        public virtual AsyncPageable<DocumentModelSummary> GetModelsAsync(CancellationToken cancellationToken = default)
        {
            async Task<Page<DocumentModelSummary>> FirstPageFunc(int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetModels)}");
                scope.Start();

                try
                {
                    Response<GetModelsResponse> response = await ServiceClient.GetModelsAsync(cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            async Task<Page<DocumentModelSummary>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetModels)}");
                scope.Start();

                try
                {
                    Response<GetModelsResponse> response = await ServiceClient.GetModelsNextPageAsync(nextLink, cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Gets the number of built models on this Cognitive Services Account and the account limits.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to an <see cref="AccountProperties"/> containing
        /// the account properties.</returns>
        public virtual Response<AccountProperties> GetAccountProperties(CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetAccountProperties)}");
            scope.Start();

            try
            {
                Response<GetInfoResponse> response = ServiceClient.GetInfo(cancellationToken);
                return Response.FromValue(response.Value.CustomDocumentModels, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Gets the number of built models on this Cognitive Services Account and the account limits.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to an <see cref="AccountProperties"/> containing
        /// the account properties.</returns>
        public virtual async Task<Response<AccountProperties>> GetAccountPropertiesAsync(CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetAccountProperties)}");
            scope.Start();

            try
            {
                Response<GetInfoResponse> response = await ServiceClient.GetInfoAsync(cancellationToken).ConfigureAwait(false);
                return Response.FromValue(response.Value.CustomDocumentModels, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Get a document model operation by its ID. Note that operation information only persists for 24 hours.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to a <see cref="DocumentModel"/> containing
        /// information about the requested model.</returns>
        public virtual Response<ModelOperation> GetOperation(string operationId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(operationId, nameof(operationId));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetOperation)}");
            scope.Start();

            try
            {
                var response = ServiceClient.GetOperation(operationId, cancellationToken);
                return Response.FromValue(response.Value, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Get a document model operation by its ID. Note that operation information only persists for 24 hours.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to a <see cref="DocumentModel"/> containing
        /// information about the requested model.</returns>
        public virtual async Task<Response<ModelOperation>> GetOperationAsync(string operationId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(operationId, nameof(operationId));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetOperation)}");
            scope.Start();

            try
            {
                var response = await ServiceClient.GetOperationAsync(operationId, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(response.Value, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Lists all document model operations associated with the Form Recognizer resource. Note that operation information only persists for 24 hours.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A collection of <see cref="ModelOperationInfo"/> items.</returns>
        public virtual Pageable<ModelOperationInfo> GetOperations(CancellationToken cancellationToken = default)
        {
            Page<ModelOperationInfo> FirstPageFunc(int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetOperations)}");
                scope.Start();

                try
                {
                    var response = ServiceClient.GetOperations(cancellationToken);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            Page<ModelOperationInfo> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetOperations)}");
                scope.Start();

                try
                {
                    var response = ServiceClient.GetOperationsNextPage(nextLink, cancellationToken);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Lists all document model operations associated with the Form Recognizer resource. Note that operation information only persists for 24 hours.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A collection of <see cref="ModelOperationInfo"/> items.</returns>
        public virtual AsyncPageable<ModelOperationInfo> GetOperationsAsync(CancellationToken cancellationToken = default)
        {
            async Task<Page<ModelOperationInfo>> FirstPageFunc(int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetOperations)}");
                scope.Start();

                try
                {
                    var response = await ServiceClient.GetOperationsAsync(cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            async Task<Page<ModelOperationInfo>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetOperations)}");
                scope.Start();

                try
                {
                    var response = await ServiceClient.GetOperationsNextPageAsync(nextLink, cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }

            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }

        #endregion

        #region Copy
        /// <summary>
        /// Copy a custom model stored in this resource (the source) to the user specified
        /// target Form Recognizer resource.
        /// </summary>
        /// <param name="waitUntil">
        /// <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service;
        /// <see cref="WaitUntil.Started"/> if it should return after starting the operation.
        /// </param>
        /// <param name="modelId">Model identifier of the model to copy to the target Form Recognizer resource.</param>
        /// <param name="target">A <see cref="CopyAuthorization"/> with the copy authorization to the target Form Recognizer resource.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="CopyModelOperation"/> to wait on this long-running operation.  Its <see cref="CopyModelOperation.Value"/> upon successful
        /// completion will contain meta-data about the model copied.</returns>
        public virtual CopyModelOperation CopyModelTo(WaitUntil waitUntil, string modelId, CopyAuthorization target, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(modelId, nameof(modelId));
            Argument.AssertNotNull(target, nameof(target));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(CopyModelTo)}");
            scope.Start();

            try
            {
                var response = ServiceClient.CopyDocumentModelTo(modelId, target, cancellationToken);
                var operation = new CopyModelOperation(ServiceClient, Diagnostics, response.Headers.OperationLocation, response.GetRawResponse());

                if (waitUntil == WaitUntil.Completed)
                {
                    operation.WaitForCompletion(cancellationToken);
                }

                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Copy a custom model stored in this resource (the source) to the user specified
        /// target Form Recognizer resource.
        /// </summary>
        /// <param name="waitUntil">
        /// <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service;
        /// <see cref="WaitUntil.Started"/> if it should return after starting the operation.
        /// </param>
        /// <param name="modelId">Model identifier of the model to copy to the target Form Recognizer resource.</param>
        /// <param name="target">A <see cref="CopyAuthorization"/> with the copy authorization to the target Form Recognizer resource.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="CopyModelOperation"/> to wait on this long-running operation.  Its <see cref="CopyModelOperation.Value"/> upon successful
        /// completion will contain meta-data about the model copied.</returns>
        public virtual async Task<CopyModelOperation> CopyModelToAsync(WaitUntil waitUntil, string modelId, CopyAuthorization target, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(modelId, nameof(modelId));
            Argument.AssertNotNull(target, nameof(target));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(CopyModelTo)}");
            scope.Start();

            try
            {
                var response = await ServiceClient.CopyDocumentModelToAsync(modelId, target, cancellationToken).ConfigureAwait(false);
                var operation = new CopyModelOperation(ServiceClient, Diagnostics, response.Headers.OperationLocation, response.GetRawResponse());

                if (waitUntil == WaitUntil.Completed)
                {
                    await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false);
                }

                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Generate authorization for copying a custom model into the target Form Recognizer resource.
        /// </summary>
        /// <param name="modelId">A unique ID for your copied model. If not specified, a model ID will be created for you.</param>
        /// <param name="description">An optional description to add to the model.</param>
        /// <param name="tags">A list of user-defined key-value tag attributes associated with the model.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to <see cref="CopyAuthorization"/> containing
        /// the authorization information necessary to copy a custom model into a target Form Recognizer resource.</returns>
        public virtual Response<CopyAuthorization> GetCopyAuthorization(string modelId = default, string description = default, IDictionary<string, string> tags = default, CancellationToken cancellationToken = default)
        {
            modelId ??= Guid.NewGuid().ToString();

            var request = new AuthorizeCopyRequest(modelId)
            {
                Description = description
            };

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    request.Tags.Add(tag);
                }
            }

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetCopyAuthorization)}");
            scope.Start();

            try
            {
                var response = ServiceClient.AuthorizeCopyDocumentModel(request, cancellationToken);
                return Response.FromValue(response.Value, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Generate authorization for copying a custom model into the target Form Recognizer resource.
        /// </summary>
        /// <param name="modelId">A unique ID for your copied model. If not specified, a model ID will be created for you.</param>
        /// <param name="description">An optional description to add to the model.</param>
        /// <param name="tags">A list of user-defined key-value tag attributes associated with the model.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>A <see cref="Response{T}"/> representing the result of the operation. It can be cast to <see cref="CopyAuthorization"/> containing
        /// the authorization information necessary to copy a custom model into a target Form Recognizer resource.</returns>
        public virtual async Task<Response<CopyAuthorization>> GetCopyAuthorizationAsync(string modelId = default, string description = default, IDictionary<string, string> tags = default, CancellationToken cancellationToken = default)
        {
            modelId ??= Guid.NewGuid().ToString();

            var request = new AuthorizeCopyRequest(modelId)
            {
                Description = description
            };

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    request.Tags.Add(tag);
                }
            }

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(GetCopyAuthorization)}");
            scope.Start();

            try
            {
                var response = await ServiceClient.AuthorizeCopyDocumentModelAsync(request, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(response.Value, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        #endregion Copy

        #region Composed Model

        /// <summary>
        /// Composes a model from a collection of existing models.
        /// A model built by composition allows multiple models to be called with a single model ID. When a document is
        /// submitted to be analyzed with its model ID, a classification step is first performed to route it to the correct
        /// custom model.
        /// </summary>
        /// <param name="waitUntil">
        /// <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service;
        /// <see cref="WaitUntil.Started"/> if it should return after starting the operation.
        /// </param>
        /// <param name="componentModelIds">List of model ids to use in the composition.</param>
        /// <param name="modelId">A unique ID for your model. If not specified, a model ID will be created for you.</param>
        /// <param name="description">An optional description to add to the model.</param>
        /// <param name="tags">A list of user-defined key-value tag attributes associated with the model.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>
        /// <para>A <see cref="BuildModelOperation"/> to wait on this long-running operation. Its Value upon successful
        /// completion will contain meta-data about the built model.</para>
        /// </returns>
        public virtual BuildModelOperation ComposeModel(WaitUntil waitUntil, IEnumerable<string> componentModelIds, string modelId = default, string description = default, IDictionary<string, string> tags = default, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(componentModelIds, nameof(componentModelIds));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(ComposeModel)}");
            scope.Start();

            try
            {
                modelId ??= Guid.NewGuid().ToString();
                var composeRequest = new ComposeDocumentModelRequest(modelId, ConvertToComponentModelInfo(componentModelIds))
                {
                    Description = description
                };

                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        composeRequest.Tags.Add(tag);
                    }
                }

                var response = ServiceClient.ComposeDocumentModel(composeRequest, cancellationToken);
                var operation = new BuildModelOperation(response.Headers.OperationLocation, response.GetRawResponse(), ServiceClient, Diagnostics);

                if (waitUntil == WaitUntil.Completed)
                {
                    operation.WaitForCompletion(cancellationToken);
                }

                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Composes a model from a collection of existing models.
        /// A model built by composition allows multiple models to be called with a single model ID. When a document is
        /// submitted to be analyzed with its model ID, a classification step is first performed to route it to the correct
        /// custom model.
        /// </summary>
        /// <param name="waitUntil">
        /// <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service;
        /// <see cref="WaitUntil.Started"/> if it should return after starting the operation.
        /// </param>
        /// <param name="componentModelIds">List of model ids to use in the composition.</param>
        /// <param name="modelId">A unique ID for your model. If not specified, a model ID will be created for you.</param>
        /// <param name="description">An optional description to add to the model.</param>
        /// <param name="tags">A list of user-defined key-value tag attributes associated with the model.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> controlling the request lifetime.</param>
        /// <returns>
        /// <para>A <see cref="BuildModelOperation"/> to wait on this long-running operation. Its Value upon successful
        /// completion will contain meta-data about the built model.</para>
        /// </returns>
        public virtual async Task<BuildModelOperation> ComposeModelAsync(WaitUntil waitUntil, IEnumerable<string> componentModelIds, string modelId = default, string description = default, IDictionary<string, string> tags = default, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(componentModelIds, nameof(componentModelIds));

            using DiagnosticScope scope = Diagnostics.CreateScope($"{nameof(DocumentModelAdministrationClient)}.{nameof(ComposeModel)}");
            scope.Start();

            try
            {
                modelId ??= Guid.NewGuid().ToString();
                var composeRequest = new ComposeDocumentModelRequest(modelId, ConvertToComponentModelInfo(componentModelIds))
                {
                    Description = description
                };

                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        composeRequest.Tags.Add(tag);
                    }
                }

                var response = await ServiceClient.ComposeDocumentModelAsync(composeRequest, cancellationToken).ConfigureAwait(false);
                var operation = new BuildModelOperation(response.Headers.OperationLocation, response.GetRawResponse(), ServiceClient, Diagnostics);

                if (waitUntil == WaitUntil.Completed)
                {
                    await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false);
                }

                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        internal static List<ComponentModelInfo> ConvertToComponentModelInfo(IEnumerable<string> componentModelIds)
            => componentModelIds.Select((modelId) => new ComponentModelInfo(modelId)).ToList();

        #endregion Composed Model
    }
}
