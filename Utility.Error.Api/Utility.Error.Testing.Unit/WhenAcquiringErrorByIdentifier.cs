using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Utility.Error.Testing.Unit.Controller;


namespace Utility.Error.Testing.Unit
{
    /// <summary>
    /// When Acquiring Error By Identifier.
    /// </summary>
    [TestClass]
    public class WhenAcquiringErrorByIdentifier
    {
        private static IServiceProvider _serviceProvider;
        private const string Identifier01 = "d11337c1-d222-49b6-aaa0-ff443b6ddd36";

        /// <summary>
        /// Initialise.
        /// </summary>
        [TestInitialize]
        public void Initialise()
        {
            // Register Service Types.
            _serviceProvider = Startup.Initialise();
        }

        // Test Methods.
        #region TestMethods

        /// <summary>
        /// Then Support Services Can Be Initialised.
        /// </summary>
        [TestMethod]
        [TestCategory("UNIT")]
        public void UN_APP_01_ThenSupportingServicesCanBeInitialised()
        {
            // Arrange.
            using (var scope = _serviceProvider.CreateScope())
            {
                // Act.
                var controller = scope.ServiceProvider.GetService<BaseController>().As<ErrorController>();

                // Assert.
                controller.Should().NotBeNull();
            }
        }

        /// <summary>
        /// Then Single Error Can Be Read With Identifier.
        /// </summary>
        [TestMethod]
        [TestCategory("UNIT")]
        public void UN_APP_02_ThenSingleErrorCanBeReadWithIdentifier()
        {
            // Arrange.
            using (var scope = _serviceProvider.CreateScope())
            {
                // Act.
                var controller = scope.ServiceProvider.GetService<BaseController>().As<ErrorController>();
                var error = controller.GetErrorByIdentifier(Identifier01);

                // Assert.
                error.Should().NotBeNull();
                error.Identifier.Should().Be(Identifier01);
            }
        }

        #endregion
    }
}
