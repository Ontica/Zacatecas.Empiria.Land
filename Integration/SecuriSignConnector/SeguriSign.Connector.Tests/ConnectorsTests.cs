using System;
using Xunit;
using SeguriSign.Connector.Services;

namespace SeguriSign.Tests
{
    public class ConnectorsTests
    {

        [Fact]
        public void ShouldSignWithContent() {

            var conn = new ConnectorService();

            var result = conn.SignWithContent();

            Xunit.Assert.NotNull(result);

        }


        [Fact]
        public void Prueba2()
        {

            var conn = new ConnectorService();

            var result = conn.AddUser();

            Xunit.Assert.NotNull(result);

        }

    }
}
