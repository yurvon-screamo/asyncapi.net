﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using Saunter.AsyncApiSchema.v2;
using Saunter.Generation;
using Saunter.Generation.Filters;

namespace Saunter
{
    public class AsyncApiOptions
    {
        /// <summary>
        ///     The base asyncapi schema.
        ///     This will be augmented with other information auto-discovered from attributes.
        /// </summary>
        public AsyncApiDocument AsyncApi { get; set; } = new AsyncApiDocument();

        /// <summary>
        ///     A list of marker types from assemblies to scan for Saunter attributes.
        /// </summary>
        public IList<Type> AssemblyMarkerTypes { get; set; } = new List<Type>();

        /// <summary>
        ///     A function to select a schemaId for a type.
        /// </summary>
        public Func<Type, string> SchemaIdSelector { get; set; } = DefaultSchemaIdFactory.Generate;

        /// <summary>
        ///     A function to select the name for a property.
        /// </summary>
        public Func<MemberInfo, string> PropertyNameSelector { get; set; } =
            prop => JsonNamingPolicy.CamelCase.ConvertName(prop.Name);

        /// <summary>
        ///     A list of filters that will be applied to the generated AsyncAPI document.
        /// </summary>
        public IList<IDocumentFilter> DocumentFilters { get; } = new List<IDocumentFilter>();

        /// <summary>
        ///     A list of filters that will be applies to any generated channels.
        /// </summary>
        public IList<IChannelItemFilter> ChannelItemFilters { get; } = new List<IChannelItemFilter>();

        /// <summary>
        ///     A list of filters that will be applied to any generated Publish operations.
        /// </summary>
        public IList<OperationFilter> OperationFilters { get; } = new List<OperationFilter>();

        /// <summary>
        ///     Options related to the Saunter middleware
        /// </summary>
        public AsyncApiMiddlewareOptions Middleware { get; set; } = new AsyncApiMiddlewareOptions();
    }

    public class AsyncApiMiddlewareOptions
    {
        public const string AsyncApiMiddlewareDefaultRoute = "/asyncapi/asyncapi.json";
        public const string AsyncApiUiMiddlewareBasePath = "/asyncapi/ui/";
        public const string AsyncApiUiMiddlewareDefaultRoute = "/asyncapi/ui/index.html";

        /// <summary>
        ///     The route which the AsyncApi document will be hosted
        /// </summary>
        public string Route { get; set; } = AsyncApiMiddlewareDefaultRoute;

        /// <summary>
        ///     The route which the AsyncApi UI will be hosted
        /// </summary>
        public string UiRoute { get; set; } = AsyncApiUiMiddlewareDefaultRoute;

        /// <summary>
        /// 
        /// </summary>
        public string UiBaseRoute { get; set; } = AsyncApiUiMiddlewareBasePath;

        /// <summary>
        ///     
        /// </summary>
        public string PlaygroundBaseAddress { get; set; } = "https://playground.asyncapi.io/";

        /// <summary>
        ///     Key/Value pairs where keys will be replaced by the corresponding value in the html
        ///     returned from playground
        /// </summary>
        public Dictionary<string, string> HtmlProxyRewrites { get; set; } = new Dictionary<string, string>
        {
            {
                "text-xs uppercase text-grey mt-10 mb-4 font-thin",
                "text-xs uppercase text-dark-grey mt-10 mb-4 font-thin"
            },
            {
                "text-grey text-sm",
                "text-dark-grey text-sm"
            }
        };

        /// <summary>
        ///     Key/Value pairs where keys will be replaced by the corresponding value in the other
        ///     assets (css, js) returned from playground
        /// </summary>
        public Dictionary<string, string> AssetsProxyRewrites { get; set; } = new Dictionary<string, string>
        {
            {
                "font-weight: lighter",
                "font-weight: normal"
            }
        };
    }
}