// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.Migrate.Models
{
    /// <summary> Defines the request body for bulk remove of move resources operation. </summary>
    public partial class BulkRemoveContent
    {
        /// <summary> Initializes a new instance of BulkRemoveContent. </summary>
        public BulkRemoveContent()
        {
            MoveResources = new ChangeTrackingList<string>();
        }

        /// <summary> Gets or sets a value indicating whether the operation needs to only run pre-requisite. </summary>
        public bool? ValidateOnly { get; set; }
        /// <summary> Gets or sets the list of resource Id&apos;s, by default it accepts move resource id&apos;s unless the input type is switched via moveResourceInputType property. </summary>
        public IList<string> MoveResources { get; }
        /// <summary> Defines the move resource input type. </summary>
        public MoveResourceInputType? MoveResourceInputType { get; set; }
    }
}
