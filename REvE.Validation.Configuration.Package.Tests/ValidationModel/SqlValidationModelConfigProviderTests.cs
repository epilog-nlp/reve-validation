using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace REvE.Validation.Configuration.Tests
{
    using Models;
    using Contracts;

    //[TestClass]
    public class SqlValidationModelConfigProviderTests : ValidationModelConfigProviderTests<SqlValidationModelConfigProvider>
    {
        protected override Lazy<SqlValidationModelConfigProvider> Configuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
