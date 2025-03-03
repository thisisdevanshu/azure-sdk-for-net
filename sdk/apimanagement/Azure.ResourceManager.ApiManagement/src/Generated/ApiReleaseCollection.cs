// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;

namespace Azure.ResourceManager.ApiManagement
{
    /// <summary>
    /// A class representing a collection of <see cref="ApiReleaseResource" /> and their operations.
    /// Each <see cref="ApiReleaseResource" /> in the collection will belong to the same instance of <see cref="ApiResource" />.
    /// To get an <see cref="ApiReleaseCollection" /> instance call the GetApiReleases method from an instance of <see cref="ApiResource" />.
    /// </summary>
    public partial class ApiReleaseCollection : ArmCollection, IEnumerable<ApiReleaseResource>, IAsyncEnumerable<ApiReleaseResource>
    {
        private readonly ClientDiagnostics _apiReleaseClientDiagnostics;
        private readonly ApiReleaseRestOperations _apiReleaseRestClient;

        /// <summary> Initializes a new instance of the <see cref="ApiReleaseCollection"/> class for mocking. </summary>
        protected ApiReleaseCollection()
        {
        }

        /// <summary> Initializes a new instance of the <see cref="ApiReleaseCollection"/> class. </summary>
        /// <param name="client"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the parent resource that is the target of operations. </param>
        internal ApiReleaseCollection(ArmClient client, ResourceIdentifier id) : base(client, id)
        {
            _apiReleaseClientDiagnostics = new ClientDiagnostics("Azure.ResourceManager.ApiManagement", ApiReleaseResource.ResourceType.Namespace, Diagnostics);
            TryGetApiVersion(ApiReleaseResource.ResourceType, out string apiReleaseApiVersion);
            _apiReleaseRestClient = new ApiReleaseRestOperations(Pipeline, Diagnostics.ApplicationId, Endpoint, apiReleaseApiVersion);
#if DEBUG
			ValidateResourceId(Id);
#endif
        }

        internal static void ValidateResourceId(ResourceIdentifier id)
        {
            if (id.ResourceType != ApiResource.ResourceType)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid resource type {0} expected {1}", id.ResourceType, ApiResource.ResourceType), nameof(id));
        }

