﻿using Newtonsoft.Json;

namespace Yelp.Api.Models
{
    public abstract class ResponseBase : ModelBase
    {
        [JsonProperty("error")]
        public ResponseError Error { get; set; }
    }

    public abstract class BusinessResponseBase : ResponseBase
    {
        [JsonProperty("businesses")]
        public BusinessResponse[] Businesses { get; set; }
    }

    public sealed class ResponseError
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}