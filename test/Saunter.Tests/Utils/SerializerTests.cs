﻿using System.Reflection;

using Saunter.Generation;
using Saunter.Serialization;
using Saunter.Tests.Generation.DocumentGeneratorTests;

using Shouldly;

using Xunit;

namespace Saunter.Tests.Utils;

public abstract class SerializerTests
{
    private readonly DocumentGenerator _documentGenerator;
    private readonly AsyncApiOptions _options;

    protected SerializerTests()
    {
        _options = new()
        {
            AsyncApi = new()
            {
                Info = new()
                {
                    Version = "1.0.0",
                    Title = GetType().FullName,
                }
            }
        };
        _documentGenerator = new DocumentGenerator();
    }

    protected abstract IAsyncApiDocumentSerializer CreateSerializer();

    [Fact]
    public void TestSerialize()
    {
        AsyncApiSchema.v2.AsyncApiDocument doc = _documentGenerator.GenerateDocument(new[] { typeof(MethodAttributesTests.TenantMessagePublisher).GetTypeInfo() }, _options, _options.AsyncApi, ActivatorServiceProvider.Instance);
        string serializedDoc = CreateSerializer().Serialize(doc);

        serializedDoc.ShouldNotBeNullOrWhiteSpace();
    }
}

public class NewtonsoftSerializerTests : SerializerTests
{
    protected override IAsyncApiDocumentSerializer CreateSerializer()
    {
        return new NewtonsoftAsyncApiDocumentSerializer();
    }
}