        /// <summary>
        /// Creates a new Release for the API.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases/{releaseId}
        /// Operation Id: ApiRelease_CreateOrUpdate
        /// </summary>
        /// <param name="waitUntil"> <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service; <see cref="WaitUntil.Started"/> if it should return after starting the operation. For more information on long-running operations, please see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/LongRunningOperations.md"> Azure.Core Long-Running Operation samples</see>. </param>
        /// <param name="releaseId"> Release identifier within an API. Must be unique in the current API Management service instance. </param>
        /// <param name="data"> Create parameters. </param>
        /// <param name="ifMatch"> ETag of the Entity. Not required when creating an entity, but required when updating an entity. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="releaseId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="releaseId"/> or <paramref name="data"/> is null. </exception>
        public virtual async Task<ArmOperation<ApiReleaseResource>> CreateOrUpdateAsync(WaitUntil waitUntil, string releaseId, ApiReleaseData data, string ifMatch = null, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(releaseId, nameof(releaseId));
            Argument.AssertNotNull(data, nameof(data));

            using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.CreateOrUpdate");
            scope.Start();
            try
            {
                var response = await _apiReleaseRestClient.CreateOrUpdateAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, releaseId, data, ifMatch, cancellationToken).ConfigureAwait(false);
                var operation = new ApiManagementArmOperation<ApiReleaseResource>(Response.FromValue(new ApiReleaseResource(Client, response), response.GetRawResponse()));
                if (waitUntil == WaitUntil.Completed)
                    await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false);
                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Creates a new Release for the API.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases/{releaseId}
        /// Operation Id: ApiRelease_CreateOrUpdate
        /// </summary>
        /// <param name="waitUntil"> <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service; <see cref="WaitUntil.Started"/> if it should return after starting the operation. For more information on long-running operations, please see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/LongRunningOperations.md"> Azure.Core Long-Running Operation samples</see>. </param>
        /// <param name="releaseId"> Release identifier within an API. Must be unique in the current API Management service instance. </param>
        /// <param name="data"> Create parameters. </param>
        /// <param name="ifMatch"> ETag of the Entity. Not required when creating an entity, but required when updating an entity. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="releaseId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="releaseId"/> or <paramref name="data"/> is null. </exception>
        public virtual ArmOperation<ApiReleaseResource> CreateOrUpdate(WaitUntil waitUntil, string releaseId, ApiReleaseData data, string ifMatch = null, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(releaseId, nameof(releaseId));
            Argument.AssertNotNull(data, nameof(data));

            using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.CreateOrUpdate");
            scope.Start();
            try
            {
                var response = _apiReleaseRestClient.CreateOrUpdate(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, releaseId, data, ifMatch, cancellationToken);
                var operation = new ApiManagementArmOperation<ApiReleaseResource>(Response.FromValue(new ApiReleaseResource(Client, response), response.GetRawResponse()));
                if (waitUntil == WaitUntil.Completed)
                    operation.WaitForCompletion(cancellationToken);
                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Returns the details of an API release.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases/{releaseId}
        /// Operation Id: ApiRelease_Get
        /// </summary>
        /// <param name="releaseId"> Release identifier within an API. Must be unique in the current API Management service instance. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="releaseId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="releaseId"/> is null. </exception>
        public virtual async Task<Response<ApiReleaseResource>> GetAsync(string releaseId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(releaseId, nameof(releaseId));

            using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.Get");
            scope.Start();
            try
            {
                var response = await _apiReleaseRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, releaseId, cancellationToken).ConfigureAwait(false);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new ApiReleaseResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Returns the details of an API release.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases/{releaseId}
        /// Operation Id: ApiRelease_Get
        /// </summary>
        /// <param name="releaseId"> Release identifier within an API. Must be unique in the current API Management service instance. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="releaseId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="releaseId"/> is null. </exception>
        public virtual Response<ApiReleaseResource> Get(string releaseId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(releaseId, nameof(releaseId));

            using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.Get");
            scope.Start();
            try
            {
                var response = _apiReleaseRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, releaseId, cancellationToken);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new ApiReleaseResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Lists all releases of an API. An API release is created when making an API Revision current. Releases are also used to rollback to previous revisions. Results will be paged and can be constrained by the $top and $skip parameters.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases
        /// Operation Id: ApiRelease_ListByService
        /// </summary>
        /// <param name="filter"> |     Field     |     Usage     |     Supported operators     |     Supported functions     |&lt;/br&gt;|-------------|-------------|-------------|-------------|&lt;/br&gt;| notes | filter | ge, le, eq, ne, gt, lt | substringof, contains, startswith, endswith |&lt;/br&gt;. </param>
        /// <param name="top"> Number of records to return. </param>
        /// <param name="skip"> Number of records to skip. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> An async collection of <see cref="ApiReleaseResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual AsyncPageable<ApiReleaseResource> GetAllAsync(string filter = null, int? top = null, int? skip = null, CancellationToken cancellationToken = default)
        {
            async Task<Page<ApiReleaseResource>> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.GetAll");
                scope.Start();
                try
                {
                    var response = await _apiReleaseRestClient.ListByServiceAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, filter, top, skip, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new ApiReleaseResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            async Task<Page<ApiReleaseResource>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.GetAll");
                scope.Start();
                try
                {
                    var response = await _apiReleaseRestClient.ListByServiceNextPageAsync(nextLink, Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, filter, top, skip, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new ApiReleaseResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
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
        /// Lists all releases of an API. An API release is created when making an API Revision current. Releases are also used to rollback to previous revisions. Results will be paged and can be constrained by the $top and $skip parameters.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases
        /// Operation Id: ApiRelease_ListByService
        /// </summary>
        /// <param name="filter"> |     Field     |     Usage     |     Supported operators     |     Supported functions     |&lt;/br&gt;|-------------|-------------|-------------|-------------|&lt;/br&gt;| notes | filter | ge, le, eq, ne, gt, lt | substringof, contains, startswith, endswith |&lt;/br&gt;. </param>
        /// <param name="top"> Number of records to return. </param>
        /// <param name="skip"> Number of records to skip. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> A collection of <see cref="ApiReleaseResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual Pageable<ApiReleaseResource> GetAll(string filter = null, int? top = null, int? skip = null, CancellationToken cancellationToken = default)
        {
            Page<ApiReleaseResource> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.GetAll");
                scope.Start();
                try
                {
                    var response = _apiReleaseRestClient.ListByService(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, filter, top, skip, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new ApiReleaseResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            Page<ApiReleaseResource> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.GetAll");
                scope.Start();
                try
                {
                    var response = _apiReleaseRestClient.ListByServiceNextPage(nextLink, Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, filter, top, skip, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new ApiReleaseResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
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
        /// Checks to see if the resource exists in azure.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases/{releaseId}
        /// Operation Id: ApiRelease_Get
        /// </summary>
        /// <param name="releaseId"> Release identifier within an API. Must be unique in the current API Management service instance. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="releaseId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="releaseId"/> is null. </exception>
        public virtual async Task<Response<bool>> ExistsAsync(string releaseId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(releaseId, nameof(releaseId));

            using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.Exists");
            scope.Start();
            try
            {
                var response = await _apiReleaseRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, releaseId, cancellationToken: cancellationToken).ConfigureAwait(false);
                return Response.FromValue(response.Value != null, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Checks to see if the resource exists in azure.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}/apis/{apiId}/releases/{releaseId}
        /// Operation Id: ApiRelease_Get
        /// </summary>
        /// <param name="releaseId"> Release identifier within an API. Must be unique in the current API Management service instance. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="releaseId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="releaseId"/> is null. </exception>
        public virtual Response<bool> Exists(string releaseId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(releaseId, nameof(releaseId));

            using var scope = _apiReleaseClientDiagnostics.CreateScope("ApiReleaseCollection.Exists");
            scope.Start();
            try
            {
                var response = _apiReleaseRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, releaseId, cancellationToken: cancellationToken);
                return Response.FromValue(response.Value != null, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        IEnumerator<ApiReleaseResource> IEnumerable<ApiReleaseResource>.GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IAsyncEnumerator<ApiReleaseResource> IAsyncEnumerable<ApiReleaseResource>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAllAsync(cancellationToken: cancellationToken).GetAsyncEnumerator(cancellationToken);
        }
    }
}
