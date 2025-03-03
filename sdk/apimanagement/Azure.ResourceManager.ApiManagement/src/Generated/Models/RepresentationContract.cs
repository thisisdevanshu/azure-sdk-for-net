// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.ApiManagement.Models
{
    /// <summary> Operation request/response representation details. </summary>
    public partial class RepresentationContract
    {
        /// <summary> Initializes a new instance of RepresentationContract. </summary>
        /// <param name="contentType"> Specifies a registered or custom content type for this representation, e.g. application/xml. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="contentType"/> is null. </exception>
        public RepresentationContract(string contentType)
        {
            if (contentType == null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            ContentType = contentType;
            FormParameters = new ChangeTrackingList<ParameterContract>();
            Examples = new ChangeTrackingDictionary<string, ParameterExampleContract>();
        }

        /// <summary> Initializes a new instance of RepresentationContract. </summary>
        /// <param name="contentType"> Specifies a registered or custom content type for this representation, e.g. application/xml. </param>
        /// <param name="schemaId"> Schema identifier. Applicable only if &apos;contentType&apos; value is neither &apos;application/x-www-form-urlencoded&apos; nor &apos;multipart/form-data&apos;. </param>
        /// <param name="typeName"> Type name defined by the schema. Applicable only if &apos;contentType&apos; value is neither &apos;application/x-www-form-urlencoded&apos; nor &apos;multipart/form-data&apos;. </param>
        /// <param name="formParameters"> Collection of form parameters. Required if &apos;contentType&apos; value is either &apos;application/x-www-form-urlencoded&apos; or &apos;multipart/form-data&apos;.. </param>
        /// <param name="examples"> Exampled defined for the representation. </param>
        internal RepresentationContract(string contentType, string schemaId, string typeName, IList<ParameterContract> formParameters, IDictionary<string, ParameterExampleContract> examples)
        {
            ContentType = contentType;
            SchemaId = schemaId;
            TypeName = typeName;
            FormParameters = formParameters;
            Examples = examples;
        }

        /// <summary> Specifies a registered or custom content type for this representation, e.g. application/xml. </summary>
        public string ContentType { get; set; }
        /// <summary> Schema identifier. Applicable only if &apos;contentType&apos; value is neither &apos;application/x-www-form-urlencoded&apos; nor &apos;multipart/form-data&apos;. </summary>
        public string SchemaId { get; set; }
        /// <summary> Type name defined by the schema. Applicable only if &apos;contentType&apos; value is neither &apos;application/x-www-form-urlencoded&apos; nor &apos;multipart/form-data&apos;. </summary>
        public string TypeName { get; set; }
        /// <summary> Collection of form parameters. Required if &apos;contentType&apos; value is either &apos;application/x-www-form-urlencoded&apos; or &apos;multipart/form-data&apos;.. </summary>
        public IList<ParameterContract> FormParameters { get; }
        /// <summary> Exampled defined for the representation. </summary>
        public IDictionary<string, ParameterExampleContract> Examples { get; }
    }
}
