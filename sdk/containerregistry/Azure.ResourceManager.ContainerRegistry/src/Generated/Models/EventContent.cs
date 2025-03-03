// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.ContainerRegistry.Models
{
    /// <summary> The content of the event request message. </summary>
    public partial class EventContent
    {
        /// <summary> Initializes a new instance of EventContent. </summary>
        internal EventContent()
        {
        }

        /// <summary> Initializes a new instance of EventContent. </summary>
        /// <param name="id"> The event ID. </param>
        /// <param name="timestamp"> The time at which the event occurred. </param>
        /// <param name="action"> The action that encompasses the provided event. </param>
        /// <param name="target"> The target of the event. </param>
        /// <param name="request"> The request that generated the event. </param>
        /// <param name="actor"> The agent that initiated the event. For most situations, this could be from the authorization context of the request. </param>
        /// <param name="source"> The registry node that generated the event. Put differently, while the actor initiates the event, the source generates it. </param>
        internal EventContent(string id, DateTimeOffset? timestamp, string action, Target target, Request request, Actor actor, Source source)
        {
            Id = id;
            Timestamp = timestamp;
            Action = action;
            Target = target;
            Request = request;
            Actor = actor;
            Source = source;
        }

        /// <summary> The event ID. </summary>
        public string Id { get; }
        /// <summary> The time at which the event occurred. </summary>
        public DateTimeOffset? Timestamp { get; }
        /// <summary> The action that encompasses the provided event. </summary>
        public string Action { get; }
        /// <summary> The target of the event. </summary>
        public Target Target { get; }
        /// <summary> The request that generated the event. </summary>
        public Request Request { get; }
        /// <summary> The agent that initiated the event. For most situations, this could be from the authorization context of the request. </summary>
        internal Actor Actor { get; }
        /// <summary> The subject or username associated with the request context that generated the event. </summary>
        public string ActorName
        {
            get => Actor?.Name;
        }

        /// <summary> The registry node that generated the event. Put differently, while the actor initiates the event, the source generates it. </summary>
        public Source Source { get; }
    }
}
