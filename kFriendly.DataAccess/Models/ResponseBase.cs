﻿using kFriendly.Entities;
using Newtonsoft.Json;

namespace kFriendly.Core.Models
{
    public abstract class ResponseBase : ModelBase
    {
        [JsonProperty("error")]
        public ResponseError Error { get; set; }
    }

    public abstract class BusinessResponseBase : ResponseBase
    {
        [JsonProperty("businesses")]
        public BusinessDetailsResponse[] Businesses { get; set; }
    }

    public sealed class ResponseError
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}